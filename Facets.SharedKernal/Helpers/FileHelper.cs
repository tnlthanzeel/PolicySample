namespace Facets.SharedKernal.Helpers;

public static class FileHelper
{
    public static string GetFileExtension(string fileName)
    {
        var extension = fileName.Split(".").Last();

        return $".{extension}";
    }
}
