//
// Copyright (C) 2025.  Andrew. C. Hopkins.  All Rights Reserved.
//

using System.Globalization;

namespace Orchid.Bcl;

public static class StringExtensions
{
   // API
   //
   public static string ToTitleCase(this string str)
   {
      return !string.IsNullOrEmpty(str)
            ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str)
            : string.Empty
         ;
   }
   
   // Implementation
   //
}