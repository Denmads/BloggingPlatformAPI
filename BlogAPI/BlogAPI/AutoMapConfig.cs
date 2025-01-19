using AutoMapper;
using BlogAPI.Dto;
using BlogAPI.Models;

namespace BlogAPI
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Post, PostResponseDto>()
                .ForMember(
                post => post.Tags, 
                m => m.MapFrom(
                    post => post.Tags
                        .Select(tag => tag.Name)
                        .ToList()
                    )
                );
        });
    }
}
