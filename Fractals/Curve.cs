using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Fractals
{
    internal class Curve : Fractal
    {
        /// <summary>
        /// Отношение длин отрезков
        /// </summary>
        double distanceScale = 1.0 / 3;
        /// <summary>
        /// Углы отклонения отрезков на каждой итерации
        /// </summary>
        double[] angles = new double[4] { 0, Math.PI / 3, -2 * Math.PI / 3, Math.PI / 3 };
        /// <summary>
        /// Длина отрезка
        /// </summary>
        double Lenght { get; set; }

        public Curve(double l, int depth, double dx, double dy) : base(depth, dx, dy)
        {
            Lenght = l;
        }

        /// <summary>
        /// Переопределенный метод отрисовки
        /// </summary>
        /// <param name="image">Контрол, куда рендерить изображения</param>
        /// <param name="depth">Глубина фрактала</param>
        public override void Draw(Image image)
        {
            DrawCurve(Depth, 0, Lenght, new Point(image.RenderTransformOrigin.X + dx,
                image.RenderTransformOrigin.Y + image.ActualHeight / 2.5 + dy), new Point(0, 0));
            // Вызов закрытия и рендера
            base.Draw(image);
        }

        void DrawCurve(int depth, double angle, double l, Point p1, Point p2)
        {
            if (depth == 0)
            {
                context.DrawLine(pen, p1, p2);
            }
            else
            {
                Point p = new Point(p1.X, p1.Y);
                Point prev = p1;
                for (int i = 0; i < 4; i++)
                {
                    // Меняем угол
                    angle += angles[i];
                    // вычисляем новую точку
                    p = new Point(p.X + l * Math.Cos(angle), p.Y + l * Math.Sin(angle));
                    // Вызываем следующий шаг рекурсии
                    DrawCurve(depth - 1, angle, l * distanceScale, prev, p);
                    // Текущий отрзок становится предыдущим
                    prev = p;
                }
            }
        }
    }
}
