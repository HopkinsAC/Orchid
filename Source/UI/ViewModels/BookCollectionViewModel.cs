//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Collections.ObjectModel;

namespace Orchid.UI.ViewModels;

public class BookCollectionViewModel : BindableBase
{
   // Construction
   //
   public BookCollectionViewModel()
   {
      Books.Add(new SpineViewModel {BookId = 1, Title = "Bulbasaur"});
      Books.Add(new SpineViewModel {BookId = 2, Title = "Ivysaur"});
      Books.Add(new SpineViewModel {BookId = 3, Title = "Venusaur"});
      Books.Add(new SpineViewModel {BookId = 4, Title = "Charmander"});
      Books.Add(new SpineViewModel {BookId = 5, Title = "Charmeleon"});
      Books.Add(new SpineViewModel {BookId = 6, Title = "Charizard"});
      Books.Add(new SpineViewModel {BookId = 7, Title = "Squirtle"});
      Books.Add(new SpineViewModel {BookId = 8, Title = "Wartortle"});
      Books.Add(new SpineViewModel {BookId = 9, Title = "Blastoise"});
      Books.Add(new SpineViewModel {BookId = 10, Title = "Caterpie"});
      Books.Add(new SpineViewModel {BookId = 11, Title = "Metapod"});
      Books.Add(new SpineViewModel {BookId = 12, Title = "Butterfree"});
      Books.Add(new SpineViewModel {BookId = 13, Title = "Weedle"});
      Books.Add(new SpineViewModel {BookId = 14, Title = "Kakua"});
      Books.Add(new SpineViewModel {BookId = 15, Title = "Beedrill"});
   }
   
   // API
   //
   public ObservableCollection<SpineViewModel> Books { get; } = [];

   public void Scroll(int offset)
   {
      
   }

   // Implementation
   //
}

public class SpineViewModel: BindableBase
{
   // Construction
   //
   
   // API
   //
   public int BookId
   {
      get => _bookId;
      set => SetProperty(ref _bookId, value);
   }

   public string? Title
   {
      get => _title;
      set => SetProperty(ref _title, value);
   }

   // Implementation
   //
   private int _bookId;
   private string? _title;
}