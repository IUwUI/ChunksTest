using UnityEngine;

namespace toe {

    public interface IChunk 
    {
        Vector2Int Position { get; }
        Vector2Int Size { get; }

        bool IsDirty();
        void SetDirty(bool dirty);

        uint GetBlock(Vector2Int position);
        void SetBlock(Vector2Int position, uint blockID);
    }

}