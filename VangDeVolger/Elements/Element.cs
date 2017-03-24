using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
        public Spot Parent { get; set; }
        public PictureBox Pb { get; set; }

        /// <summary>
        /// Initialize Element Class
        /// </summary>
        protected Element(Spot parent)
        {
            Parent = parent;
            Pb = new PictureBox
            {
                Size = new Size(parent.Scale, parent.Scale),
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        /// <summary>
        /// Move towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public abstract bool Move(Direction direction);

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="e"></param>
        public virtual void KeyDown(KeyEventArgs e)
        {
            return;
        }
    }
}
