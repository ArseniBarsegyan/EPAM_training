﻿using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
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
    }
}