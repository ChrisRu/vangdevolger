using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.PathFinding;

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
            set {
                _moveTime = value;
                _timer.Interval = value;
            }
        }

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        public Enemy(Spot parent) : base(parent)
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
            List<Spot> path = _pathFinder.GetOptimalPath(Parent);
            if (path.Count > 1)
            {
                // Move along path
                Direction? direction = Parent.Neighbors.FirstOrDefault(neighboor => neighboor.Value == path[1]).Key;
                if (direction != null)
                {
                    Move((Direction) direction);
                }
            }
            else
            {
                // Move randomly
                Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
                Direction randomDirection = directions[new Random().Next(0, directions.Length)];
                if (Parent.Neighbors[randomDirection]?.Element != null)
                {
                    Move(randomDirection);
                }
            }
        }
    }
}
