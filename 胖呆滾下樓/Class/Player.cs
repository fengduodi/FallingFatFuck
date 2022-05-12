using System.Drawing;
using System.Windows.Forms;

namespace 胖呆滾下樓.Class
{
    public class Player
    {
        public int x, y, size;

        public int fallSpeed;

        int degree;

        bool clockwise;

        public bool left, right;

        public int life;

        Image playerImage, lifeImage;

        int lifeImageSize;

        public Player()
        {
            size = 50;

            x = Form1.windowsWidth / 2 - size / 2;
            y = Form1.backgroundY + 50;

            fallSpeed = 3;

            degree = 0;

            clockwise = true;

            left = false;
            right = false;

            life = 12;

            playerImage = Image.FromFile("image\\player.png");
            playerImage = new Bitmap(playerImage, new Size(size, size));

            lifeImageSize = 30;
            lifeImage = Image.FromFile("image\\life.png");
            lifeImage = new Bitmap(lifeImage, new Size(lifeImageSize, lifeImageSize));
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.TranslateTransform(x + size / 2, y + size / 2);

            G.RotateTransform(degree);

            G.DrawImage(playerImage, -size / 2, -size / 2);

            G.ResetTransform();
        }

        public void ShowLife(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            for (int i = 0; i < life; i++)
                G.DrawImage(lifeImage, Form1.backgroundX + i * lifeImageSize, 10);
        }

        public void Fall()
        {
            int turnSpeed = 10;

            if (clockwise)
                degree = (degree + turnSpeed) % 360;
            else
                degree = (degree - turnSpeed) % 360;

            y += fallSpeed;
        }

        public void Move()
        {
            int moveSpeed = 5, turnSpeed = 30;

            if (left)
            {
                x -= moveSpeed;

                if (x < Form1.backgroundX)
                    x = Form1.backgroundX;

                degree -= turnSpeed;

                clockwise = false;
            }

            if (right)
            {
                x += moveSpeed;

                if (x + size > Form1.backgroundX + Form1.backgroundWidth)
                    x = Form1.backgroundX + Form1.backgroundWidth - size;

                degree += turnSpeed;

                clockwise = true;
            }
        }
    }
}