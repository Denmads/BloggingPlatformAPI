using BlogAPI.Models;

namespace BlogAPI.Repositories
{
    public class TagRepository(DatabaseContext databaseContext) : ITagRepository
    {
        public Tag CreateOrGet(string name) {
            Tag? tag = databaseContext.Tags.Where(t => t.Name == name).FirstOrDefault();

            if (tag != null)
                return tag;

            tag = new Tag { Name = name };

            databaseContext.Tags.Add(tag);
            databaseContext.SaveChanges();

            return tag;
        }

        public void Delete(Tag tag) {
            databaseContext.Tags.Remove(tag);
            databaseContext.SaveChanges();
        }
    }
}
