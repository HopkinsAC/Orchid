//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using Orchid.Api;
using Orchid.Profiling;

namespace Orchid.Services;

public interface IPokedexService
{
   // Events
   //
   
   // Properties
   //
   
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
   }
   
   // API
   //
   public async Task LoadPokedexesAsync()
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      await Task.Delay(TimeSpan.FromSeconds(5));
   }
   
   // Implementation
   //
   private readonly IPokeApi _pokeApi;
}