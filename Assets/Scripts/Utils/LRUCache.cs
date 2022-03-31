using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace toe {

    //https://stackoverflow.com/questions/754233/is-it-there-any-lru-implementation-of-idictionary
    public class LRUCache<K,V>
    {
        public delegate void OnRemove(K key, V value);
        private OnRemove _removeCallback;

        private uint _capacity;
        private Dictionary<K, LinkedListNode<LRUCacheItem>> _cacheMap = new Dictionary<K, LinkedListNode<LRUCacheItem>>();
        private LinkedList<LRUCacheItem> _lruList = new LinkedList<LRUCacheItem>();

        public LRUCache(uint capacity, OnRemove removeCallback)
        {
            _capacity = capacity;
            _removeCallback = removeCallback;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public V Get(K key)
        {
            LinkedListNode<LRUCacheItem> node;
            if (_cacheMap.TryGetValue(key, out node))
            {
                V value = node.Value.value;
                _lruList.Remove(node);
                _lruList.AddLast(node);
                return value;
            }
            return default(V);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(K key, V value)
        {
            if (_cacheMap.TryGetValue(key, out var existingNode))
            {
                _lruList.Remove(existingNode);
            }
            else if (_cacheMap.Count >= _capacity)
            {
                RemoveFirst();
            }

            LRUCacheItem cacheItem = new LRUCacheItem(key, value);
            LinkedListNode<LRUCacheItem> node = new LinkedListNode<LRUCacheItem>(cacheItem);
            _lruList.AddLast(node);
            // cacheMap.Add(key, node); - here's bug if try to add already existing value
            _cacheMap[key] = node;
        }

        private void RemoveFirst()
        {
            LinkedListNode<LRUCacheItem> node = _lruList.First;
            _lruList.RemoveFirst();

            _cacheMap.Remove(node.Value.key);
            _removeCallback(node.Value.key, node.Value.value);
        }

        private class LRUCacheItem
        {
            public LRUCacheItem(K k, V v)
            {
                key = k;
                value = v;
            }
            public K key;
            public V value;
        }
    }

}