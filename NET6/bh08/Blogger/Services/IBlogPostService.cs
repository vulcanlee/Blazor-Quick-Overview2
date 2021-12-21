using Blogger.Models;

namespace Blogger.Services
{
    public interface IBlogPostService
    {
        Task DeleteAsync(BlogPost item);
        Task<List<BlogPost>> GetAsync();
        Task PostAsync(BlogPost item);
        Task PutAsync(BlogPost item);
    }
}
