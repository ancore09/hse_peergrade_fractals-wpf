using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Fractals
{
    internal class Carpet : Fractal
    {
        /// <summary>
        /// Длина строны
        /// </summary>
        double Length { get; set; }
        /// <summary>
        /// Легендарная картинка
        /// </summary>
        BitmapImage? rickImage;
        public Carpet(int depth, double length, double dx, double dy) : base(depth, dx, dy)
        {
            Length = length;
            // Загружаем картинку
            try
            {
                rickImage = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/rick.jpg"));
                rickImage.Freeze();
            }
            catch (Exception ex)
            {
                rickImage = null;
            }

        }

        /// <summary>
        /// Переопределенный метод отрисовки
        /// </summary>
        /// <param name="image">Контрол, куда рендерить изображения</param>
        /// <param name="depth">Глубина фрактала</param>
        public override void Draw(Image image)
        {
            if (rickImage != null)
            {
                Rect rect = new Rect(0+dx, 0+dy, image.ActualWidth, image.ActualHeight);
                context.DrawImage(rickImage, rect);
            }
            drawRectangle(Depth, Length, Length, new Point(0 + dx, 0 + dy));
            base.Draw(image);
        }

        void drawRectangle(int depth, double w, double h, Point rectOrigin)
        {
            if (depth == 0)
            {
                return;
            }
            else
            {
                // Просчитываем координаты для следующих квадратов
                double width = w / 3;
                double x1 = rectOrigin.X + width;
                double x2 = rectOrigin.X + 2 * width;

                double height = h / 3;
                double y1 = rectOrigin.Y + height;
                double y2 = rectOrigin.Y + 2 * height;

                Rect rect = new Rect(x1, y1, width, height);

                if (rickImage != null)
                {
                    context.DrawImage(rickImage, rect);
                }
                else
                {
                    context.DrawRectangle(Brushes.Blue, null, rect);
                }

                // Вызываем следующий шаг рекурсии
                drawRectangle(depth - 1, width, height, rectOrigin);
                drawRectangle(depth - 1, width, height, new Point(x1, rectOrigin.Y));
                drawRectangle(depth - 1, width, height, new Point(x2, rectOrigin.Y));
                drawRectangle(depth - 1, width, height, new Point(rectOrigin.X, y1));
                drawRectangle(depth - 1, width, height, new Point(rectOrigin.X, y2));
                drawRectangle(depth - 1, width, height, new Point(x2, y1));
                drawRectangle(depth - 1, width, height, new Point(x1, y2));
                drawRectangle(depth - 1, width, height, new Point(x2, y2));
            }
        }
    }
}
