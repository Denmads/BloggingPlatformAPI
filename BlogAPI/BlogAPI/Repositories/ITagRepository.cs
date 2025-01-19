using BlogAPI.Models;

namespace BlogAPI.Repositories
{
    public interface ITagRepository
    {
        public Tag CreateOrGet(string name);
        public void Delete(Tag tag);
    }
}
