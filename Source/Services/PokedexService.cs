//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Text.Json;
using Microsoft.Extensions.Logging;
using Orchid.Api;
using Orchid.Bcl;
using Orchid.Domain;
using Orchid.Logging;
using Orchid.Profiling;

namespace Orchid.Services;

public interface IPokedexService
{
   // Events
   //
   
   // Properties
   //
   IEnumerable<Pokedex> Pokedexes { get; } 
   
   // Methods
   //
   Task LoadPokedexesAsync();
}

public class PokedexService : IPokedexService
{
   // Construction
   //
   public PokedexService(IPokeApi pokeApi)
   {
      // Set dependencies
      //
      _pokeApi = pokeApi;

      if (!Directory.Exists(CachePath))
      {
         Directory.CreateDirectory(CachePath);
      }
   }
   
   // API
   //
   public IEnumerable<Pokedex> Pokedexes => _cache.Values;
   
   public async Task LoadPokedexesAsync()
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      var foundOnDisk = await LoadFromDiskAsync();
      if (foundOnDisk)
      {
         return;
      }

      await LoadFromApiAsync();   
   }
   
   // Implementation
   //
   private const string CachePath = "./Cache/Pokedexes";
   
   private readonly IPokeApi _pokeApi;
   private readonly Dictionary<PokedexId, Pokedex> _cache = new();
    
   private async Task<bool> LoadFromDiskAsync()
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
       
      var files = Directory
         .EnumerateFiles(CachePath, "*.json", SearchOption.TopDirectoryOnly)
         .ToList();
      if (files.Count == 0)
      {
         return false;
      }
     
      // Have to load the files into a temporary list because the ordering of
      // files does not necessarily match the pokedex id ordering (ie 1.json, 
      // 10.json, 11.json, 2.json, etc.).
      //
      var pokedexes = new List<Pokedex>();
      foreach (var file in files)
      {
         await using var fs = File.OpenRead(file);
         
         var pokedex = await JsonSerializer.DeserializeAsync<Pokedex>(fs);
         if (pokedex is null)
         {
            Log.CoreLogger.LogError("Failed to load pokedex from disk - {file}", file);
            continue;
         }
         
         pokedexes.Add(pokedex);     
      }
  
      // Once we have the pokedexes loaded, sort them by their pokedex id and
      // insert them into the cache.
      //
      foreach (var pokedex in pokedexes.OrderBy(x => x.Id.Value))
      {
         if (_cache.TryAdd(pokedex.Id, pokedex))
         {
            Log.CoreLogger.LogTrace("Loaded pokedex '{pokedex}' into the cache.", pokedex);
         }
         else
         {
            Log.CoreLogger.LogError("Failed to load pokedex '{pokedex}' into the cache.", pokedex);
         }
      }
   
      return true;
   }
   
   private async Task LoadFromApiAsync()
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif

      // Retrieve the named url collection of pokedexes.  This SHOULD return
      // roughly 33 sets of pokedex information (23 March 2025).  If we don't
      // have any sets, then log it and return to the caller.
      //
      var response = await _pokeApi.GetPokedexesAsync();
      if (response is not { Count: > 0 })
      {
         Log.CoreLogger.LogError("Failed to get pokedexes from api");
         return;
      }
      Log.CoreLogger.LogTrace("Loaded {count} pokedex urls", response.Count);

      // Now that we have a list of pokedexes and their urls, loop through
      // the list and load more detailed information such as name, region,
      // and pokemon species.
      //
      foreach (var entry in response.Results)
      {
         var rawPokedex = await _pokeApi.GetPokedexAsync(entry.Url);
         if (rawPokedex is null || !rawPokedex.IsMainSeries)
         {
            continue;
         }

         var pokedex = new Pokedex
         {
            Id = new PokedexId(rawPokedex.Id),
            Name =
               ((
                  from n in rawPokedex.Names
                  where n.Language.Name == "en"
                  select n.Name
               ).FirstOrDefault() ?? rawPokedex.Name).ToTitleCase(),
            Description =
            (
               from d in rawPokedex.Descriptions
               where d.Language.Name == "en"
               select d.Description
            ).FirstOrDefault() ?? string.Empty,
         };

         _cache.Add(pokedex.Id, pokedex);
         await SaveToDisk(pokedex);
         
         Log.CoreLogger.LogTrace("Loaded pokedex '{pokedex}' into the cache", pokedex);
      }
   }
   
   private static async Task SaveToDisk(Pokedex pokedex)
   {
 #if PROFILING
       using var mp = new MethodProfiler();
 #endif
 
      var file = Path.Combine(CachePath, $"{pokedex.Id.Value}.json");
          
      await using var stream = File.OpenWrite(file);
      await JsonSerializer.SerializeAsync(stream, pokedex).ConfigureAwait(false);
   }
}