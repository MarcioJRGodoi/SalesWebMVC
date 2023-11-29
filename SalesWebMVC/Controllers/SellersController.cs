using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModel;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAll();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAll();
            var viweModel = new SellerFormViewModel { Departments = departments };
            return View(viweModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departmenst = await _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Departments = departmenst, Seller = seller };
                return View(viewModel);
            }
            await _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornceça um id!" });
            }

            var seller = await _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Nenhum vendedor encontrado para o id fornecido" });
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
             

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornceça um id!" });
            }

            var seller = await _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Nenhum vendedor encontrado para o id fornecido" });
            }
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornceça um id!" });
            }

            var seller = await _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Nenhum vendedor encontrado para o id fornecido" });
            }

            List<Department> departments = await _departmentService.FindAll();
            SellerFormViewModel viweModel = new() { Seller = seller, Departments = departments };
            return View(viweModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departmenst = await _departmentService.FindAll();
                var viewModel = new SellerFormViewModel {Departments = departmenst, Seller = seller };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Não corresponde ao id do vendedor" });
            }
            try
            {
                await _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch(DbConcirrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var vierModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(vierModel);
        }

    }
}
