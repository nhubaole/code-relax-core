using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Quiz;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class QuizController : ControllerExtensions
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            this._quizService = quizService;
        }

        /// <summary>
        /// Lấy danh sách tất cả quiz
        /// </summary>
        /// <returns>Danh sách các quiz</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizsAsync();
            return Ok(quizzes);
        }

        /// <summary>
        /// Lấy thông tin quiz bằng ID
        /// </summary>
        /// <param name="id">ID của quiz</param>
        /// <returns>Thông tin chi tiết của quiz</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        /// <summary>
        /// Tạo mới một quiz
        /// </summary>
        /// <param name="quizReq">Thông tin quiz cần tạo</param>
        /// <returns>Quiz vừa được tạo</returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizInforReq quizReq)
        {
            return ApiOK(await _quizService.AddQuizAsync(quizReq));
        }

        /// <summary>
        /// Cập nhật thông tin của quiz
        /// </summary>
        /// <param name="id">ID của quiz cần cập nhật</param>
        /// <param name="quizReq">Thông tin quiz mới</param>
        /// <returns>Không có nội dung trả về nếu thành công</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizInforReq quizReq)
        {
            var result = await _quizService.UpdateQuizAsync(id, quizReq);
            return ApiOK(result);
        }

        /// <summary>
        /// Xóa một quiz
        /// </summary>
        /// <param name="id">ID của quiz cần xóa</param>
        /// <returns>Không có nội dung trả về nếu thành công</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var result = await _quizService.DeleteQuizAsync(id);
            return ApiOK(result);
        }

    }
}
