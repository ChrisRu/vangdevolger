
namespace VangDeVolger.Elements.Birds
{
    using System;
    using System.Windows.Forms;

    public class Enemy : Bird
    {
        public Timer MoveTimer { get; set; }

        public int PrevTime { get; set; }

        private PathFinder _pathFinder { get; set; }

        /// <summary>
        /// Initialize new Enemy Class
        /// </summary>
        public Enemy()
        {
            this.ImageLeft = Properties.Resources.bird_red_left;
            this.ImageRight = Properties.Resources.bird_red_right;
            this.Pb.Image = this.ImageLeft;
            this.GoingRight = false;
            this._initMovement(500);
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public override bool CanMove(Direction direction)
        {
            this.ChangeDirection(direction);
            return false;
        }

        /// <summary>
        /// Initialize PathFinder Movement
        /// </summary>
        private void _initMovement(int time)
        {
            this._pathFinder = new PathFinder();
            this.MoveTimer = new Timer
            {
                Interval = time
            };
            this.MoveTimer.Tick += this._moveAlongPath;
            this.MoveTimer.Start();
        }

        /// <summary>
        /// Move Enemy Along 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _moveAlongPath(object sender, EventArgs e)
        {
            Direction? direction = _pathFinder.GetNextDirection(Parent, typeof(Player), true);
            if (direction == null)
            {
                _startNewGame("You won!\nStart a new game?", "Victory!");
            }
            else
            {
                if (Parent.Neighbors[(Direction) direction].Element is Player)
                {
                    _startNewGame("You lost!\nTry again?","Game Over");
                }
                ChangeDirection((Direction) direction);
                Move((Direction) direction);
            }
        }

        /// <summary>
        /// Ask to start new game or not
        /// </summary>
        private void _startNewGame(string message, string title)
        {
            MoveTimer.Stop();

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //start a new game
            }
            else
            {
                // close the game
                Application.Exit();
            }
        }
    }
}
