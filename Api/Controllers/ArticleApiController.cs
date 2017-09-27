using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.DataAccess.Interface;
using Data.Entities;
using Api.Dto;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ArticleApi")]
    public class ArticleApiController : BaseApiController
    {
        private readonly IEntityBaseRepository<Article> _articleRepository;
        private readonly IEntityBaseRepository<Comment> _commentRepository;

        public ArticleApiController(
            IEntityBaseRepository<Article> articleRepository,
            IEntityBaseRepository<Comment> commentRepository
            )
        {
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet("GetDetail/{id}")]
        public IActionResult GetDetail(
            int id)
        {
            var article = _articleRepository.AllIncluding(x => x.User)
                                            .FirstOrDefault(x => x.Id == id);
            var result = ToArticleDto(article);
            article.UserView++;
            _articleRepository.Update(article);
            _articleRepository.Commit();
            return new OkObjectResult(result);
        }

        [HttpPost("PostComment")]
        public IActionResult PostComment(
            UserComment userComment)
        {
            var commentEntity = ToUserCommentEntity(userComment);
            _commentRepository.Add(commentEntity);
            _commentRepository.Commit();
            return Ok();
        }

        private Comment ToUserCommentEntity(UserComment userComment)
        {
            return new Comment
            {
                ArticleId = userComment.ArticleId,
                UserComment = userComment.Comment,
                User = new User
                {
                    Email = userComment.UserEmail,
                    Name = userComment.UserName
                }
            };
        }

        [HttpPost("UpdateLike")]
        public IActionResult UpdateLike(
            )
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
                UserComments = GetUserCommentDto(input.Id).ToList(),
                LikeCount = input.LikeCount,
                DislikeCount = input.DislikeCount
            };
        }

        private IEnumerable<UserComment> GetUserCommentDto(
            int articleId)
        {
            var comments = _commentRepository.AllIncluding(x => x.ArticleId == articleId, x => x.User);
            foreach (var comment in comments)
            {
                yield return new UserComment
                {
                    Id = comment.Id,
                    Comment = comment.UserComment,
                    UserEmail = comment.User.Email,
                    UserName = comment.User.Name,
                    Date = comment.CreatedDate != null ? comment.CreatedDate.Value.ToString("dd-MM-yy") : string.Empty
                };
            }
        }
    }
}