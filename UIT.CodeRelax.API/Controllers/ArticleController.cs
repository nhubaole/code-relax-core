using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
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
            var article = await articleService.GetArticleAndQuizzesByIdAsync(ArticleId);

            if (article != null)
            {
                return ApiOK(article);
            }
            return NoContent();
        }

        ///// <summary>
        ///// Lấy thông tin article bằng id
        ///// </summary>
        ///// <param name="ArticleId">Id article</param>
        ///// <returns>Thông tin article</returns>
        //[HttpGet("{ArticleId}/with-quizzes")]
        //public async Task<IActionResult> GetArticleAndQuizzesById(int ArticleId)
        //{
        //    var article = await articleService.GetArticleAndQuizzesByIdAsync(ArticleId);

        //    if (article != null)
        //    {
        //        return ApiOK(article);
        //    }
        //    return NoContent();
        //}


        /// <summary>
        /// Lấy toàn bộ articles
        /// </summary>
        /// <returns>Danh sách articles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllArticles() {
            return ApiOK(await articleService.GetAllArticlesAsync());
        }

        /// <summary>
        /// Tạo mới articles
        /// </summary>
        /// <returns>Articlc vừa tạo</returns>
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody]ArticleInfoReq article)
        {
            return ApiOK (await articleService.AddArticleAsync(article));
        }


        /// <summary>
        /// update articles
        /// </summary>
        ///  <param name="ArticleId">Id article</param>
        /// <returns>Articlc vừa update</returns>
        [HttpPut("{ArticleId}")]
        public async Task<IActionResult> UpdateArticle(int ArticleId, [FromBody] ArticleInfoReq article)
        {
            if (ArticleId != article.Id)
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
        public async Task<IActionResult> DeleteArticle (int ArticleId)
        {
            return ApiOK(await articleService.DeleteArticleAsync(ArticleId));
        }  

    }
}
