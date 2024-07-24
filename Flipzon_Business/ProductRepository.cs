using AutoMapper;
using Flipzon_Business.Repository.IRepository;
using Flipzon_DataAccess;
using Flipzon_DataAccess.Data;
using Flipzon_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flipzon_Business
{
    public class ProductRepository : IProductRepository
    {
        public readonly ApplicationDBContext _database;

        public readonly IMapper _mapper;

        public ProductRepository(ApplicationDBContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        async Task<ProductDTO> IProductRepository.Create(ProductDTO objDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(objDTO);

            _database.Products.Add(product);
            await _database.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(product);
        }

        async Task<int> IProductRepository.Delete(int id)
        {
            var deleteObjByID = _database.Products.FirstOrDefaultAsync(a => a.Id == id);
            if (deleteObjByID != null)
            {
                _database.Remove(deleteObjByID);
                await _database.SaveChangesAsync();
            }
            return 0;
        }

        async Task<ProductDTO> IProductRepository.Get(int id)
        {
            var getObjByID = await _database.Products.FirstOrDefaultAsync(a => a.Id == id);
            if (getObjByID != null)
            {
                return _mapper.Map<Product, ProductDTO>(getObjByID);
            }
            return new ProductDTO();
        }

        async Task<IEnumerable<ProductDTO>> IProductRepository.GetAll()
        {

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_database.Products);

        }

        async Task<ProductDTO> IProductRepository.Update(ProductDTO objDTO)
        {
            var getObjFromDB = await _database.Products.FirstOrDefaultAsync(a => a.Id == objDTO.Id);
            if (getObjFromDB != null)
            {
                getObjFromDB.Name = objDTO.Name;
                getObjFromDB.Description = objDTO.Description;
                getObjFromDB.ImageUrl = objDTO.ImageUrl;
                getObjFromDB.CategoryId = objDTO.CategoryId;
                getObjFromDB.Color = objDTO.Color;
                getObjFromDB.ShopFavorites = objDTO.ShopFavorites;
                getObjFromDB.CustomerFavorites = objDTO.CustomerFavorites;
                getObjFromDB.Description = objDTO.Description;

                _database.Products.Update(getObjFromDB);
                await _database.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(getObjFromDB);
            }
            return new ProductDTO();
        }
    }
}
