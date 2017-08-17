using Data.DataAccess.Interface;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    public class SectionController : Controller
    {
        private readonly IEntityBaseRepository<Section> _sectionRepository;

        public SectionController(IEntityBaseRepository<Section> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public IActionResult Index()
        {
            return View(_sectionRepository.GetAll().OrderBy(x => x.Order));
        }

        public IActionResult Create()
        {
            return View(new Section());
        }

        public IActionResult Edit(int id)
        {
            var model = _sectionRepository.GetSingle(id);
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Create(Section model)
        {
            if (model.Id == 0)
                _sectionRepository.Add(model);
            else
                _sectionRepository.Update(model);
            _sectionRepository.Commit();
            return RedirectToAction("Index");
        }
    }
}