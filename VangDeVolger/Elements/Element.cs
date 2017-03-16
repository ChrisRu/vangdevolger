using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
        public const int Scale = 32;
        public int X;
        public int Y;
        public PictureBox Pb;

        /// <summary>
        /// Initialize Element Class
        /// </summary>
        /// <param name="x">Position X on grid</param>
        /// <param name="y">Position Y on grid</param>
        protected Element(int x, int y)
        {
            X = x;
            Y = y;
            Pb = new PictureBox
            {
                Size = new Size(Scale, Scale),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(x * Scale, y * Scale)
            };
        }

        /// <summary>
        /// Move towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public abstract bool Move(Direction direction);


        /// <summary>
        /// Convert Direction to X and Y Location
        /// </summary>
        /// <param name="x">Initial X Position</param>
        /// <param name="y">Initial Y Position</param>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Tuple with X and Y values</returns>
        public Coordinates DirectionToLocation(int x, int y, Direction? direction)
        {
            int newX = x;
            int newY = y;
            switch (direction)
            {
                case Direction.Up:
                    if (y > 0)
                    {
                        newY--;
                    }
                    break;
                case Direction.Down:
                    if (y < Level.Grid.GetLength(0) - 1)
                    {
                        newY++;
                    }
                    break;
                case Direction.Left:
                    if (x > 0)
                    {
                        newX--;
                    }
                    break;
                case Direction.Right:
                    if (x < Level.Grid.GetLength(1) - 1)
                    {
                        newX++;
                    }
                    break;
            }
            return new Coordinates(newX, newY);
        }
    }
}
