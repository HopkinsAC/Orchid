//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Collections.ObjectModel;
using Orchid.Api;
using Orchid.Services;
using Orchid.UI.Mvvm;

namespace Orchid.UI.ViewModels;

public interface IBookCollection
{
   public int Id { get; }
   public string Title { get; }
}

public class BookCollection : IBookCollection
{
   public int Id { get; init; }
   public string Title { get; init; } = string.Empty;
}

public class BookshelfViewModel : ViewModelBase, INavigationAware
{
   // Construction
   //
   public BookshelfViewModel(IPokedexService pokdexService, IRegionManager regionManager)
   {
      // Set dependencies.
      //
      _pokedexService = pokdexService;
      _regionManager = regionManager;
   }
   
   // API
   //
   public int SelectedIndex
   {
      get => _selectedIndex;
      set => SetProperty(ref _selectedIndex, value);
   }
   public ObservableCollection<IBookCollection> BookCollections { get; } = new();
   
   public override void OnNavigatedFrom(NavigationContext navigationContext)
   {
      BookCollections.Clear();

      foreach (var pokedex in _pokedexService.Pokedexes)
      {
         var bookCollection = new BookCollection()
         {
            Id = pokedex.Id.Value,
            Title = pokedex.Name
         };
         BookCollections.Add(bookCollection);
      }

      // Set the currently selected BookCollection to "Kanto", which SHOULD be
      // at index 1 (if I actually received a list of the correct pokedexes
      // during loading).
      //
      SelectedIndex = 1;
   }
   
   // Implementation
   //
   private readonly IPokedexService _pokedexService;
   private readonly IRegionManager _regionManager;
   
   private int _selectedIndex = -1;
}