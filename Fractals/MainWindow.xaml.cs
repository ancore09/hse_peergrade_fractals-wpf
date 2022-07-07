using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Переменные смещения по осям.
        /// </summary>
        double dx = 0, dy = 0;
        public MainWindow()
        {
            InitializeComponent();
            // Установка ограничений на размер окна.
            MinWidth = SystemParameters.PrimaryScreenWidth / 2;
            MinHeight = SystemParameters.PrimaryScreenHeight / 2;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MaxHeight = SystemParameters.PrimaryScreenHeight;
            // Костыль, чтобы произошло измерение параметров контрола Image.
            DrawingVisual drawing = new DrawingVisual();
            var bmp = new RenderTargetBitmap(1, 1, 0, 0, PixelFormats.Pbgra32);
            bmp.Render(drawing);
            img.Source = bmp;
        }

        /// <summary>
        /// Обработчик нажания кнопки Draw
        /// </summary>
        /// <param name="sender">Издатель события</param>
        /// <param name="e">Параметры события</param>
        private void drawBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxItem? item = fractalSelector.SelectedItem as ComboBoxItem;
                switch (item?.Content.ToString())
                {
                    case "Tree":
                        drawTree();
                        break;
                    case "Curve":
                        drawCurve();
                        break;
                    case "Carpet":
                        drawCarpet();
                        break;
                    case "Triangle":
                        drawTriangle();
                        break;
                    case "Cantor":
                        drawCantor();
                        break;
                }
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
        /// <summary>
        /// Метод получает необходимые параметры и запускает отрисовку фрактала
        /// </summary>
        private void drawTree()
        {
            img.Source = null;
            // Углы
            double leftAngle = leftAngleSlider.Value;
            double rightAngle = rightAngleSlider.Value;
            // Глубина рекурсии
            int depth = (int)depthSlider.Value;
            // Величина зума
            int zoom = (int)zoomSlider.Value;
            double rootLength = Height / 7;
            (new Tree(rootLength * zoom, 0.8, leftAngle, rightAngle, depth, dx, dy)).Draw(img);
        }
        /// <summary>
        /// Метод получает необходимые параметры и запускает отрисовку фрактала
        /// </summary>
        private void drawCurve()
        {
            // Очиска ссылки на предыдущую картинку фрактала (для работы сборщика мусора)
            img.Source = null;
            // Глубина рекурсии
            int depth = (int)depthSlider.Value;
            // Величина зума
            int zoom = (int)zoomSlider.Value;
            double rootLength = img.ActualWidth / 3;
            (new Curve(rootLength * zoom, depth, dx, dy)).Draw(img);
        }
        /// <summary>
        /// Метод получает необходимые параметры и запускает отрисовку фрактала
        /// </summary>
        private void drawCarpet()
        {
            img.Source = null;
            int depth = (int)depthSlider.Value;
            int zoom = (int)zoomSlider.Value;
            double rootLength = img.ActualWidth;
            (new Carpet(depth, rootLength * zoom, dx, dy)).Draw(img);
        }
        /// <summary>
        /// Метод получает необходимые параметры и запускает отрисовку фрактала
        /// </summary>
        void drawTriangle()
        {
            // Очиска ссылки на предыдущую картинку фрактала (для работы сборщика мусора)
            img.Source = null;
            // Глубина рекурсии
            int depth = (int)depthSlider.Value;
            // Величина зума
            int zoom = (int)zoomSlider.Value;
            double rootLength = Width / 2;
            (new Triangle(depth, rootLength * zoom, dx, dy)).Draw(img);
        }
        /// <summary>
        /// Метод получает необходимые параметры и запускает отрисовку фрактала
        /// </summary>
        void drawCantor()
        {
            // Очиска ссылки на предыдущую картинку фрактала (для работы сборщика мусора)
            img.Source = null;
            // Глубина рекурсии
            int depth = (int)depthSlider.Value;
            // Величина зума
            int zoom = (int)zoomSlider.Value;
            double rootLength = img.ActualWidth;
            double spacing = spaceSlider.Value;
            (new Cantor(depth, spacing, rootLength * zoom, dx, dy)).Draw(img);
        }
        /// <summary>
        /// ОБработчик выбора типа фрактала
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void fractalSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBoxItem)fractalSelector.SelectedItem).Content?.ToString())
            {
                case "Tree":
                    treeInt.Visibility = Visibility.Visible;
                    cantorInt.Visibility = Visibility.Collapsed;
                    depthSlider.Maximum = 15;
                    break;
                case "Curve":
                    treeInt.Visibility = Visibility.Collapsed;
                    cantorInt.Visibility = Visibility.Collapsed;
                    depthSlider.Maximum = 7;
                    break;
                case "Carpet":
                    treeInt.Visibility = Visibility.Collapsed;
                    cantorInt.Visibility = Visibility.Collapsed;
                    depthSlider.Maximum = 6;
                    break;
                case "Triangle":
                    treeInt.Visibility = Visibility.Collapsed;
                    cantorInt.Visibility = Visibility.Collapsed;
                    depthSlider.Maximum = 10;
                    break;
                case "Cantor":
                    treeInt.Visibility = Visibility.Collapsed;
                    cantorInt.Visibility = Visibility.Visible;
                    depthSlider.Maximum = 13;
                    break;
            }
            dx = dy = 0;
        }
        /// <summary>
        /// ОБработчик изменения размера окна
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //img.Width = Width * 2d / 3;
            //img.Height = Height;
            drawBtn_Click(null, null);
        }
        /// <summary>
        /// ОБработчик изменения значения слайдера
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void depthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            depthLabel.Content = "Depth: " + depthSlider.Value.ToString();
            drawBtn_Click(null, null);
        }
        /// <summary>
        /// ОБработчик изменения значения слайдера
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void leftAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            leftAngleLabel.Content = "Left Angle: " + leftAngleSlider.Value.ToString();
            drawBtn_Click(null, null);
        }
        /// <summary>
        /// ОБработчик изменения значения слайдера
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void rightAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            rightAngleLabel.Content = "Right Angle: " + rightAngleSlider.Value.ToString();
            drawBtn_Click(null, null);
        }
        /// <summary>
        /// ОБработчик изменения значения слайдера
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            zoomLabel.Content = "Zoom: " + zoomSlider.Value.ToString() + "x";
            drawBtn_Click(sender, null);
        }
        /// <summary>
        /// ОБработчик нажатия кнопки сохранения картинки
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем диалоговое окно
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG file (*.png)|*.png";
                if (saveFileDialog.ShowDialog() == true)
                {
                    var filePath = saveFileDialog.FileName;
                    // Сохраняем битмапу из Image в файл
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// ОБработчик нажатий кнопок движения фрактала
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Параметры события</param>
        private void moveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Увеличиваем смещение в зависимости от того, какая кнопка была нажата
            switch (((Button)sender).Name)
            {
                case "moveLeftBtn":
                    dx -= 10;
                    break;
                case "moveRightBtn":
                    dx += 10;
                    break;
                case "moveDownBtn":
                    dy += 10;
                    break;
                case "moveUpBtn":
                    dy -= 10;
                    break;
            }
            drawBtn_Click(sender, null);
        }
    }
}
