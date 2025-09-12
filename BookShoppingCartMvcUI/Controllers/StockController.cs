using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using THEBOOKSTORE.Models.DTOs;
using THEBOOKSTORE.Repoditories;

namespace THEBOOKSTORE.Controllers
{
    [Authorize(Roles=nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockRepository.GetStocks(sTerm);

            return View(stocks);
        }
        public async Task<IActionResult> ManangeStock(int bookId)
        {
            var axistingStock = await _stockRepository.GetStockByBookId(bookId);

            var stock = new StockDTO
            {
                BookId = bookId,
                Quantity = axistingStock != null ? axistingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManangeStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
                return View(stock);

            try
            {
                await _stockRepository.ManangeStock(stock);
                TempData["successMessage"] = "Количество книг изменено";

            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Что-то пошло не так!!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
