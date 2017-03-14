using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    internal class Player : Bird
    {
        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public Player(int x, int y, int scale) : base(x, y, scale)
        {
            ImageLeft = Properties.Resources.bird_green_left;
            ImageRight = Properties.Resources.bird_green_right;
            Pb.Image = ImageRight;
        }

        public override void Move(KeyEventArgs e)
        {

        }
    }
}
