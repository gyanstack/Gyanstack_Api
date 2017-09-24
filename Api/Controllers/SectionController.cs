using Api.Models;
using Data.DataAccess.Interface;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    public class SectionController : BaseController
    {
        private readonly IEntityBaseRepository<Section> _sectionRepository;

        public SectionController(IEntityBaseRepository<Section> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public IActionResult Index()
        {
            return View(_sectionRepository.GetAll().OrderBy(x => x.Order).ToList().ConvertAll(ToSectionViewModel));
        }

        public IActionResult Create()
        {
            return View(new SectionViewModel());
        }

        public IActionResult Edit(int id)
        {
            var model = _sectionRepository.GetSingle(id);
            return View("Create", ToSectionViewModel(model));
        }

        [HttpPost]
        public IActionResult Create(SectionViewModel model)
        {
            if (model.Id == 0)
                _sectionRepository.Add(ToSection(model));
            else
                _sectionRepository.Update(ToSection(model));
            _sectionRepository.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _sectionRepository.DeleteWhere(x => x.Id == id);
            _sectionRepository.Commit();
            return RedirectToAction("Index");
        }

        private SectionViewModel ToSectionViewModel(Section input)
        {
            return new SectionViewModel
            {
                Active = input.Active,
                Description = input.Description,
                Name = input.Name,
                Id = input.Id,
                Order = input.Order,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate
            };
        }

        private Section ToSection(SectionViewModel input)
        {
            return new Section
            {
                Active = input.Active,
                Description = input.Description,
                Name = input.Name,
                Id = input.Id,
                Order = input.Order
            };
        }
    }
}