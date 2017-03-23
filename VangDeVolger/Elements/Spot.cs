using System.Collections.Generic;
using System.Drawing;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger.Elements
{
    public class Spot
    {
        public const int Scale = 32;
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
        public Spot(ElementType blockType)
        {
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

        /// <summary>
        /// Move from current spot to next spot
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        public void MoveElement(Direction direction)
        {
            int x = 0;
            int y = 0;
            switch (direction)
            {
                case Direction.Up:
                    y = -Scale;
                    break;
                case Direction.Down:
                    y = Scale;
                    break;
                case Direction.Left:
                    x = -Scale;
                    break;
                case Direction.Right:
                    x = Scale;
                    break;
            }

            // Move Picturebox
            Element.Pb.Location = Point.Add(Element.Pb.Location, new Size(x, y));
            // Move Element to Neighboor
            Neighbors[direction].Element = Element;
            // Set Element Parent to Neighboor
            Element.Parent = Neighbors[direction];
            // Set Element of spot to null
            Element = null;
        }
    }
}
