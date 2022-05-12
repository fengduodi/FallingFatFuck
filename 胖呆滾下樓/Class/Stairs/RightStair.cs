using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    class RightStair : Stair
    {
        public RightStair(int y) : base(y) { }

        public override void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.Lime), x, y, w, h);

            Pen pen = new Pen(Color.Black, 5);
            pen.EndCap = LineCap.ArrowAnchor;

            G.DrawLine(pen, x + 15, y + h / 2, x + w - 15, y + h / 2);
        }

        public override void Rise()
        {
            base.Rise();

            if (Touch())
            {
                player.x += 3;

                if (player.x < Form1.backgroundX)
                    player.x = Form1.backgroundX;
            }
        }
    }
}