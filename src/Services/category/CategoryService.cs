using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.CategoryDTO;

namespace Backend_Teamwork.src.Services.category
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetByIdAsync(Guid id)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            if (foundCategory == null)
            {
                return null;
            }
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }

        public async Task<CategoryReadDto> GetByNameAsync(string name)
        {
            var foundCategory = await _categoryRepository.GetByNameAsync(name);
            if (foundCategory == null)
            {
                return null;
            }
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }

        public async Task<List<CategoryReadDto>> GetByNameWithPaginationAsync(
            PaginationOptions paginationOptions
        )
        {
            var foundCategories = await _categoryRepository.GetByNameWithPaginationAsync(
                paginationOptions
            );
            if (foundCategories.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(foundCategories);
        }

        public async Task<List<CategoryReadDto>> SortByNameAsync()
        {
            var categories = await _categoryRepository.SortByNameAsync();
            if (categories.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto category)
        {
            var foundName = await _categoryRepository.GetByNameAsync(category.Name);
            if (foundName != null)
            {
                return null;
            }
            var mappedCategory = _mapper.Map<CategoryCreateDto, Category>(category);
            var createdCategory = await _categoryRepository.CreateAsync(mappedCategory);
            return _mapper.Map<Category, CategoryReadDto>(createdCategory);
        }

        public async Task<CategoryReadDto> UpdateAsync(Guid id, CategoryUpdateDto category)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            var foundName = await _categoryRepository.GetByNameAsync(category.Name);
            if (foundName != null || foundCategory == null)
            {
                return null;
            }
            var mappedCategory = _mapper.Map<CategoryUpdateDto, Category>(category);
            var updatedCategory = await _categoryRepository.UpdateAsync(mappedCategory);
            return _mapper.Map<Category, CategoryReadDto>(updatedCategory);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            if (foundCategory == null)
            {
                return false;
            }
            await _categoryRepository.DeleteAsync(foundCategory);
            return true;
        }
    }
}