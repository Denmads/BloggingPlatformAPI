using BlogAPI.DTO;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogAPI.Repositories
{
    public class PostRepository(DatabaseContext databaseContext, ITagRepository tagRepository) : IPostRepository
    {
        public Post Get(int id) {
            return databaseContext.Posts
                .Include(p => p.Tags)
                .Where(post => post.Id == id).First();
        }

        public List<Post> GetAll() {
            return databaseContext.Posts
                .Include(p => p.Tags)
                .ToList();
        }

        public Post Insert(PostDto dto) {
            Post post = new Post {
                Title = dto.Title,
                Content = dto.Content,
                Category = dto.Category,
                Tags = dto.Tags
                    .Select(tagRepository.CreateOrGet)
                    .ToList()
            };

            databaseContext.Posts.Add(post);
            databaseContext.SaveChanges();

            return post;
        }

        public Post Update(int id, PostDto dto) {
            var post = Get(id);

            var oldTags = post.Tags;

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.Category = dto.Category;
            post.Tags = dto.Tags
                .Select(tagRepository.CreateOrGet)
                .ToList();

            databaseContext.SaveChanges();

            // Delete unused tags
            CheckTags(oldTags.ToList());

            return post;
        }

        public bool Exists(int id) {
            return databaseContext.Posts.Any(post => post.Id == id);
        }

        public void Delete(int id) {
            var post = Get(id);
            
            databaseContext.Posts.Remove(post);
            databaseContext.SaveChanges();

            CheckTags(post.Tags.ToList());
        }

        private void CheckTags(List<Tag> tags) {
            foreach (var tag in tags) {
                var postCount = databaseContext.Posts.Where(post => post.Tags.Contains(tag)).Count();

                if (postCount == 0) {
                    tagRepository.Delete(tag);
                }
            }
        }
    }
}
