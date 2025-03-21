//
// Copyright (C) 2025.  Andrew C. Hopkins.  All Rights Reserved.
//

using System.Collections.ObjectModel;
using Orchid.UI.Infrastructure.Mvvm;

namespace Orchid.UI.ViewModels;

public struct Pokemon
{
   public required string Name { get; set; }
   public required int PokedexID { get; set; }

   public Pokemon()
   {
   }
}

public partial class MainWindowViewModel : ViewModelBase
{
   // Construction
   //
   public MainWindowViewModel()
   {
      Pokemon.Add(new Pokemon { Name="Bulbasaur", PokedexID = 1 });
      Pokemon.Add(new Pokemon { Name="Ivysaur", PokedexID = 2 });
      Pokemon.Add(new Pokemon { Name="Venausaur", PokedexID = 3 });
      Pokemon.Add(new Pokemon { Name="Charmander", PokedexID = 4 });
      Pokemon.Add(new Pokemon { Name="Charmeleon", PokedexID = 5 });
      Pokemon.Add(new Pokemon { Name="Charizard", PokedexID = 6 });
      Pokemon.Add(new Pokemon { Name="Squirtle", PokedexID = 7 });
      Pokemon.Add(new Pokemon { Name="Wartortle", PokedexID = 8 });
      Pokemon.Add(new Pokemon { Name="Blastoise", PokedexID = 9 });
      
      Pokemon.Add(new Pokemon { Name="Caterpie", PokedexID = 10 });
      Pokemon.Add(new Pokemon { Name="Metapod", PokedexID = 11 });
      Pokemon.Add(new Pokemon { Name="Butterfree", PokedexID = 12 });
      Pokemon.Add(new Pokemon { Name="Weedle", PokedexID = 13 });
      Pokemon.Add(new Pokemon { Name="Kakuna", PokedexID = 14 });
      Pokemon.Add(new Pokemon { Name="Beedrill", PokedexID = 15 });
      Pokemon.Add(new Pokemon { Name="Pidgey", PokedexID = 16 });
      Pokemon.Add(new Pokemon { Name="Pidgeotto", PokedexID = 17 });
      Pokemon.Add(new Pokemon { Name="Pidgeot", PokedexID = 18 });
      
      Pokedexes.Add("Kanto");
      Pokedexes.Add("Let's Go! Kanto");
      Pokedexes.Add("Original Johto");
      Pokedexes.Add("Updated Johto");
      Pokedexes.Add("Original Hoenn");
      Pokedexes.Add("New Hoenn");
      Pokedexes.Add("Original Sinnoh");
      Pokedexes.Add("Extended Sinnoh");
      Pokedexes.Add("Original Unova");
      Pokedexes.Add("Updated Unova");
      Pokedexes.Add("Central Kalos");
      Pokedexes.Add("Coastal Kalos");
      Pokedexes.Add("Mountain Kalos");
      Pokedexes.Add("Original Alola");
      Pokedexes.Add("Original Melemele");
      Pokedexes.Add("Original Akala");
      Pokedexes.Add("Original Ula'ula");
      Pokedexes.Add("Original Poni");
      Pokedexes.Add("Updated Alola");
      Pokedexes.Add("Updated Melemele");
      Pokedexes.Add("Updated Akala");
      Pokedexes.Add("Updated Ula'ula");
      Pokedexes.Add("Updated Poni");
      Pokedexes.Add("Galar");
      Pokedexes.Add("Isle of Armor");
      Pokedexes.Add("Crown Tundra");
      Pokedexes.Add("Hisui");
      Pokedexes.Add("Paldea");
      Pokedexes.Add("Kitakami");
      Pokedexes.Add("Blueberry");
   }
   
   // API
   //
   public ObservableCollection<Pokemon> Pokemon { get; } = new();
   
   public ObservableCollection<string> Pokedexes { get; } = new();
   
   // Implementation
   //
}