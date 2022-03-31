using UnityEngine;
using UnityEngine.Tilemaps;

namespace toe {

    public class StoneBlock : IBlock
    {
        private Tile _tile;
        private Texture2D _texture;
        private Sprite _sprite;

        public StoneBlock() 
        {
            _texture = Resources.Load<Texture2D>("Textures/stone");
            _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0, 0), _texture.width);
            
            _tile = ScriptableObject.CreateInstance<Tile>();
            _tile.sprite = _sprite;
        }
        
        public string GetRegistryName() { return "stone"; }
        public Tile GetTile() { return _tile; }
    }

}