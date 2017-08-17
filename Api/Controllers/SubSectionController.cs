using Data.DataAccess.Interface;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    public class SubSectionController : Controller
    {
        private readonly IEntityBaseRepository<SubSection> _subSectionRepository;

        public SubSectionController(IEntityBaseRepository<SubSection> subSectionRepository)
        {
            _subSectionRepository = subSectionRepository;
        }

        public IActionResult Index()
        {
            return View(_subSectionRepository.GetAll().OrderBy(x => x.Order));
        }

        public IActionResult Create()
        {
            return View(new Section());
        }

        public IActionResult Edit(int id)
        {
            var model = _subSectionRepository.GetSingle(id);
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(SubSection model)
        {
            if (model.Id == 0)
                _subSectionRepository.Add(model);
            else
                _subSectionRepository.Update(model);
            _subSectionRepository.Commit();
            return RedirectToAction("Index");
        }
    }
}