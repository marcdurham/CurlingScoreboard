using System.Drawing;

namespace CurlingScoreboard
{
    public class Board
    {
        public const int DefaultWidth = 1000;
        public const int DefaultHeight = 300;
        public const int FontDivider = 12;

        readonly Graphics graphics;
        readonly Image image;

        public Board(int width = DefaultWidth, int height = DefaultHeight)
        {
            Width = height * 5;
            Height = height;
            image = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(image);
            
            var region = new Region(new Rectangle(0, 0, Width, Height));
            graphics.FillRegion(Brushes.LightGray, region);

            DrawNumberScale();
            DrawTeam(1, "Yellow", Brushes.Yellow);
            DrawTeam(-1, "Red", Brushes.Red);
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public void GenerateImage(string v)
        {
            DrawBox(6, 1, value: 3);
            DrawBox(15, 1, value: 13);
            DrawBox(14, 1, value: 14);
            DrawBox(14, -1, value: 13);
            DrawBox(13, -1, value: 13);
            DrawBox(3, -1, value: 2);
            DrawH(-2);

            image.Save(v);
        }

        void DrawNumberScale()
        {
            Font font = new(FontFamily.GenericSansSerif, Height / FontDivider, FontStyle.Regular);
            StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            for (int p = 1; p <= 15; p++)
            {
                int x = PositionX(p);
                int y = PositionY(0);
                var layout = new RectangleF(x, y, BoxHeight(), BoxHeight());
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

            var font = new Font(FontFamily.GenericSansSerif, Height / FontDivider, FontStyle.Regular);
            int x = PositionX(position);
            int y = PositionY(row);
            graphics.DrawRectangle(Pens.Black, x, y, BoxHeight(), BoxHeight());
            var layout = new RectangleF(x, y, BoxHeight(), BoxHeight());
          
            graphics.DrawString($"{value}", font, Brushes.Black, layout, format);
        }

        void DrawH(int row)
        {
            StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var font = new Font(FontFamily.GenericSansSerif, Height / FontDivider, FontStyle.Regular);
            int x = BoxHeight();
            int y = PositionY(row);
            graphics.DrawRectangle(Pens.Black, x, y, BoxHeight(), BoxHeight());
            var layout = new RectangleF(x, y, BoxHeight(), BoxHeight());

            graphics.DrawString($"H", font, Brushes.Black, layout, format);
        }

        void DrawTeam(int row, string name, Brush brush)
        {
            StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };

            var font = new Font(FontFamily.GenericSansSerif, Height / FontDivider, FontStyle.Regular);
            int x = BoxHeight();
            int y = PositionY(row);
            graphics.DrawRectangle(Pens.Black, x, y, BoxHeight()*3, BoxHeight());
            var layout = new RectangleF(x, y, BoxHeight() * 3, BoxHeight());

            graphics.DrawString($"{name}", font, brush, layout, format);
        }

        int PositionX(int position)
        {
            return (BoxHeight() * 4) + position * (int)(BoxHeight() * 1.1);
        }

        int PositionY(int row)
        {
            return (int)(Height / 2) - (int)(row * BoxHeight()) - (int)(BoxHeight() / 2);
        }

        int BoxHeight()
        {
            return (int)(Height / 5);
        }
    }
}
