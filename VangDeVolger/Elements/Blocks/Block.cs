using System.Drawing;

namespace VangDeVolger.Elements.Blocks
{
    public abstract class Block : Element
    {
        protected Image Image { get; set; }

        protected Block(Spot parent) : base(parent)
        {
            
        }
    }
}
