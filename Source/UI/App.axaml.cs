//
// Copyright (C) 2025.  Andrew C. Hopkins.  All Rights Reserved.
//

using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Orchid.Logging;
using Orchid.Profiling;
using Orchid.UI.ViewModels;
using Orchid.UI.Views;

namespace Orchid.UI;

public partial class App : Avalonia.Application
{
   // Construction
   //
   public App()
   {
#if PROFILING
      ProfileServer.Instance.BeginSession();
      using var mp = new MethodProfiler(); 
#endif

      Log.Initialize();
   }
  
   // API
   //
   public override void Initialize()
   {
 #if PROFILING
       using var mp = new MethodProfiler(); 
 #endif
 
       AvaloniaXamlLoader.Load(this);
   }

   public override void OnFrameworkInitializationCompleted()
   {
#if PROFILING
      using var mp = new MethodProfiler(); 
#endif
        
      if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
      {
         // Line below is needed to remove Avalonia data validation.
         // Without this line you will get duplicate validations from both Avalonia and CT
         BindingPlugins.DataValidators.RemoveAt(0);
         desktop.MainWindow = new MainWindow
         {
            DataContext = new MainWindowViewModel(),
         };

         desktop.Exit += (_, __) =>
         {
#if PROFILING
            ProfileServer.Instance.EndSession();
#endif
         };
      }

      base.OnFrameworkInitializationCompleted();
   }

   // Implementation
   //
}