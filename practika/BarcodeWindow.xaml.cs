using System.Windows;
using ZXing;
using ZXing.Common;
using System.Drawing; // System.Drawing.Common
using System.Windows.Media.Imaging;
using System.IO;

namespace practika
{
    public partial class BarcodeWindow : Window
    {
        public BarcodeWindow(string barcodeData)
        {
            InitializeComponent();
            GenerateBarcode(barcodeData);
        }

        private void GenerateBarcode(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128, // Выбираем формат штрихкода (Code 128, QR_CODE, etc.)
                Options = new EncodingOptions
                {
                    Height = 200, //  размеры
                    Width = 250,
                    Margin = 10
                }
            };

            var bitmap = writer.Write(data);  // Создаем Bitmap
            imgBarcode.Source = ConvertBitmap(bitmap); // Отображаем в Image
        }

        // Метод для преобразования Bitmap в BitmapSource (для WPF)
        private BitmapSource ConvertBitmap(Bitmap bitmap)
        {
            // ИСПРАВЛЕНО: Используем Point и Size
            var rect = new Rectangle(new System.Drawing.Point(0, 0), new System.Drawing.Size(bitmap.Width, bitmap.Height));
            var bitmapData = bitmap.LockBits(
                rect,
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                System.Windows.Media.PixelFormats.Bgr24, //  Bgr32, Bgra32, Pbgra32 - в зависимости от данных!
                null,
                bitmapData.Scan0,
                bitmapData.Stride * bitmapData.Height,
                bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }
    }
}