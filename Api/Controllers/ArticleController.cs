using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.DataAccess.Interface;
using Data.Entities;
using Api.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IEntityBaseRepository<Article> _articleRepository;
        private readonly IEntityBaseRepository<SubSection> _subSectionRepository;
        private readonly IEntityBaseRepository<Section> _sectionRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private string _basePath;

        public ArticleController(
            IEntityBaseRepository<Article> articleRepository,
            IEntityBaseRepository<SubSection> subSectionRepository,
            IEntityBaseRepository<Section> sectionRepository,
            IEntityBaseRepository<User> userRepository,
            IConfiguration configuration
            )
        {
            _articleRepository = articleRepository;
            _subSectionRepository = subSectionRepository;
            _sectionRepository = sectionRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _basePath = _configuration["BasePath"];
        }

        public IActionResult Index()
        {
            return View(_articleRepository.AllIncluding(x => x.SubSection, x => x.User).OrderBy(x => x.ModifiedDate).ToList().ConvertAll(ToArticleViewModel));
        }

        public IActionResult Create()
        {
            var model = new ArticleViewModel();
            LoadDropDownForView(model);
            return View(model);
        }

        public IActionResult Edit(
            int id)
        {
            var model = ToArticleViewModel(_articleRepository.GetSingle(id));
            LoadDropDownForView(model);
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(
            ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var section = _sectionRepository.GetSingle((int)model.SectionId).Name;
                var subsection = _subSectionRepository.GetSingle((int)model.SubSectionId).Name;
                var webPath = _configuration["WebPath"];
                var contentPath = $"{_configuration["ContentPath"]}/{section}/{subsection}/{model.Route}";
                var pathString = $"{webPath}/{contentPath}";
                var imagePathString = $"{pathString}/images";
                Directory.CreateDirectory(imagePathString);

                if (model.Images != null && model.Images.Any())
                {
                    UploadImages(model.Images, imagePathString);
                }

                if (model.File != null)
                {
                    var articlePath = WriteHtmlFileContent(model.File, pathString, imagePathString, contentPath);
                    model.Path = articlePath;
                }

                if (model.Id == 0)
                {
                    _articleRepository.Add(ToArticle(model));
                }
                else
                {
                    var article = _articleRepository.GetSingle(model.Id);
                    _articleRepository.Update(UpdateArticle(model, article));
                }

                _articleRepository.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                LoadDropDownForView(model);
                return View(model);
            }
        }

        public IActionResult Delete(
            int id)
        {
            _articleRepository.DeleteWhere(x => x.Id == id);
            _articleRepository.Commit();
            return RedirectToAction("Index");
        }

        private string WriteHtmlFileContent(
            IFormFile htmlFile,
            string pathString,
            string imagePathString,
            string contentPath)
        {
            var result = string.Empty;
            var htmlFileName = htmlFile.FileName.Split('\\').LastOrDefault();
            using (var reader = new StreamReader(htmlFile.OpenReadStream()))
            {
                result = reader.ReadToEnd();
            }

            var imgPath = $"{_configuration["BasePath"]}/{contentPath}/images";
            result = result.Replace("replaceText", imgPath);
            var data = string.IsNullOrEmpty(result) ? null : result.Split('\n').ToList();
            pathString = $"{pathString}/{htmlFileName}";
            System.IO.File.WriteAllLines(pathString, data);

            return $"{_configuration["BasePath"]}/{ contentPath}/{htmlFileName}";
        }

        private static void UploadImages(
            List<IFormFile> images,
            string imagePathString)
        {
            foreach (var image in images)
            {
                //Path.GetFileNameWithoutExtension(formFile.Name)
                var imageFileName = image.FileName.Split('\\').LastOrDefault();
                var filePath = $"{imagePathString}/{imageFileName}";

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    image.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        private void LoadDropDownForView(
            ArticleViewModel model)
        {
            model.SectionList = _sectionRepository.GetAll().OrderBy(y => y.Order).ToList().ConvertAll(ToDropDownViewModel);
            model.SubSectionList = _subSectionRepository.GetAll().OrderBy(y => y.Order).ToList().ConvertAll(ToDropDownViewModel);
            model.UserList = _userRepository.GetAll().Where(u => u.UserType == 1 && u.Active).OrderBy(y => y.Id).ToList().ConvertAll(ToDropDownViewModel);
        }

        private Article ToArticle(
            ArticleViewModel model)
        {
            return new Article
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Route = model.Route,
                Path = model.Path,
                SubSectionId = model.SubSectionId,
                AuthorId = model.AuthorId
            };
        }

        private Article UpdateArticle(
            ArticleViewModel model, Article article)
        {
            article.Name = model.Name;
            article.Description = model.Description;
            article.Route = model.Route;
            article.Path = (string.IsNullOrEmpty(model.Path)) ? article.Path : model.Path;
            article.SubSectionId = model.SubSectionId;
            article.AuthorId = model.AuthorId;
            return article;
        }

        private ArticleViewModel ToArticleViewModel(
            Article input)
        {
            return new ArticleViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Route = input.Route,
                SectionId = input.SubSection != null ? input.SubSection.SectionId : null,
                SubSectionId = input.SubSectionId,
                AuthorId = input.AuthorId,
                SubSection = input.SubSection != null ? input.SubSection.Name : string.Empty,
                Author = input.User != null ? input.User.Name : string.Empty,
                Path = input.Path,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate
            };
        }
    }
}