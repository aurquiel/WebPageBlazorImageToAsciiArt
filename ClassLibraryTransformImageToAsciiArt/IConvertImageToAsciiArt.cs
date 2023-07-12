namespace ClassLibraryTransformImageToAsciiArt
{
    public interface IConvertImageToAsciiArt
    {
        MemoryStream ConvertImageToAsciiArtStream(Stream imageStream);
    }
}