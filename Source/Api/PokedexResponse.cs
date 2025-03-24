//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Text.Json.Serialization;

namespace Orchid.Api;

public record PokedexResponse
{
   // Construction
   //
   
   // API
   //
   [JsonPropertyName("id")]
   public int Id { get; init; }
            
   [JsonPropertyName("name")]
   public string Name { get; init; } = string.Empty;
         
   [JsonPropertyName("names")]
   public NameProperty[] Names { get; init; } = [];
 
   [JsonPropertyName("descriptions")]
   public DescriptionProperty[] Descriptions { get; init; } = [];
   
   [JsonPropertyName("is_main_series")]
   public bool IsMainSeries { get; init; }

   [JsonPropertyName("region")]
   public NamedUrlProperty Region { get; init; } = new();

   [JsonPropertyName("version_groups")]
   public NamedUrlProperty[] VersionGroups { get; init; } = [];
         
   [JsonPropertyName("pokemon_entries")]
   public PokemonEntryProperty[] PokemonEntries { get; init; } = [];
   
   // Implementation
   //
}


public record NameProperty
{
   // Construction
   //
   
   // API
   //
   [JsonPropertyName("name")]
   public string Name { get; init; } = string.Empty;
   
   [JsonPropertyName("language")]
   public NamedUrlProperty Language { get; init; } = new();
   
   // Implementation
   //
}

public record DescriptionProperty
{
   // Construction
   //
      
   // API
   //
   [JsonPropertyName("description")]
   public string Description { get; init; } = string.Empty;
    
   [JsonPropertyName("language")]
   public NamedUrlProperty Language { get; init; } = new();
   
   // Implementation
   //
}

public record PokemonEntryProperty
{
   // Construction
   //
    
   // API
   //
   [JsonPropertyName("entry_number")]
   public int EntryNumber { get; init; }
 
   [JsonPropertyName("pokemon_species")]
   public NamedUrlProperty PokemonSpecies { get; init; } = new();
   
   // Implementation
   //
}