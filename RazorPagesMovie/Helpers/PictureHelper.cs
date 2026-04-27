using static System.Net.Mime.MediaTypeNames;

namespace RazorPagesMovie.Helpers
{
    public static class PictureHelper
    {
        public static string UploadNewImage(IWebHostEnvironment environment,
        IFormFile file)
        {
            string guid = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(file.FileName);
            string shortPath = Path.Combine("images/Movies", guid + ext);
            string path = Path.Combine(environment.WebRootPath, shortPath);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            shortPath = "/" + shortPath;
            return shortPath;
        }
    }
}