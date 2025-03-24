//
// Copyright (C) 2025.  Andrew C. Hopkins.  All Rights Reserved.
//

using System.Security.Cryptography;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Orchid.Api;
using Orchid.Logging;
using Orchid.Profiling;
using Orchid.Services;
using Orchid.UI.Views;
using Prism.DryIoc;

namespace Orchid.UI;

public partial class App : PrismApplication 
{
   // Construction
   //
  
   // API
   //
   public override void Initialize()
   {
 #if PROFILING
       ProfileServer.Instance.BeginSession();
       using var mp = new MethodProfiler(); 
 #endif
 
       AvaloniaXamlLoader.Load(this);
       
       Log.Initialize();
       base.Initialize();
   }

   public override async void OnFrameworkInitializationCompleted()
   {
#if PROFILING
      using var mp = new MethodProfiler(); 
#endif

      // We don't support running on non classical desktops (no mobile), so
      // simply bail out and return to the caller.
      //
      if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
      {
         return;
      }
      
      var splashWindow = Container.Resolve<SplashWindow>();
      desktop.MainWindow = splashWindow;
      splashWindow.Show();
      
      // We display the splash screen for two seconds, and then being loading
      // the cached pokemon data.
      //
      try
      {
         await Task.Delay(TimeSpan.FromSeconds(2));
         await Task.Run(LoadData);
      }
      
      catch (TaskCanceledException)
      {
         splashWindow.Close();
         return;
      }

      var shell = Container.Resolve<MainWindow>(); 
      var regionManager = Container.Resolve<IRegionManager>();
      
      RegionManager.SetRegionManager(shell, regionManager);
      RegionManager.UpdateRegions();
      
      InitializeShell(shell);

      desktop.MainWindow = shell;
      desktop.MainWindow?.Show();
      
      splashWindow.Close();

      // Need this block so that I can shut down the profiler session when the
      // application closes.
      // 
      desktop.Exit += OnExit;

      base.OnFrameworkInitializationCompleted();
   }

   protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
   {
      var servicesModule = typeof(ServicesModule);
      moduleCatalog.AddModule(new ModuleInfo()
      {
         ModuleName = servicesModule.Name,
         ModuleType = servicesModule.AssemblyQualifiedName
      });
   }

   protected override void RegisterTypes(IContainerRegistry containerRegistry)
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      containerRegistry.RegisterSingleton<IPokeApi>(() => new PokeApi(new HttpClient()
      {
         BaseAddress = new Uri("https://pokeapi.co/api/v2/")
      }));
   }

   protected override AvaloniaObject CreateShell()
   {
#pragma warning disable CS8603 // Possible null reference return.
      return null;
#pragma warning restore CS8603 // Possible null reference return.
   }

   // Implementation
   //
   
   private void LoadData()
   {
 #if PROFILING
       using var mp = new MethodProfiler();
 #endif
       
       try
      {
         var servicesModule = Container.Resolve<ServicesModule>();
         servicesModule.LoadData();
      }
   
      catch (Exception e)
      {
         Log.CoreLogger.LogError("Failed to load cached data - {desc}", e.Message);
      }
   }

   private static void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
   {
#if PROFILING
      ProfileServer.Instance.EndSession();
#endif
   }
}