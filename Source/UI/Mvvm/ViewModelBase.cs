// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.

namespace Orchid.UI.Mvvm;

public abstract class ViewModelBase : BindableBase, INavigationAware
{
   // Construction
   //
   
   // API
   //
   public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
   
   public virtual void OnNavigatedTo(NavigationContext navigationContext)
   {
   }

   public virtual void OnNavigatedFrom(NavigationContext navigationContext)
   {
   }
   
   // Implementation
   //
}