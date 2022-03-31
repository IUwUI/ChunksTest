using System.Collections.Generic;
using UnityEngine;

namespace toe {

    public class World : MonoBehaviour
    {
        public const uint MAX_CHUNK_COUNT = 20;

        LRUCache<Vector2Int, IChunk> _chunkCache;
        private IChunkGenerator _chunkGenerator;
        private BlockRegistry _blocksRegistry;
        private Camera _camera;
        
        [SerializeField]
        private GameObject _chunkPrefab;

        public BlockRegistry GetBlockRegistry() 
        {
            return _blocksRegistry;
        }

        private void Start()
        {
            _camera = Camera.main;
            _chunkCache = new LRUCache<Vector2Int, IChunk>(MAX_CHUNK_COUNT, OnChunkRemoved);
            _blocksRegistry = new BlockRegistry(true);
            _chunkGenerator = new FlatChunkGenerator(
                _blocksRegistry.GetBlockID(BlockRegistry.STONE_BLOCK),
                _blocksRegistry.GetBlockID(BlockRegistry.DIRT_BLOCK)
            );
        }
        
        private void Update()
        {
            var bottomLeftCorner = _camera.ViewportToWorldPoint(new Vector3(-1, -1, _camera.nearClipPlane));
            var topRightCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

            int minX = (int)bottomLeftCorner.x / 16;
            int minY = (int)bottomLeftCorner.y / 16;

            int maxX = (int)topRightCorner.x / 16;
            int maxY = (int)topRightCorner.y / 16;

            for(int y = minY-1; y <= maxY; ++y) 
            {
                for(int x = minX; x <= maxX; ++x) 
                {
                    var chunkPos = new Vector2Int(x, y);
                    if(_chunkCache.Get(chunkPos) == null)
                        InstantiateChunk(chunkPos);
                }
            }
        }

        private void InstantiateChunk(Vector2Int pos) 
        {
            IChunk chunk = new Chunk(new Vector2Int(pos.x, pos.y), _chunkGenerator);
            var chunkGameObject = Instantiate(_chunkPrefab, new Vector3(pos.x * 16, pos.y * 16, 1), Quaternion.Euler(0,0,0), this.transform);
            chunkGameObject.name = "Chunk(" + pos.x + ", " + pos.y + ")";
            
            var chunkObject = chunkGameObject.GetComponent<ChunkObject>();
            chunkObject.SetChunk(chunk);

            _chunkCache.Add(pos, chunk);
        }

        private void OnChunkRemoved(Vector2Int pos, IChunk chunk) 
        {
            var chunkObjects = GetComponentsInChildren<ChunkObject>();
            foreach(var chunkObject in chunkObjects) 
            {
                if(chunkObject.GetChunk() == chunk) 
                {
                    Destroy(chunkObject.gameObject);
                    return;
                }
            }
        }
    }

}