using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fractals
{
    internal class Tree : Fractal
    {
        /// <summary>
        /// Отношение длин отрезков между итерациями
        /// </summary>
        double LenghtRatio { get; set; }
        /// <summary>
        /// Длина отрезка
        /// </summary>
        double Lenght { get; set; }
        /// <summary>
        /// Левый угол
        /// </summary>
        double LeftAngle { get; set; }
        /// <summary>
        /// Правый угол
        /// </summary>
        double RightAngle { get; set; }


        /*Color startColor;
        Color endColor;*/

        public Tree(double l, double ratio, double la, double ra, int depth, double dx, double dy) : base(depth, dx, dy)
        {
            LeftAngle = la;
            RightAngle = ra;
            LenghtRatio = ratio;
            Lenght = l;
            /*startColor = start;
            endColor = end;*/
        }

        /// <summary>
        /// Переопределенный метод отрисовки
        /// </summary>
        /// <param name="image">Контрол, куда рендерить изображения</param>
        /// <param name="depth">Глубина фрактала</param>
        public override void Draw(Image image)
        {
            DrawBranch(Depth, new Point(image.RenderTransformOrigin.X + image.ActualWidth / 2 + dx,
                image.RenderTransformOrigin.Y + image.ActualHeight / 1.2 + dy), Lenght, -Math.PI / 2);
            // Вызов закрытия и рендера
            base.Draw(image);
        }

        void DrawBranch(int depth, Point pt, double length, double angle)
        {
            double x1 = pt.X + length * Math.Cos(angle);
            double y1 = pt.Y + length * Math.Sin(angle);
            Point p2 = new Point(x1, y1);

            // Тут я пытался сделать градиент, промежуточные цвета считаются верно,
            // но ручка почему-то не применяется на рисунке(((
            /*Color tempColor;
            tempColor.R = (byte)((double)depth / Depth * endColor.R + (1 - (double)depth / Depth) * startColor.R);
            tempColor.G = (byte)((double)depth / Depth * endColor.G + (1 - (double)depth / Depth) * startColor.G);
            tempColor.B = (byte)((double)depth / Depth * endColor.B + (1 - (double)depth / Depth) * startColor.B);
            //Debug.WriteLine(Depth - depth + " " + tempColor.R + " " + tempColor.G + " " + tempColor.B);
            Pen p = new Pen(new SolidColorBrush(tempColor), 1);
            p.Freeze();*/

            // Рисуем линию
            context.DrawLine(pen, pt, p2);

            if (depth == 0)
            {
                return;
            }
            else
            {
                DrawBranch(depth - 1, new Point(x1, y1), length * LenghtRatio, angle + LeftAngle);
                DrawBranch(depth - 1, new Point(x1, y1), length * LenghtRatio, angle - RightAngle);
            }
        }

    }
}
