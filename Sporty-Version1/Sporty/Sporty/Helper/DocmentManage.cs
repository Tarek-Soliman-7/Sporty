namespace Sporty.Helper
{
    public static class DocmentManage
    {
        static List<string> _allowedExtensions = [".jpg", ".png"];
        static int MaxSize = 2 * 1024 * 1024; // 2 MB
        //upload
        public static string? Upload(IFormFile file)
        {
            // 1- Cheack Extenstion

            //var Extension = Path.GetExtension(file.FileName);
            // 2- Cheack Size
            if (file.Length > MaxSize) return null;

            // 3- Get The Folder Locater Path 
            //wwwroot/Files/Images

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Image");

            //4-Make Attachment Unique Using Guid
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5-Get The Full Path
            var FullPath = Path.Combine(FolderPath, fileName);//Full path
            // 6-create File  Stream To Copy The File[Umanged]
            using var stream = new FileStream(FullPath, FileMode.Create);

                //7-use Stream to Copy the file
                file.CopyTo(stream);
            //8-Return The File Path
            return fileName;


        }

        public static void Delete(string FileName)
        {
            var Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Image", FileName);
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}
