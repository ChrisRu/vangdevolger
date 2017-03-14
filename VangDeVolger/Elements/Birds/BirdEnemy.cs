using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    internal class Enemy : Bird
    {
        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public Enemy(int x, int y, int scale) : base(x, y, scale)
        {
            ImageLeft = Properties.Resources.bird_red_left;
            ImageRight = Properties.Resources.bird_red_right;
            Pb.Image = ImageRight;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public override void Move(KeyEventArgs e)
        {

        }
    }
}
