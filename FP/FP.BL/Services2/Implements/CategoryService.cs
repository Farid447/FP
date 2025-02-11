using BlogApp.BL.DTOs.CategoryDtos;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.BL.Services.Implements;

public class CategoryService(ICategoryRepository _repo):ICategoryService
{
    public async Task<IEnumerable<CategoryListItem>> GetAllAsync()
    {
       return (await _repo.GetAllAsync()).Select(x=>new CategoryListItem()
       {
           Id = x.Id,
           Name = x.Name,
           Icon = x.Icon
       });
    }

    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        Category cat = dto;
        await _repo.AddAsync(cat);
        await _repo.SaveAsync();
        return cat.Id;
    }
}