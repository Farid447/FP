using Microsoft.AspNetCore.Http;

namespace FP.BL.Extentions;

public static class FileExtention
{
    public const string Type = "image";
    public const int Size = 1;
    public static bool IsValid(this IFormFile file)
    {
        if(!file.FileName.StartsWith(Type)) return false;
        if (file.Length > Size * 1024 * 1024) return false;
        return true;
    }

    public static async Task<string> UploadAsync(this IFormFile file, params string[] path)
    {
        string uploadpath = Path.Combine(path);
        if (!Path.Exists(uploadpath)) Directory.CreateDirectory(uploadpath);

        string filename = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute
            + DateTime.Now.Second + file.FileName;

        using (Stream stream = File.Create(Path.Combine(uploadpath, filename)))
        {
            await file.CopyToAsync(stream);
        }

        return filename;
    }

    public static void DeleteImage(this string imagename, params string[] path)
    {
        string deletepath = Path.Combine(path);
        if (System.IO.File.Exists(Path.Combine(deletepath, imagename)))
        {
            System.IO.File.Delete(Path.Combine(deletepath, imagename));
        }
    }
}
