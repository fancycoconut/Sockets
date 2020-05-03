using System.Collections.Generic;

namespace Sockets.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }   
            else
            {
                dictionary[key] = value;
            }
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            var exists = dictionary.TryGetValue(key, out TValue value);
            return exists ? value : default;
        }
    }
}
