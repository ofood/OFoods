using System.Collections.Concurrent;

namespace OFoods.Utility.Extensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return dict.TryRemove(key, out value);
        }
    }
}
