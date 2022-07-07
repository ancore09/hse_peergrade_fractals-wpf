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
    internal class Fractal
    {
        /// <summary>
        /// Рисунок фрактала
        /// </summary>
        protected DrawingVisual drawing;
        /// <summary>
        /// Виртуальный контекст для рисования
        /// </summary>
        protected DrawingContext context;
        /// <summary>
        /// Ручка для обводки прямых
        /// </summary>
        protected Pen pen = new Pen(Brushes.Blue, 1);
        /// <summary>
        /// Ручка для обводки множества кантора
        /// </summary>
        protected Pen penCantor = new Pen(Brushes.Blue, 4);
        /// <summary>
        /// Глубина рекурсии
        /// </summary>
        public int Depth { get; init; }
        /// <summary>
        /// Переменные сдвига
        /// </summary>
        protected double dx, dy;
        public Fractal(int depth, double dx, double dy)
        {
            this.dx = dx;
            this.dy = dy;
            // Заморозка ручки, чтобы wpf не обрабатывал ее ивенты
            pen.Freeze();
            //penCantor.Freeze();
            Depth = depth;
            drawing = new DrawingVisual();
            context = drawing.RenderOpen();
        }
        /// <summary>
        /// Базовый метод отрисовки (по факту закрывает контекст и рендерит картинку)
        /// </summary>
        /// <param name="image">Контрол, в который рендерить</param>
        /// <param name="depth">Глубина фрактала</param>
        virtual public void Draw(Image image) {
            context.Close();
            var bmp = new RenderTargetBitmap((int)image.ActualWidth, (int)image.ActualHeight, 0, 0, PixelFormats.Pbgra32);
            bmp.Render(drawing);
            image.Source = bmp;
        }
    }
}
