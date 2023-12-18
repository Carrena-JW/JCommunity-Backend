namespace JCommunity.AppCore.Core.Utils;

public static class DetectImage
{
    public static bool IsImage(this IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        var imageFormat = Image.DetectFormat(stream);

        return imageFormat != null;
    }

    public static bool IsImage(string path)
    {
        using Stream stream = new FileStream(path, FileMode.Open);

        var imageFormat = Image.DetectFormat(stream);

        return imageFormat != null;
    }

    public static bool IsImage(Stream stream)
    {
        var imageFormat = Image.DetectFormat(stream);

        return imageFormat != null;
    }
}
