//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Orchid.Logging;

public static class Log
{
   // API
   //
   public static ILogger CoreLogger
      =>
         _coreLogger ?? throw new InvalidOperationException("Core Logger not initialized");

   public static void Initialize()
   {
      if (_isInitialized)
      {
         return;
      }
      
      Serilog.Log.Logger = new LoggerConfiguration()
         .WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}"
         ).MinimumLevel.Verbose()
         .CreateLogger();
      
      var logFactory = new LoggerFactory().AddSerilog(Serilog.Log.Logger);
      
      _coreLogger = logFactory.CreateLogger("Core");
      _isInitialized = true;
   }
   
   public static void Initialize(IConfiguration config)
   {
      if (_isInitialized)
      {
         return;
      }
      
      Serilog.Log.Logger = new LoggerConfiguration()
         .ReadFrom.Configuration(config)
         .CreateLogger();
      
      var logFactory = new LoggerFactory().AddSerilog(Serilog.Log.Logger);
      
      _coreLogger = logFactory.CreateLogger("Core");
      _isInitialized = true;
   }
   
   // Implementation
   //
   private static bool _isInitialized;
   private static ILogger? _coreLogger = null;
}