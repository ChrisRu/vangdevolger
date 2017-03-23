using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Elements.Birds;
using VangDeVolger.Elements.Blocks;

namespace VangDeVolger.Elements
{
    public class Spot
    {
        public const int Scale = 32;
        public Element Element { get; set; }
        public Dictionary<Direction, Spot> Neighbors { get; set; }

        public Spot(ElementType blockType)
        {
            Neighbors = new Dictionary<Direction, Spot>();

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
        /// <param name="nextSpot">Spot to move to</param>
        /// <param name="direction">Direction of movement</param>
        public void MoveTo(Direction direction)
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

            this.Element.Pb.Location = Point.Add(this.Element.Pb.Location, new Size(x, y));
            Neighbors[direction].Element = this.Element;
            this.Element = null;          
        }
    }
}
