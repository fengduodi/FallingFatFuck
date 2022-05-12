using System.Drawing;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    class PinStair : Stair
    {
        public PinStair(int y) : base(y) { }

        public override void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.Black), x, y, w, h);

            Point[] pins = new Point[11];

            for (int i = 0; i < pins.Length; i++)
            {
                pins[i].X = x + w / (pins.Length - 1) * i;

                if (i % 2 == 0)
                    pins[i].Y = y;
                else
                    pins[i].Y = y - 10;
            }

            G.FillPolygon(new SolidBrush(Color.Black), pins);
        }

        public override void Rise()
        {
            y -= riseSpeed;

            if (Touch())
            {
                player.y = y - player.size;

                if (!lifeChanged)
                {
                    player.life -= 4;

                    lifeChanged = true;
                }
            }

            if (y < 0)
                Remove();
        }
    }
}