using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Articles
{
    public class ArticleInforRes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public JsonDocument SubTitle { get; set; }
        public JsonDocument Content { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
