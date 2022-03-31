using System.Collections.Generic;

namespace toe {

    public class BlockRegistry
    {
        public static readonly AirBlock AIR_BLOCK;
        public static readonly DirtBlock DIRT_BLOCK;
        public static readonly StoneBlock STONE_BLOCK;

        private List<IBlock> _blocks;

        static BlockRegistry() {
            AIR_BLOCK = new AirBlock();
            DIRT_BLOCK = new DirtBlock();
            STONE_BLOCK = new StoneBlock();
        }

        public BlockRegistry(bool registerDefaultBlocks=true) 
        {
            _blocks = new List<IBlock>();

            if(registerDefaultBlocks) {
                RegisterBlock(AIR_BLOCK);
                RegisterBlock(DIRT_BLOCK);
                RegisterBlock(STONE_BLOCK);
            }
        }

        public uint RegisterBlock(IBlock block) 
        {
            int index = _blocks.IndexOf(block);
            if(index < 0) 
            {
                _blocks.Add(block);
                return (uint)_blocks.Count - 1;
            }
            return (uint)index;
        }

        public uint GetBlockID(IBlock block) {
            int id = _blocks.IndexOf(block);
            if(id < 0) return 0;
            return (uint)id;
        }

        public IBlock GetBlockByID(uint id) 
        {
            if(id > _blocks.Count) return null;
            return _blocks[(int)id];
        }

        public IBlock GetBlockByName(string name) 
        {
            foreach(IBlock block in _blocks) 
            { 
                if(block.GetRegistryName() == name)
                    return block;
            }
            return null;
        }
        
    }
}