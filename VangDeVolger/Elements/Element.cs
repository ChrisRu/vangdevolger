using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
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
                Size = new Size(Level.Scaling, Level.Scaling),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(x * Level.Scaling, y * Level.Scaling)
            };
        }

        /// <summary>
        /// Move towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public abstract bool Move(Direction direction);
    }
}
