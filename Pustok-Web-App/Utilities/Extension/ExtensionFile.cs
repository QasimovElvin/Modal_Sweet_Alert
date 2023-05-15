using Microsoft.AspNetCore.Mvc;

namespace Pustok_Web_App.Utilities.Extension;

public static class ExtensionFile
{
    public static bool CheckType(this IFormFile file,string filetype)
    {
        return  file.ContentType.Contains(filetype);
    }
    public static bool CheckSizeFile(this IFormFile file,int size)
    {
        return file.Length/1024>size;
    }
    public static async Task<string> SaveFileAsync(this IFormFile file,string root,string folder)
    {
        string UniqueFileName=Guid.NewGuid().ToString()+"_"+ file.FileName;
        string path=Path.Combine(root,folder,UniqueFileName);
        FileStream stream=new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        return UniqueFileName;
    }
}
