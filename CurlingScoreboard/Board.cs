using System.Drawing;

namespace CurlingScoreboard
{
    public class Board
    {
        public const int DefaultWidth = 1000;
        public const int DefaultHeight = 300;

        readonly Graphics graphics;
        readonly Image image;

        public Board(int width = DefaultWidth, int height = DefaultHeight)
        {
            Width = width;
            Height = height;
            image = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(image);
            
            var region = new Region(new Rectangle(0, 0, Width, Height));
            graphics.FillRegion(Brushes.LightGray, region);

            DrawNumberScale();
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public void GenerateImage(string v)
        {
            DrawBox(6, 1, 3);
            DrawBox(15, 1, 13);
            DrawBox(3, -1, 2);

            image.Save(v);
        }

        void DrawNumberScale()
        {
            Font font = new(FontFamily.GenericSansSerif, 12.0f, FontStyle.Regular);
            StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            for (int p = 1; p <= 15; p++)
            {
                int x = PositionX(p);
                int y = PositionY(0);
                var layout = new RectangleF(x, y, 30, 30);
                graphics.DrawString($"{p}", font, Brushes.Black, layout, format);
            }
        }

        void DrawBox(int position, int row, int value)
        {
            StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var font = new Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Regular);
            int x = PositionX(position);
            int y = PositionY(row);
            graphics.DrawRectangle(Pens.Blue, x, y, 30, 30);
            var layout = new RectangleF(x, y, 30, 30);
            //graphics.DrawString($"{value}", font, Brushes.Blue, x, y);
          
            graphics.DrawString($"{value}", font, Brushes.Blue, layout, format);
        }

        int PositionX(int position)
        {
            return 50 + position * 30;
        }

        int PositionY(int row)
        {
            return Height / 2 - (row * 35);
        }
    }
}
