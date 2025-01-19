using BlogAPI.DTO;
using BlogAPI.Models;

namespace BlogAPI.Repositories
{
    public interface IPostRepository
    {
        public Post Get(int id);
        public List<Post> GetAll();
        public Post Insert(PostDto dto);
        public Post Update(int id, PostDto dto);
        public bool Exists(int id);
        public void Delete(int id);
    }
}
