using Blogger.Models;

namespace Blogger.Services
{
    public interface IMyUserService
    {
        Task DeleteAsync(MyUser item);
        Task<List<MyUser>> GetAsync();
        Task PostAsync(MyUser item);
        Task PutAsync(MyUser item);
    }
}
