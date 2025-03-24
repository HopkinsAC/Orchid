//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Orchid.Profiling;

internal struct ProfileResult
{
   public string Name;
   public long Start;
   public long End;
   public int ThreadId;
};

public class ProfileServer 
{
   // Construction
   //
  
   // API
   //
   public void BeginSession(string? fileName = null)
   {
      // If I have a null file name here, then I need to create a valid, unique
      // file name.  Using an ISO timestamp prepended to a constant value.
      //
      if (fileName == null)
      {
         // NOTE I could have used DateTime.Now here, but DateTime.UtcNow seems
         //      a better fit (more accurate? - is that the right word?).
         //
         var isoTimestamp = DateTime.UtcNow.ToString("yyyyMMdd-THHmmss");
         fileName = $"{isoTimestamp}_ProfileResults.json";
      }
      
      _outputWriter = new StreamWriter(File.OpenWrite(fileName));
      
      WriteHeader();
   }
    
   public void EndSession()
   {
      WriteFooter();
      _outputWriter?.Close();
      _profileCount = 0;
   }

   internal void WriteResult(ProfileResult result)
   {
      if (_profileCount++ > 0)
      {
         _outputWriter?.Write(",");
      }
      
      var name = result.Name.Replace("\"", "\'");
      _outputWriter?.Write("{");
      _outputWriter?.Write("\"cat\":\"function\",");
      _outputWriter?.Write($"\"dur\":{result.End - result.Start},");
      _outputWriter?.Write($"\"name\":\"{name}\",");
      _outputWriter?.Write($"\"ph\":\"X\",");
      _outputWriter?.Write($"\"pid\":0,");
      _outputWriter?.Write($"\"tid\":{result.ThreadId},");
      _outputWriter?.Write($"\"ts\":{result.Start}");
      _outputWriter?.Write("}");

      _outputWriter?.Flush();
   }
 
   public static ProfileServer Instance { get; } = new();
   
   // Implementation
   //
   private int _profileCount;
   private TextWriter? _outputWriter;

   private ProfileServer()
   {
   }
   
   private void WriteHeader()
   {
      _outputWriter?.Write("{\"otherData\": {},\"traceEvents\":[");
      _outputWriter?.Flush();
   }
    
   private void WriteFooter()
   {
      _outputWriter?.Write("]}");
      _outputWriter?.Flush();
   }
}

// NOTE This is a marker interface.
//
public interface IProfiler : IDisposable
{
}

public class MethodProfiler : IProfiler 
{
   // Construction
   //
   public MethodProfiler([CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
   {
      _ = callerMemberName ?? throw new ArgumentNullException(nameof(callerMemberName));
      
      _methodName = $"{callerMemberName}: {callerLineNumber}";
      _startTicks = Stopwatch.GetTimestamp();
   }
   
   // API
   //
   public void Dispose()
   {
      var endTicks = Stopwatch.GetTimestamp();
      

      var microsecondsPerTicks = Stopwatch.Frequency / 1_000_000f;
      var start = (long) (_startTicks / microsecondsPerTicks);
      var end = (long) (endTicks / microsecondsPerTicks);
      var threadId = Environment.CurrentManagedThreadId;
           
      ProfileServer.Instance.WriteResult(new ProfileResult
      {
         Name = _methodName,
         Start = start,
         End =  end,
         ThreadId = threadId
      });
      
      GC.SuppressFinalize(this);
   }
   
   // Implementation
   //
   private readonly string _methodName;
   private readonly long _startTicks;
}

public class ScopeProfiler(string ScopeName) : IProfiler
{
   // Construction
   //

   // API
   //
   public void Dispose()
   {
      var endTicks = Stopwatch.GetTimestamp();

      var microsecondsPerTicks = Stopwatch.Frequency / 1_000_000f;
      var start = (long) (_startTicks / microsecondsPerTicks);
      var end = (long) (endTicks / microsecondsPerTicks);
      var threadId = Environment.CurrentManagedThreadId;
           
      ProfileServer.Instance.WriteResult(new ProfileResult
      {
         Name = ScopeName,
         Start = start,
         End =  end,
         ThreadId = threadId
      });
      
      GC.SuppressFinalize(this);
   }
   
   // Implementation
   //
   private readonly long _startTicks = Stopwatch.GetTimestamp();
}