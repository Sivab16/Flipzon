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
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ApplicationDBContext _database;

        public readonly IMapper _mapper;

        public CategoryRepository(ApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _database = applicationDBContext;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(objDTO);

            _database.categories.Add(category);
            await _database.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(category);

        }

        public async Task<int> Delete(int id)
        {
            var deleteObjByID = await _database.categories.FirstOrDefaultAsync(a => a.Id == id);
            if (deleteObjByID != null)
            {
                _database.Remove(deleteObjByID);
                await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var getObjByID = await _database.categories.FirstOrDefaultAsync(a => a.Id == id);
            if (getObjByID != null)
            {
                return _mapper.Map<Category, CategoryDTO>(getObjByID);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_database.categories);

        }


        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var getObjFromDB = await _database.categories.FirstOrDefaultAsync(a => a.Id == objDTO.Id);
            if (getObjFromDB != null)
            {
                getObjFromDB.Name = objDTO.Name;
                getObjFromDB.Description = objDTO.Description;
                getObjFromDB.CreatedDate = objDTO.CreatedDate;

                _database.categories.Update(getObjFromDB);
                await _database.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(getObjFromDB);
            }
            return new CategoryDTO();
        }
    }
}
