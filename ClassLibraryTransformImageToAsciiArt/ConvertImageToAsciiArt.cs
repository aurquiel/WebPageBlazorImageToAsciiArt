using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace ClassLibraryTransformImageToAsciiArt
{
    public class ConvertImageToAsciiArt : IConvertImageToAsciiArt
    {
        private readonly char[] ASCII_CHARACTERS_BY_SURFACE =
        {   '`', '^', '"', ',', ':', ';', 'I', 'l', '!', 'i', '~', '+', '_', '-', '?',
            ']', '[', '}', '{', '1', ')', '(', '|', '\\', '/', 't', 'f', 'j', 'r', 'x', 'n', 'u',
            'v', 'c', 'z', 'X', 'Y', 'U', 'J', 'C', 'L', 'Q', '0', 'O', 'Z', 'm', 'w', 'q', 'p',
            'd', 'b', 'k', 'h', 'a', 'o', '*', '#', 'M', 'W', '&', '8', '%', 'B', '@', '$'};


        public MemoryStream ConvertImageToAsciiArtStream(Stream imageStream)
        {
            System.Drawing.Image img = Bitmap.FromStream(imageStream);
            var asciiArtLines = ConvertOperation((Bitmap)img);
            return ReturnAsMemoryStream(asciiArtLines);
        }

        private List<string> ConvertOperation(Bitmap image)
        {
            List<string> asciiArtLines = new List<string>();
            Size size = image.Size;
            for (int y = 0; y < size.Height - 1; y++)
            {
                var line = "";
                for (int x = 0; x < size.Width - 1; x++)
                {
                    var px = image.GetPixel(x, y);
                    line += ConvertPixelToCharacter(px);
                }
                asciiArtLines.Add(line);
            }
            return asciiArtLines;
        }

        private char ConvertPixelToCharacter(Color pixel)
        {
            var pixel_brightness = pixel.R + pixel.G + pixel.B;
            var max_brightness = 255 * 3;
            var brightness_weight = ASCII_CHARACTERS_BY_SURFACE.Length / (float)max_brightness;
            var index = (int)(pixel_brightness * brightness_weight) - 1;
            return (index < 0) ? ASCII_CHARACTERS_BY_SURFACE[ASCII_CHARACTERS_BY_SURFACE.Length - 1] : ASCII_CHARACTERS_BY_SURFACE[index];
        }

        private MemoryStream ReturnAsMemoryStream(List<string> ascii_art)
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    foreach (var line in ascii_art)
                    {
                        tw.WriteLine(line);
                    }
                    tw.Flush();
                    ms.Position = 0;
                    bytes = ms.ToArray();
                }
            }

            return new MemoryStream(bytes);
        }
    }
}