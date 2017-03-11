using System.Collections.Generic;
using System.Xml.Schema;
using VangDeVolger.Blocks;

namespace VangDeVolger.PathFinding
{
    internal class PathFinderBlock
    {
        public Block Block;
        public PathFinderBlock CameFrom = null;
        public List<PathFinderBlock> SiblingBlocks;

        public int F;
        public int G;
        public int H;

        public PathFinderBlock(Block block)
        {
            Block = block;
            F = 0;
            G = 0;
            H = 0;
        }
    }
}
