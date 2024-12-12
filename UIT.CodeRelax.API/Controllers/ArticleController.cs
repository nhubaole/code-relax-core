using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticleController : ControllerExtensions
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        /// <summary>
        /// Lấy thông tin article bằng id
        /// </summary>
        /// <param name="ArticleId">Id article</param>
        /// <returns>Thông tin article</returns>
        [HttpGet("{ArticleId}")]
        public async Task<IActionResult> GetArticleById(int ArticleId) {
            var article = await articleService.GetArticleByIdAsync(ArticleId);

            if (article != null)
            {
                return ApiOK(article);
            }
            return NoContent();
        }

        /// <summary>
        /// Lấy toàn bộ articles
        /// </summary>
        /// <returns>Danh sách articles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllArticles() {
            return ApiOK(await articleService.GetAllArticlesAsync());
        }

        /// <summary>
        /// Lấy danh sách article của người dùng
        /// </summary>
        /// <param name="UserId">Id người dùng</param>
        /// <returns>Danh sách articles của người đó</returns>
        [HttpGet("article/{UserId}")]
        public async Task<IActionResult> GetArticlesByUSerId(int UserId)
        {
            return ApiOK(await articleService.GetArticleByUserId(UserId));
        }

        /// <summary>
        /// Tạo mới articles
        /// </summary>
        /// <returns>Articlc vừa tạo</returns>
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody]Article article)
        {
            await articleService.AddArticleAsync(article);
            return ApiOK (await articleService.GetArticleByIdAsync(article.Id));
        }


        /// <summary>
        /// update articles
        /// </summary>
        ///  <param name="ArticleId">Id article</param>
        /// <returns>Articlc vừa update</returns>
        [HttpPut("{ArticleId}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            return ApiOK(await articleService.UpdateArticleAsync(article));
        }

        /// <summary>
        /// Xóa một article
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpDelete("{ArticleId}")]
        public async Task<IActionResult> DeleteArticle (int id)
        {
            return ApiOK(await articleService.DeleteArticleAsync(id));
        }
        
    }
}
