using System.Collections.Generic;
using System.Drawing;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger.Elements
{
    public class Spot
    {
        public int Scale;
        public Element Element { get; set; }
        public Dictionary<Direction, Spot> Neighbors { get; set; } = new Dictionary<Direction, Spot>();

        // PathFinder Properties
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public Spot CameFromSpot { get; set; }

        /// <summary>
        /// Initialize Spot Class
        /// </summary>
        /// <param name="blockType"></param>
        /// <param name="scale"></param>
        public Spot(ElementType blockType, int scale)
        {
            Scale = scale;
            switch (blockType)
            {
                case ElementType.Solid:
                    Element = new BlockSolid(this);
                    break;
                case ElementType.Movable:
                    Element = new BlockMovable(this);
                    break;
                case ElementType.Player:
                    Element = new Player(this);
                    break;
                case ElementType.Enemy:
                    Element = new Enemy(this);
                    break;
                case ElementType.None:
                    Element = null;
                    break;
            }
        }
    }
}
