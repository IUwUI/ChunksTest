using UnityEngine;
using UnityEngine.Tilemaps;

namespace toe {

    public class DirtBlock : IBlock
    {
        private Tile _tile;
        private Texture2D _texture;
        private Sprite _sprite;

        public DirtBlock() 
        {
            _texture = Resources.Load<Texture2D>("Textures/dirt");
            _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0, 0), _texture.width);
            
            _tile = ScriptableObject.CreateInstance<Tile>();
            _tile.sprite = _sprite;
        }
        
        public string GetRegistryName() { return "dirt"; }
        public Tile GetTile() { return _tile; }
    }

}