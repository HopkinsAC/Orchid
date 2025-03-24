//
// Copyright (C) 2025.  Andrew C. Hopkins.  All Rights Reserved.
//

using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Orchid.UI.ViewModels;
using Orchid.UI.Views;

namespace Orchid.UI;

public partial class App : Avalonia.Application
{
   // Construction
   //
  
   // API
   //
   public override void Initialize()
   {
      AvaloniaXamlLoader.Load(this);
   }

   public override void OnFrameworkInitializationCompleted()
   {
      if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
      {
         // Line below is needed to remove Avalonia data validation.
         // Without this line you will get duplicate validations from both Avalonia and CT
         BindingPlugins.DataValidators.RemoveAt(0);
         desktop.MainWindow = new MainWindow
         {
            DataContext = new MainWindowViewModel(),
         };
      }

      base.OnFrameworkInitializationCompleted();
   }

   // Implementation
   //
}