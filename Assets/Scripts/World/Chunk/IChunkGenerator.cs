using UnityEngine;

namespace toe {

    public interface IChunkGenerator
    {
        void Generate(Vector2Int position, Vector2Int size, ref uint[,] blocks);
    }

}