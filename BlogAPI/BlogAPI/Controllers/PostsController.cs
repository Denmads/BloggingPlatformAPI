using AutoMapper;
using BlogAPI.Dto;
using BlogAPI.DTO;
using BlogAPI.Models;
using BlogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PostsController(IPostRepository postRepository) : ControllerBase {

        private IMapper mapper = AutoMapperConfig.Configuration.CreateMapper();

        [HttpPost]
        public ActionResult<PostResponseDto> Post([FromBody] PostDto data) {
            var post = postRepository.Insert(data);
            return StatusCode(201, mapper.Map<PostResponseDto>(post));
        }

        [HttpPut, Route("{id}")]
        public ActionResult<PostResponseDto> Put(int id, [FromBody] PostDto data) {
            if (!postRepository.Exists(id))
                return StatusCode(404);

            var post = postRepository.Update(id, data);
            return StatusCode(200, mapper.Map<PostResponseDto>(post));
        }

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id) {
            if (!postRepository.Exists(id))
                return StatusCode(404);

            postRepository.Delete(id);
            return StatusCode(204);
        }

        [HttpGet, Route("{id}")]
        public ActionResult<PostResponseDto> Get(int id) {
            if (!postRepository.Exists(id))
                return StatusCode(404);

            return StatusCode(204, mapper.Map<PostResponseDto>(postRepository.Get(id)));
        }

        [HttpGet]
        public ActionResult<PostResponseDto> GetAll([FromQuery] string term = "") {
            var posts = postRepository.GetAll();

            if (!string.IsNullOrEmpty(term)) {
                posts = posts
                    .Where(
                        post => post.Title.Contains(term) || post.Content.Contains(term) || post.Category.Contains(term))
                .ToList();
            }

            var responseList = posts.Select(mapper.Map<PostResponseDto>).ToList();

            return StatusCode(200, responseList);
        }
    }
}
