using UnityEngine.Tilemaps;

namespace toe {

    public interface IBlock
    {
        string GetRegistryName();
        Tile GetTile();
    }

}