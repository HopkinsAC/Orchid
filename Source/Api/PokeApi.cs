//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Orchid.Logging;
using Orchid.Profiling;

namespace Orchid.Api;

public interface IPokeApi
{
   // Events
   //
   
   // Properties
   //
   Task<NamedUrlResponse?> GetPokedexesAsync();

   // Methods
   //
}

public class PokeApi(HttpClient httpClient) : IPokeApi
{
   // Construction
   //
   
   // API
   //
   public async Task<NamedUrlResponse?> GetPokedexesAsync()
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
       
      return await Get<NamedUrlResponse>("pokedexes", 0, 50);
   }
   
   // Implementation
   //
   private async Task<T?> Get<T>(string request, int offset, int limit)
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      return await Get<T>($"{request}?offset={offset}&limit={limit}");
   }
   
   private async Task<T?> Get<T>(string requestUrl)
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      try
      {
         var response = await httpClient.GetFromJsonAsync<T>(requestUrl);
         if (response != null)
         {
            return response;
         }
         
         Log.CoreLogger.LogError("PokeApi: Could not get a response - response is null");
      }
      
      catch (Exception e)
      {
         Log.CoreLogger.LogError("PokeApi: Could not get a response - request threw an exception: {message}", e.Message);
      }
      
      return default;
   }
}