//
// Copyright (C) 2025.  Andrew C. Hopkins.  All Rights Reserved.
//

using Orchid.UI.Infrastructure.Mvvm;

namespace Orchid.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
   public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
}