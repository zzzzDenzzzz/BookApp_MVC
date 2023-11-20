namespace WebApplication1.Helpers;

public static class FileUploadHelper
{
    public static async Task<string> UploadAsync(IFormFile? fromFile)
    {
        if (fromFile == null) throw new Exception("File was not upload");
        var filename = $"{Guid.NewGuid()}{Path.GetExtension(fromFile.FileName)}";
        await using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
        await fromFile.CopyToAsync(fs);
        return @$"/uploads/{filename}";
    }
}