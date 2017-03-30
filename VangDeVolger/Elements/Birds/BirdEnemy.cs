using System;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    public class Enemy : Bird
    {
        private PathFinder _pathFinder;
        public Timer MoveTimer;

        public int PrevTime;

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        public Enemy()
        {
            ImageLeft = Properties.Resources.bird_red_left;
            ImageRight = Properties.Resources.bird_red_right;
            Pb.Image = ImageLeft;
            GoingRight = false;
            _initMovement(500);
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        public override bool CanMove(Direction direction)
        {
            ChangeDirection(direction);
            return false;
        }

        /// <summary>
        /// Initialize PathFinder Movement
        /// </summary>
        private void _initMovement(int time)
        {
            _pathFinder = new PathFinder();
            MoveTimer = new Timer
            {
                Interval = time
            };
            MoveTimer.Tick += _moveAlongPath;
            MoveTimer.Start();
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
                _startNewGame();
            }
            else
            {
                if (Parent.Neighbors[(Direction) direction].Element is Player)
                {
                    _startNewGame();
                }
                ChangeDirection((Direction) direction);
                Move((Direction) direction);
            }
        }

        /// <summary>
        /// Ask to start new game or not
        /// </summary>
        private void _startNewGame()
        {
            MoveTimer.Stop();

            DialogResult result = MessageBox.Show("You lose!\nStart a new game?", "Game Over", MessageBoxButtons.YesNo);
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
