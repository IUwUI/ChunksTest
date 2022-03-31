using UnityEngine;

namespace toe {

    public class Chunk : IChunk
    {
        public const uint CHUNK_WIDTH = 16;
        public const uint CHUNK_HEIGHT = 16;

        private uint[,] _blocks;
        private Vector2Int _position;
        private bool _isDirty;

        public Vector2Int Position { get { return _position; } }
        public Vector2Int Size { get { return new Vector2Int((int)CHUNK_WIDTH, (int)CHUNK_HEIGHT); } }

        public Chunk(Vector2Int position) 
        {
            _position = position;
            _blocks = new uint[CHUNK_HEIGHT, CHUNK_WIDTH];
            _isDirty = true;
        }
        public Chunk(Vector2Int position, IChunkGenerator generator) 
        {
            _position = position;
            _blocks = new uint[CHUNK_HEIGHT, CHUNK_WIDTH];
            _isDirty = true;
            generator.Generate(Position, Size, ref _blocks);
        }

        public bool IsDirty() 
        {
            return _isDirty;
        }
        public void SetDirty(bool dirty) 
        {
            _isDirty = dirty;
        }

        public uint GetBlock(Vector2Int position) 
        {
            if(position.x < 0 || position.x > CHUNK_WIDTH || position.y < 0 || position.y >= CHUNK_HEIGHT) return 0;
            return _blocks[position.y, position.x];
        }
        public void SetBlock(Vector2Int position, uint blockID) 
        {
            if(position.x < 0 || position.x > CHUNK_WIDTH || position.y < 0 || position.y >= CHUNK_HEIGHT) return;
            
            if(_blocks[position.y, position.x] != blockID) 
            {
                _blocks[position.y, position.x] = blockID;
                _isDirty = true;
            }
        }
    }

}
