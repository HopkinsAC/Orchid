//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

namespace Orchid.Domain;

public record PokedexId(int Value);

public record PokedexEntryId(int Value);

public class Pokedex
{
   // Construction
   //
   
   // API
   //
   public PokedexId Id { get; init; } = new(0);
   
   public string Name { get; init; } = string.Empty;
   
   public string Description { get; init; } = string.Empty;

   public List<PokedexEntry> PokedexEntries { get; } = new List<PokedexEntry>();
   
   // Implementation
   //
}

public class PokedexEntry
{
   // Construction
   //
   
   // API
   //
   public PokedexEntryId EntryId { get; init; } = new(0);

   public PokemonId PokemonId { get; set; } = new(0);
   
   public string Name { get; init; } = string.Empty;
   
   // Implementation
   //
}