using UnityEngine;
using UnityEngine.Tilemaps;

namespace toe {

    public class ChunkObject : MonoBehaviour
    {

        private IChunk _chunk = null;
        private BlockRegistry _blockRegistry;

        [SerializeField]
        private Tilemap _tilemap;

        public void SetChunk(IChunk chunk) 
        {
            _chunk = chunk;
            _chunk.SetDirty(true);
        }
        public IChunk GetChunk() 
        {
            return _chunk;
        }

        private void Start() 
        {
            Camera.onPreRender += OnPreCameraRender;
            _blockRegistry = this.GetComponentInParent<World>().GetBlockRegistry();
        }

        private void OnPreCameraRender(Camera camera) 
        {
            if(_chunk != null && _chunk.IsDirty()) 
            {
                _tilemap.size = new Vector3Int(_chunk.Size.x, _chunk.Size.y, 0);
                _tilemap.tileAnchor = new Vector3Int(0,0,0);

                for(int y = 0; y < _tilemap.size.y; ++y) 
                {
                    for(int x = 0; x < _tilemap.size.x; ++x) 
                    {
                        Tile tile = _blockRegistry.GetBlockByID(_chunk.GetBlock(new Vector2Int(x, y))).GetTile();
                        _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                    }
                }

                _chunk.SetDirty(false);
            }
        }
    }

}