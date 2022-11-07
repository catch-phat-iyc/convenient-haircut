using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Product.Response;

namespace Admin.Haircut.Business.Service.Interfaces
{
    public interface IProductService
    {
        Task<PagingResult<ProductModel>> GetAll(TableRequest request);
    }
}
