using System;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    public class Enemy : Bird
    {
        private PathFinder _pathFinder;
        private Timer _timer;

        public int PrevTime;
        private int _moveTime;
        public int MoveTime
        {
            get { return _moveTime; }
            set
            {
                PrevTime = _moveTime;
                if (value == -1)
                {
                    _timer.Stop();
                }
                else
                {
                    _moveTime = value;
                    _timer.Interval = value;
                    _timer.Start();
                }
            }
        }

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
            _timer = new Timer
            {
                Interval = time
            };
            _timer.Tick += _moveAlongPath;
            MoveTime = time;
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
            _timer.Stop();

            DialogResult result = MessageBox.Show("You lose!\nStart a new game?", "Notification", MessageBoxButtons.YesNo);
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
