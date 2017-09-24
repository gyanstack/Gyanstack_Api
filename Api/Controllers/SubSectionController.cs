using Api.Models;
using Data.DataAccess.Interface;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    public class SubSectionController : BaseController
    {
        private readonly IEntityBaseRepository<SubSection> _subSectionRepository;
        private readonly IEntityBaseRepository<Section> _sectionRepository;

        public SubSectionController(
            IEntityBaseRepository<SubSection> subSectionRepository,
            IEntityBaseRepository<Section> sectionRepository)
        {
            _subSectionRepository = subSectionRepository;
            _sectionRepository = sectionRepository;
        }

        public IActionResult Index()
        {
            return View(_subSectionRepository.AllIncluding(x => x.Section).OrderBy(x => x.Order).ToList().ConvertAll(ToSubSectionViewModel));
        }

        public IActionResult Create()
        {
            return View(new SubSectionViewModel
            {
                SectionList = _sectionRepository.GetAll().OrderBy(y => y.Order).ToList().ConvertAll(ToDropDownViewModel),
            });
        }

        public IActionResult Edit(
            int id)
        {
            var model = ToSubSectionViewModel(_subSectionRepository.GetSingle(id));
            model.SectionList = _sectionRepository.GetAll().OrderBy(y => y.Order).Select(y => new DropdownViewModel
            {
                Id = y.Id,
                Description = y.Description,
                Name = y.Name
            }).ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(
            SubSectionViewModel model)
        {
            if (model.Id == 0)
                _subSectionRepository.Add(ToSubSection(model));
            else
                _subSectionRepository.Update(ToSubSection(model));
            _subSectionRepository.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(
            int id)
        {
            _subSectionRepository.DeleteWhere(x => x.Id == id);
            _subSectionRepository.Commit();
            return RedirectToAction("Index");
        }

        private SubSectionViewModel ToSubSectionViewModel(
            SubSection input)
        {
            return new SubSectionViewModel
            {
                Active = input.Active,
                Description = input.Description,
                Name = input.Name,
                Id = input.Id,
                SectionId = input.SectionId,
                Order = input.Order,
                Section = input.Section != null ? input.Section.Name : string.Empty,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate
            };
        }

        private SubSection ToSubSection(
            SubSectionViewModel input)
        {
            return new SubSection
            {
                Active = input.Active,
                Description = input.Description,
                Name = input.Name,
                Id = input.Id,
                Order = input.Order,
                SectionId = input.SectionId
            };
        }
    }
}