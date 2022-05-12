using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    public class Stair
    {
        public int x, y, w, h;

        protected int riseSpeed;

        protected Player player;

        protected bool lifeChanged;

        public Stair(int y)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            w = 100;
            h = 20;

            x = random.Next(Form1.backgroundX, Form1.backgroundX + Form1.backgroundWidth - w + 1);
            this.y = y;

            riseSpeed = 3;

            player = Form1.player;

            lifeChanged = false;
        }

        public virtual void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.Blue), x, y, w, h);
        }

        public virtual void Rise()
        {
            y -= riseSpeed;

            if (Touch())
            {
                player.y = y - player.size;

                if (!lifeChanged && player.life < 12)
                {
                    player.life++;

                    lifeChanged = true;
                }
            }

            if (y < 0)
                Remove();
        }

        protected bool Touch()
        {
            int extraTouchRange = 15;

            return player.x + player.size / 2 > x - extraTouchRange && player.x + player.size / 2 < x + w + extraTouchRange &&
                   player.y + player.size >= y && player.y + player.size < y + h;
        }

        protected void Remove()
        {
            List<Stair> stairs = Form1.stairs;

            stairs.Remove(this);

            stairs.Add(Form1.CreateStair(stairs[stairs.Count - 1].y + 100));

            Form1.score++;
        }
    }
}