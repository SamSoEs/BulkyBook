using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypefromDb = _unitOfWork.Categories.Find(id);
            var CoverTypefromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var CoverTypefromDb = _unitOfWork.Categories.SingleOrDefault(c => c.Id == id);

            if (CoverTypefromDb == null)
            {
                return NotFound();
            }

            return View(CoverTypefromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }





        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypefromDb = _unitOfWork.Categories.Find(id);
            var CoverTypefromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var CoverTypefromDb = _unitOfWork.Categories.SingleOrDefault(c => c.Id == id);

            if (CoverTypefromDb == null)
            {
                return NotFound();
            }

            return View(CoverTypefromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
