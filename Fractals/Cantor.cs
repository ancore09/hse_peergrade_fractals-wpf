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
    internal class Cantor : Fractal
    {
        /// <summary>
        /// Расстояние между отрезками разных шагов рекурсии
        /// </summary>
        double spaceBetwenIters = 20;
        /// <summary>
        /// Длина отрезка
        /// </summary>
        double Length { get; set; }
        public Cantor(int depth, double spaceBetwenIters, double length, double dx, double dy) : base(depth, dx, dy)
        {
            this.spaceBetwenIters = spaceBetwenIters;
            this.Length = length;
        }

        public override void Draw(Image image)
        {
            // Меняем пропорции для "зума"
            spaceBetwenIters *= Length / image.ActualWidth;
            penCantor.Thickness = Length / image.ActualWidth;

            DrawIter(Depth, new Point(0 + dx, 10+dy),  Length);
            base.Draw(image);
        }

        void DrawIter(int depth, Point p, double length)
        {
            if (depth == 0)
            {
                return;
            } else
            {
                context.DrawLine(penCantor, p, new Point(p.X+length,p.Y));
                DrawIter(depth - 1, new Point(p.X, p.Y+spaceBetwenIters), length/ 3);
                DrawIter(depth - 1, new Point(p.X + length * 2d / 3, p.Y+spaceBetwenIters), length/3);
            }
        }
    }
}
