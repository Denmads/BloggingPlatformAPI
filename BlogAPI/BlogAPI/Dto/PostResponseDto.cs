﻿using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Dto
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
