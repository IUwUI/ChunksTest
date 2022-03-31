using UnityEngine.Tilemaps;

namespace toe {

    public class AirBlock : IBlock
    {
        public AirBlock() {}
        
        public string GetRegistryName() { return "air"; }
        public Tile GetTile() { return null; }
    }

}