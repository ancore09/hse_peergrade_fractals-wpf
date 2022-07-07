using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fractals
{
    internal class Triangle : Fractal
    {
        /// <summary>
        /// Длина стороны
        /// </summary>
        double Length { get; set; }
        public Triangle(int depth, double length, double dx, double dy) : base(depth, dx, dy)
        {
            Length = length;
        }

        /// <summary>
        /// Переопределенный метод отрисовки
        /// </summary>
        /// <param name="image">Контрол, куда рендерить изображения</param>
        /// <param name="depth">Глубина фрактала</param>
        public override void Draw(Image image)
        {
            DrawTriangle(Depth, new Point(image.ActualWidth / 2 + dx, 20 + dy), new Point(image.ActualWidth / 2 - Length / 2 + dx, 20 + dy + Length * Math.Sqrt(3) / 2),
                new Point(image.ActualWidth / 2 + Length / 2 + dx, 20 + dy + Length * Math.Sqrt(3) / 2));
            base.Draw(image);
        }
        //top left right
        void DrawTriangle(int depth, Point p1, Point p2, Point p3)
        {
            if (depth == 0)
            {
                // Рисуем треугольник
                context.DrawLine(pen, p1, p2);
                context.DrawLine(pen, p1, p3);
                context.DrawLine(pen, p2, p3);
                return;
            }
            else
            {
                // Вычисление точек маленьких треугольников
                Point np1 = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                Point np2 = new Point((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2);
                Point np3 = new Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
                
                DrawTriangle(depth - 1, p1, np1, np2);
                DrawTriangle(depth - 1, np1, p2, np3);
                DrawTriangle(depth - 1, np2, np3, p3);
            }
        }
    }
}
