using System.Threading.Tasks;
namespace DDDCleanArchStarter.Domain.Interfaces
{
    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64);
    }
}