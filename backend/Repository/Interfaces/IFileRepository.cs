namespace LinkwayAPI.Repository.Interfaces
{
    public interface IFileRepository
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        Tuple<int, string> EditImage(IFormFile imageFile, string currentImageName);
        bool Deleteimage(string imageFileName);
    }
}
