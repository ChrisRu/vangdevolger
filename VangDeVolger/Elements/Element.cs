using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
        public const int Scale = 32;
        public PictureBox Pb { get; set; }

        /// <summary>
        /// Initialize Element Class
        /// </summary>
        protected Element()
        {
            this.Pb = new PictureBox
            {
                Size = new Size(Scale, Scale),
                SizeMode = PictureBoxSizeMode.Zoom
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
