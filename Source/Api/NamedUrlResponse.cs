//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Text.Json.Serialization;

namespace Orchid.Api;

public record NamedUrlResponse
{
   // Construction
   //
   
   // API
   //
   [JsonPropertyName("count")]
   public int Count { get; init; }

   [JsonPropertyName("next")]
   public string? Next { get; init; } = string.Empty;
   
   [JsonPropertyName("previous")]
   public string? Previous { get; init; } = string.Empty;
   
   [JsonPropertyName("results")]
   public NamedUrlProperty[] Results { get; init; } = [];
   
   // Implementation
   //
}

public record NamedUrlProperty
{
   // Construction
   //
   
   // API
   //
   [JsonPropertyName("name")]
   public string Name { get; init; } = string.Empty;
   
   [JsonPropertyName("url")]
   public string Url { get; init; } = string.Empty;

   // Implementation
   //
}