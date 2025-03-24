//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using Orchid.Profiling;

namespace Orchid.Services;

[Module(ModuleName = "ServicesModule")]
public class ServicesModule : IModule
{
   // Construction
   //
   public ServicesModule(IContainerProvider containerProvider)
   {
      _containerProvider = containerProvider;
   }
   
   // API
   //
   public void RegisterTypes(IContainerRegistry containerRegistry)
   {
#if PROFILING
      using var mp = new MethodProfiler();
#endif
      
      // Register singleton services.
      //
      containerRegistry.RegisterSingleton<IPokedexService, PokedexService>();

   }

   public void OnInitialized(IContainerProvider containerProvider)
   {
   }

   public void LoadData()
   {
      var pokedexService = _containerProvider.Resolve<IPokedexService>();
      Task.Run(() => pokedexService?.LoadPokedexesAsync()).Wait();
   }
   
   // Implementation
   //
   private readonly IContainerProvider _containerProvider;
}