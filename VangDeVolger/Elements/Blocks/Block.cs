using System.Drawing;

namespace VangDeVolger.Elements.Blocks
{
    public abstract class Block : Element
    {
        protected Block(Spot parent) : base(parent)
        {

        }

        protected Image Image { get; set; }
    }
}
