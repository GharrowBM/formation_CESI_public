namespace TP04.Services
{
    public class UploadService
    {
        public string Upload(IFormFile file)
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "avatars"));
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Environment.CurrentDirectory, "avatars", fileName);
            Stream stream = System.IO.File.Create(path);
            file.CopyTo(stream);
            stream.Close();

            return "avatars/" + fileName;
        }
    }
}
