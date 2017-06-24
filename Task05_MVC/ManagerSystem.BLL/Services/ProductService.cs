using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private UnitOfWork UnitOfWork { get; }

        public ProductService(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}