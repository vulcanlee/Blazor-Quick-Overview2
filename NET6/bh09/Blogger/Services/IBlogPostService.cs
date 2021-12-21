using Blogger.Models;

namespace Blogger.Services
{
    public interface IBlogPostService
    {
        Task DeleteAsync(BlogPost item);
        Task<List<BlogPost>> GetAsync();
        Task<BlogPost> GetAsync(int id);
        Task PostAsync(BlogPost item);
        Task PutAsync(BlogPost item);
    }
}
