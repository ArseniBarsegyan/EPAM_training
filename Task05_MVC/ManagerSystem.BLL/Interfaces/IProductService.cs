using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;

namespace ManagerSystem.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProductsList();
        ProductDto GetProductById(int id);
        OperationDetails Create(ProductDto productDto);
        OperationDetails Edit(ProductDto productDto);
        OperationDetails Delete(int id);
    }
}