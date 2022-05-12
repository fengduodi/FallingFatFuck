using System;
using System.Drawing;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    class MagicStair : Stair
    {
        Timer disappearTimer;

        public MagicStair(int y) : base(y)
        {
            disappearTimer = new Timer();
            disappearTimer.Interval = 10;
            disappearTimer.Tick += new EventHandler(Disappear);
        }

        public override void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.SkyBlue), x, y, w, h);
        }

        public override void Rise()
        {
            base.Rise();

            if (Touch())
                disappearTimer.Start();
        }

        void Disappear(object sender, EventArgs e)
        {
            x++;
            y++;

            w -= 2;
            h -= 2;

            if (w == 0)
            {
                Remove();

                disappearTimer.Stop();
            }
        }
    }
}