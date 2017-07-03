using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Util;
using ManagerSystem.DAL.Entities;
using ManagerSystem.DAL.Interfaces;

namespace ManagerSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork UnitOfWork { get; }

        public ProductService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<ProductDto> GetAllProductsList()
        {
            var productList = new List<ProductDto>();

            foreach (var product in UnitOfWork.ProductRepository.GetAll())
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
                productList.Add(productDto);
            }
            return productList;
        }

        public ProductDto GetProductById(int id)
        {
            var product = UnitOfWork.ProductRepository.GetById(id);
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return productDto;
        }

        public OperationDetails Create(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };
            UnitOfWork.ProductRepository.Create(product);
            UnitOfWork.Save();

            return new OperationDetails(true, ConstantStorage.ProductCreated, string.Empty);
        }

        public OperationDetails Edit(ProductDto productDto)
        {
            var product = UnitOfWork.ProductRepository.GetById(productDto.Id);
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            UnitOfWork.ProductRepository.Update(product);
            UnitOfWork.Save();

            return new OperationDetails(true, ConstantStorage.ProductUpdated, string.Empty);
        }

        public OperationDetails Delete(int id)
        {
            UnitOfWork.ProductRepository.Delete(id);
            UnitOfWork.Save();

            return new OperationDetails(true, ConstantStorage.ProductDeleted, string.Empty);
        }
    }
}