﻿using System.Drawing;

namespace VangDeVolger.Elements.Blocks
{
    public abstract class Block : Element
    {
        protected Image Image;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        protected Block(int x, int y, int scale) : base(x, y, scale)
        {

        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public abstract bool Touch(Direction direction);
    }
}
