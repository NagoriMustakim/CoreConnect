using LinkwayAPI.Constants.API;
using LinkwayAPI.Repository.Interfaces;

namespace LinkwayAPI.Repository
{
    public class FileRepository : IFileRepository
    {
        private IWebHostEnvironment _environment;
        public FileRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                var path = Path.Combine(contentPath, FileConstant.UPLOADS);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { FileConstant.PNG, FileConstant.JPG, FileConstant.JPEG, FileConstant.JFIF,FileConstant.GIF };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format(FileConstant.ONLY_EXTENSIONS_ALLOWED, string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return Tuple.Create(1, newFileName);
            }
            catch
            {
                return Tuple.Create(0, FileConstant.ERROR_SAVING_IMAGE);
            }
        }

        public Tuple<int, string> EditImage(IFormFile imageFile, string currentImageName)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                var path = Path.Combine(contentPath, FileConstant.UPLOADS);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (currentImageName != null && currentImageName != "null")
                {
                    var currentImagePath = Path.Combine(path, currentImageName);

                    if (!System.IO.File.Exists(currentImagePath))
                    {
                        return SaveImage(imageFile);
                    }

                    var ext = Path.GetExtension(imageFile.FileName);
                    var allowedExtensions = new string[] { FileConstant.PNG, FileConstant.JPG, FileConstant.JPEG };

                    if (!allowedExtensions.Contains(ext))
                    {
                        string msg = string.Format(FileConstant.ONLY_EXTENSIONS_ALLOWED, string.Join(",", allowedExtensions));
                        return new Tuple<int, string>(0, msg);
                    }

                    string newFileName;
                    if (currentImageName == currentImageName.ToLower() + ext.ToLower())
                    {
                        //replace file with same name
                        System.IO.File.Delete(currentImagePath);
                        newFileName = currentImageName;
                        var fileWithPath = Path.Combine(path, newFileName);
                        var stream = new FileStream(fileWithPath, FileMode.Create);
                        imageFile.CopyTo(stream);
                        stream.Close();
                    }
                    else
                    {
                        //generate new name
                        System.IO.File.Delete(currentImagePath);
                        string uniqueString = Guid.NewGuid().ToString();
                        newFileName = uniqueString + ext;
                        var fileWithPath = Path.Combine(path, newFileName);
                        var stream = new FileStream(fileWithPath, FileMode.Create);
                        imageFile.CopyTo(stream);
                        stream.Close();
                    }
                    return Tuple.Create(1, newFileName);
                }
                else
                {
                    return SaveImage(imageFile);
                }
            }
            catch
            {
                return Tuple.Create(0, FileConstant.ERROR_UPDATE_IMAGE);
            }
        }

        public bool Deleteimage(string? imageFileName)
        {
            try
            {
                if (imageFileName == null)
                    return false;

                var wwwPath = _environment.ContentRootPath;
                var path = Path.Combine(wwwPath, FileConstant.UPLOADS_FOLDER, imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
