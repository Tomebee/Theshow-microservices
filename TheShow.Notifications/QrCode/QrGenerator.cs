using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace TheShow.Notifications.QrCode
{
    public interface IQrGenerator
    {
        string Generate(string content);
    }

    internal sealed class QrGenerator : IQrGenerator
    {
        private readonly QRCodeGenerator _qrGenerator;
        public QrGenerator()
        {
            _qrGenerator = new QRCodeGenerator();
        }

        public string Generate(string content)
        {
            var qrCodeInfo = _qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeInfo);
            var qrBitmap = qrCode.GetGraphic(60);
            var bitmapArray = ToByteArray(qrBitmap);
            return $"data:image/png;base64,{Convert.ToBase64String(bitmapArray)}";
        }

        private static byte[] ToByteArray(Bitmap bitmap)
        {
            using var ms = new MemoryStream();

            bitmap.Save(ms, ImageFormat.Png);

            return ms.ToArray();
        }
    }
}
