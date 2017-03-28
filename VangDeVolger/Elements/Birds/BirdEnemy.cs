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
            set
            {
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
            List<Spot> path = _pathFinder.GetOptimalPath(Parent, typeof(Player));
            // Move Along Path
            if (path != null)
            {
                Direction direction = path[0].Neighbors.FirstOrDefault(x => x.Value == path[1]).Key;
                if (Parent.Neighbors[direction].Element is Player)
                {
                    MessageBox.Show("Game Over");
                }
                Move(direction);
            }
            // No path
            else
            {
                List<Direction> directions = new List<Direction>(Parent.Neighbors.Keys.Where(x => Parent.Neighbors[x].Element == null));
                // Move randomly
                if (directions.Count > 0)
                {
                    Direction randomDirection = directions.OrderBy(x => Guid.NewGuid()).First();
                    Move(randomDirection);
                }
                // Trapped
                else
                {
                    MessageBox.Show("Trapped");
                }
            }
        }
    }
}
