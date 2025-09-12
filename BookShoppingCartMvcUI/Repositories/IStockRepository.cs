using THEBOOKSTORE.Models;
using THEBOOKSTORE.Models.DTOs;

namespace THEBOOKSTORE.Repoditories
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByBookId(int bookId);
        Task ManangeStock(StockDTO stockToManage);
    }
}