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
    public class ProductPriceRepository : IProductPriceRepository
    {

        public readonly ApplicationDBContext _database;

        public readonly IMapper _mapper;
        public ProductPriceRepository(ApplicationDBContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }
        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var productPrice = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);

            _database.ProductPrices.Add(productPrice);
            await _database.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice);
        }

        public async Task<int> Delete(int id)
        {
            var deleteObjByID = await _database.ProductPrices.FirstOrDefaultAsync(a => a.Id == id);
            if (deleteObjByID != null)
            {
                _database.Remove(deleteObjByID);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(int id)
        {
            //var getObjByID = await _database.Products.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
            var getObjByID = await _database.ProductPrices.FirstOrDefaultAsync(a => a.Id == id);
            if (getObjByID != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(getObjByID);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_database.ProductPrices.Where(a => a.ProductId == id));
            }
            else
            { 
                //return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_database.ProductPrices.Include(i=>i.Product));
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_database.ProductPrices);

            }
           

        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var getObjFromDB = await _database.ProductPrices.FirstOrDefaultAsync(a => a.Id == objDTO.Id);
            if (getObjFromDB != null)
            {
                getObjFromDB.Price = objDTO.Price;
                getObjFromDB.Size = objDTO.Size;
                getObjFromDB.ProductId = objDTO.ProductId;


                _database.ProductPrices.Update(getObjFromDB);
                await _database.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(getObjFromDB);
            }
            return new ProductPriceDTO();
        }
    }
}
