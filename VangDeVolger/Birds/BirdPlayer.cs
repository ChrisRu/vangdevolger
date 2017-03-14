using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        public PlayerBird(Point position) : base(position)
        {
            ImageLeft = Properties.Resources.bird_green_left;
            ImageRight = Properties.Resources.bird_green_right;
        }

        public override void Move(KeyEventArgs e)
        {

        }
    }
}
