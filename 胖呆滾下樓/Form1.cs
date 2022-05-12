using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 胖呆滾下樓.Class;

namespace 胖呆滾下樓
{
    public partial class Form1 : Form
    {
        public static int windowsWidth, windowsHeight;

        public static int backgroundWidth, backgroundHeight;

        public static int backgroundX, backgroundY;

        public static Player player;

        public static List<Stair> stairs;

        public static int score;

        Timer updateTimer;

        public Form1()
        {
            InitializeComponent();

            Size = new Size(600, 800);

            int bias = 15;
            windowsWidth = Width - bias;
            windowsHeight = Height - SystemInformation.ToolWindowCaptionHeight - bias;

            backgroundWidth = 500;
            backgroundHeight = 680;

            backgroundX = windowsWidth / 2 - backgroundWidth / 2;
            backgroundY = 50;

            Setting();

            updateTimer = new Timer();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Run);
            updateTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.White), backgroundX, backgroundY, backgroundWidth, backgroundHeight);

            player.Show(e);

            foreach (var stair in stairs)
                stair.Show(e);

            Point[] pins = new Point[51];

            for (int i = 0; i < pins.Length; i++)
            {
                pins[i].X = backgroundX + backgroundWidth / (pins.Length - 1) * i;

                if (i % 2 == 0)
                    pins[i].Y = backgroundY;
                else
                    pins[i].Y = backgroundY + 20;
            }

            G.FillPolygon(new SolidBrush(Color.Black), pins);

            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(192, 255, 192));

            G.FillRectangle(solidBrush, backgroundX, 0, backgroundWidth, backgroundY);
            G.FillRectangle(solidBrush, 0, backgroundY + backgroundHeight, windowsWidth, windowsHeight - (backgroundY + backgroundHeight));

            player.ShowLife(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                player.left = false;

            if (e.KeyCode == Keys.Right)
                player.right = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                player.left = true;

            if (e.KeyCode == Keys.Right)
                player.right = true;
        }

        void Run(object sender, EventArgs e)
        {
            player.Fall();

            player.Move();

            foreach (var stair in stairs.ToArray())
                stair.Rise();

            if (player.y < backgroundY)
            {
                player.y += 30;

                player.life -= 4;
            }

            if (IsGameOver())
                JumpGameOverDialog();

            Invalidate();
        }

        public static Stair CreateStair(int y)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            int i = random.Next(0, 7);

            switch (i)
            {
                case 1:
                    return new LeftStair(y);
                case 2:
                    return new RightStair(y);
                case 3:
                    return new SpringStair(y);
                case 4:
                    return new MagicStair(y);
                case 5:
                    return new PinStair(y);
            }

            return new Stair(y);
        }

        void Setting()
        {
            player = new Player();

            stairs = new List<Stair>();

            for (int i = 0; i < 10; i++)
                stairs.Add(CreateStair((i + 1) * 100));

            score = 0;
        }

        bool IsGameOver()
        {
            return player.life < 1 || player.y > backgroundY + backgroundHeight;
        }

        void JumpGameOverDialog()
        {
            updateTimer.Stop();

            DialogResult dialogResult = MessageBox.Show("分數：" + score + "\n\n再玩一次？",
                                                        "遊戲結束",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Setting();

                updateTimer.Start();
            }
            else if (dialogResult == DialogResult.No)
                Environment.Exit(0);
        }
    }
}