// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Orchid.Logging;
using Orchid.UI.ViewModels;

namespace Orchid.UI.Views;

public partial class BookCollectionView : UserControl
{
   public BookCollectionView()
   {
      InitializeComponent();
   }

   private void LeftScrollButton_OnClick(object? sender, RoutedEventArgs e)
   {
      Log.CoreLogger.LogTrace("LeftScrollButton_OnClick");
      
      if (DataContext is not BookCollectionViewModel viewModel)
      {
         return;
      }

      viewModel.Scroll(-1);
   }
   
   private void RightScrollButton_OnClick(object? sender, RoutedEventArgs e)
   {
      Log.CoreLogger.LogTrace("RightScrollButton_OnClick");
      
      if (DataContext is not BookCollectionViewModel viewModel)
      {
         return;
      }
      
      viewModel.Scroll(1);
   }
}