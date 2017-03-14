using System.Drawing;

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
        protected Block(int x, int y) : base(x, y)
        {

        }
    }
}
