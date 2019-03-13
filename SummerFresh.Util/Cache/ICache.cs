﻿using System;
using System.Collections.Generic;
namespace SummerFresh.Util.Cache
{
    public interface ICache
    {
        IList<string> AllKeys { get;}
        void Clean();
        object GetValue(string key);
        void Remove(string key);
        void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration);
    }
}
