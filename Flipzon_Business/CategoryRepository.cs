using AutoMapper;
using Flipzon_Business.Repository.IRepository;
using Flipzon_DataAccess;
using Flipzon_DataAccess.Data;
using Flipzon_Models;
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
        public CategoryDTO Create(CategoryDTO objDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(objDTO);

            _database.categories.Add(category);
            _database.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(category);
            
        }

        public int Delete(int id)
        {
            var deleteObjByID = _database.categories.FirstOrDefault(a => a.Id == id);
            if (deleteObjByID != null)
            {
                _database.Remove(deleteObjByID);
                _database.SaveChanges();
            }
            return 0;
        }

        public CategoryDTO Get(int id)
        {
            var getObjByID = _database.categories.FirstOrDefault(a => a.Id == id);
            if (getObjByID != null)
            {
                return _mapper.Map<Category, CategoryDTO>(getObjByID);
            }
            return new CategoryDTO();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_database.categories);

        }
 

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            var getObjFromDB = _database.categories.FirstOrDefault(a => a.Id == objDTO.Id);
            if (getObjFromDB != null) 
            {
                getObjFromDB.Name = objDTO.Name;
                _database.categories.Update(getObjFromDB);
                _database.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(getObjFromDB);
            }
            return new CategoryDTO();
        }
    }
}
