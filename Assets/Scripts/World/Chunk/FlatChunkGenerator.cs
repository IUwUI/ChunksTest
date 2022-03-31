using UnityEngine;

namespace toe {

    public class FlatChunkGenerator : IChunkGenerator
    {
        const uint EARTH_LEVEL = 50;
        const uint STONE_LEVEL = 40;

        private uint _stoneID;
        private uint _dirtID;

        public FlatChunkGenerator(uint stoneID, uint dirtID) 
        {
            _stoneID = stoneID;
            _dirtID = dirtID;
        }

        public void Generate(Vector2Int position, Vector2Int size, ref uint[,] blocks) 
        {
            for(int y = 0; y < size.y; ++y) 
            {
                for(int x = 0; x < size.x; ++x) 
                {
                    int height = position.y * 16 + y;

                    if(height > EARTH_LEVEL)
                        blocks[y,x] = 0;
                    else if(height <= EARTH_LEVEL && height > STONE_LEVEL)
                        blocks[y,x] = _dirtID;
                    else 
                        blocks[y,x] = _stoneID;
                }
            }
        }
    }

}