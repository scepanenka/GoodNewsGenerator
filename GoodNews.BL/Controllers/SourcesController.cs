using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodNews.DAL.Entities;
using GoodNews.DAL.UnitOfWork;

namespace GoodNews.BL.Controllers
{
    public class SourcesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SourcesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Sources.GetAllAsync());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = _unitOfWork.Sources.GetById(id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Source source)
        {
            if (ModelState.IsValid)
            {
                source.Id = Guid.NewGuid();
                _unitOfWork.Sources.Insert(source);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(source);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = _unitOfWork.Sources.GetById(id);
            if (source == null)
            {
                return NotFound();
            }
            return View(source);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Source source)
        {
            if (id != source.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Sources.Update(source);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceExists(source.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(source);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = _unitOfWork.Sources.GetById(id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _unitOfWork.Sources.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SourceExists(Guid id)
        {
            return _unitOfWork.Sources.AsQueryable().Any(e => e.Id == id);
        }
    }
}
