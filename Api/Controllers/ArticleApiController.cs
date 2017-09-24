using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.DataAccess.Interface;
using Data.Entities;
using Api.Dto;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ArticleApi")]
    public class ArticleApiController : BaseApiController
    {
        private readonly IEntityBaseRepository<Article> _articleRepository;

        public ArticleApiController(
            IEntityBaseRepository<Article> articleRepository
            )
        {
            _articleRepository = articleRepository;
        }

        [HttpGet("GetDetail/{id}")]
        public IActionResult GetDetail(
            int id)
        {
            var result = ToArticleDto(_articleRepository.AllIncluding(x => x.User, x => x.Comments)
                                            .FirstOrDefault(x => x.Id == id));
            return new OkObjectResult(result);
        }

        [HttpPost("PostComment")]
        public IActionResult PostComment(
            UserComment userComment)
        {
            return Ok();
        }

        private ArticleDto ToArticleDto(
            Article input)
        {
            return new ArticleDto
            {
                Id = input.Id,
                Name = input.Name,
                Author = input.User.Name,
                UserAvatar = input.User.UserAvatar,
                Path = input.Path,
                CreatedDate = input.CreatedDate != null ? input.CreatedDate.Value.ToString("dd-MM-yy") : string.Empty,
                UserComments = input.Comments.ToList().ConvertAll(ToUserCommentDto)
            };
        }

        private UserComment ToUserCommentDto(
            Comment input)
        {
            return new UserComment
            {
                Id = input.Id,
                Comment = input.UserComment,
                UserEmail = input.User.Email,
                UserName = input.User.Name
            };
        }
    }
}