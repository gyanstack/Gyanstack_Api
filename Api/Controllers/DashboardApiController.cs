using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.DataAccess.Interface;
using Data.Entities;
using Api.Dto;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DashboardApiController : BaseApiController
    {
        private readonly IEntityBaseRepository<Article> _articleRepository;
        private readonly IEntityBaseRepository<SubSection> _subSectionRepository;
        private readonly IEntityBaseRepository<Section> _sectionRepository;

        public DashboardApiController(
            IEntityBaseRepository<Article> articleRepository,
            IEntityBaseRepository<SubSection> subSectionRepository,
            IEntityBaseRepository<Section> sectionRepository
            )
        {
            _articleRepository = articleRepository;
            _subSectionRepository = subSectionRepository;
            _sectionRepository = sectionRepository;
        }

        [HttpGet("GetRecent/{count}")]
        public IActionResult GetRecent(
            int count)
        {
            var result = _articleRepository.AllIncluding(x => x.SubSection, x => x.SubSection.Section, x => x.User)
                                            .OrderByDescending(x => x.ModifiedDate).Take(count).ToList().ConvertAll(ToArticleDto);
            return new OkObjectResult(result);
        }

        [HttpGet("GetMostViewed/{count}")]
        public IActionResult GetMostViewed(
            int count)
        {
            var result = _articleRepository.AllIncluding(x => x.SubSection, x=>x.SubSection.Section, x => x.User)
                                            .OrderByDescending(x => x.UserView).Take(count).ToList().ConvertAll(ToArticleDto);
            return new OkObjectResult(result);
        }

        [HttpGet("GetList/{name}")]
        public IActionResult GetList(
            string name)
        {
            var result = _articleRepository.AllIncluding(x => x.SubSection, x => x.SubSection.Section, x => x.User)
                                            .Where(x => x.SubSection.Name.Contains(name)).OrderByDescending(x => x.ModifiedDate).ToList().ConvertAll(ToArticleDto);
            return new OkObjectResult(result);
        }

        private ArticleDto ToArticleDto(
            Article input)
        {
            return new ArticleDto
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Section = input.SubSection.Section.Name,
                Subsection = input.SubSection.Name,
                SubsectionId = input.SubSectionId,
                Author = input.User.Name,
                Route = input.Route,
                CreatedDate = input.CreatedDate != null ? input.CreatedDate.Value.ToString("dd-MM-yy") : string.Empty
            };
        }
    }
}