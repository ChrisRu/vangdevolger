using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Birds
{
    public class Enemy : Bird
    {
        private PathFinder _pathFinder;
        private Timer _timer;
        private int _moveTime;
        public int MoveTime
        {
            get { return _moveTime; }
            set
            {
                _moveTime = value;
                _timer.Interval = value;
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
            _timer.Start();
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
                _timer.Stop();
                MessageBox.Show("Trapped!");
            }
            else
            {
                if (Parent.Neighbors[(Direction) direction].Element is Player)
                {
                    _timer.Stop();
                    MessageBox.Show("Game Over!");
                }
                ChangeDirection((Direction)direction);
                Move((Direction)direction);
            }
        }
    }
}
