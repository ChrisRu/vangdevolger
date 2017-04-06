
namespace VangDeVolger.Elements.Birds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Execute on Game End to push an event to the Game Class
    /// </summary>
    /// <param name="victory">Has won</param>
    public delegate void GameEnd(bool victory);

    /// <summary>
    /// Enemy Class to follow Player in Grid
    /// </summary>
    public class Enemy : Bird
    {
        public Timer MoveTimer { get; set; }

        public event GameEnd GameEnd;

        private PathFinder _pathFinder { get; set; }

        private int _freezeTime { get; set; }

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
            this._freezeTime = 3000;
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
        /// Freeze Enemy for 500 milliseconds
        /// </summary>
        public void Freeze()
        {
            this.MoveTimer.Stop();
            this.Pb.Image = Properties.Resources.bird_frozen;
            Timer timer = new Timer { Interval = this._freezeTime };
            timer.Tick += (s, e) =>
            {
                this.MoveTimer.Start();
                this.Pb.Image = this.GoingRight ? this.ImageRight : this.ImageLeft;
                timer.Stop();
            };
            timer.Start();
        }

        /// <summary>
        /// Initialize PathFinder Movement
        /// </summary>
        /// <param name="time">Interval of enemy move</param>
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
        /// <param name="sender">Timer</param>
        /// <param name="e">Event Arguments</param>
        private void _moveAlongPath(object sender, EventArgs e)
        {
            Direction? nullDirection = this._pathFinder.GetNextDirection(this.Parent, typeof(Player), true);
            if (nullDirection == null)
            {
                this.MoveTimer.Stop();
                this.GameEnd(true);
            }
            else
            {
                Direction direction = (Direction)nullDirection;
                if (this.Parent.Neighbours[direction].Element is Player)
                {
                    this.MoveTimer.Stop();
                    this.GameEnd(false);
                }
                this.ChangeDirection(direction);

                if (this.Parent.Neighbours[direction].Element == null)
                {
                    this.Move(direction);
                }
                else
                {
                    List<Direction> directions = new List<Direction>(this.Parent.Neighbours.Keys.Where(key => this.Parent.Neighbours[key].Element == null));
                    if (directions.Count > 0)
                    {
                        this.Move(directions.OrderBy(x => Guid.NewGuid()).First());
                    }
                }
            }
        }
    }
}
