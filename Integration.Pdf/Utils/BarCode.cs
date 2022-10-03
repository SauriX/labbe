using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using ZXing;

namespace Integration.Pdf.Utils
{
    public class BarCode
    {
        public static byte[] Generate(string code, int width, int height, BarcodeFormat format = BarcodeFormat.ITF)
        {
            byte[] image;

            BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
            {
                Format = format,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                }
            };
            var pixelData = writer.Write(code);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.SetResolution(200F, 200F);

                    ImageConverter converter = new ImageConverter();
                    image = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                }
            }

            return image;
        }
    }
}