using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    class SpringStair : Stair
    {
        Timer jumpTimer;

        public SpringStair(int y) : base(y)
        {
            jumpTimer = new Timer();
            jumpTimer.Interval = 10;
            jumpTimer.Tick += new EventHandler(Jump);
            jumpTimer.Start();
        }

        public override void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.HotPink), x, y, w, h);

            Pen pen = new Pen(Color.Black, 5);
            pen.EndCap = LineCap.ArrowAnchor;

            G.DrawLine(pen, x + w / 2, y + h - 3, x + w / 2, y);
        }

        public override void Rise()
        {
            base.Rise();

            if (Touch())
                jumpTimer.Start();

            int distance = Math.Abs(y - (player.y + player.size));
            int range = 50;

            if (distance > range || player.y < Form1.backgroundY)
            {
                jumpTimer.Stop();

                lifeChanged = false;
            }
        }

        void Jump(object sender, EventArgs e)
        {
            int jumpDistance = player.fallSpeed + 10;

            player.y -= jumpDistance;
        }
    }
}