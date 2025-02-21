using AutoMapper;
using FP.BL.Dtos.Stadium;
using FP.BL.Exceptions.Common;
using FP.BL.Exceptions.FileExceptions;
using FP.BL.Extentions;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace FP.BL.Services.Implements;

public class StadiumService(IStadiumRepository _repo, IWebHostEnvironment _env, IMapper _mapper) : IStadiumService
{
    public async Task<IEnumerable<Stadium>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }
    public async Task CreateAsync(StadiumCreateDto dto)
    {
        if (dto.Image != null)
            if (!dto.Image.IsValid())
                throw new TypeorSizeValidException();

        if (dto.Images != null)
            foreach (var item in dto.Images) 
                if (!item.IsValid()) 
                    throw new TypeorSizeValidException();

        if (await _repo.GetByNameAsync(dto.Name) is not null)
            throw new ExistException<Stadium>();

        Stadium stadium = _mapper.Map<Stadium>(dto);
        
        if(dto.Image != null) 
            stadium.ImageUrl = dto.Image.UploadAsync(_env.WebRootPath, "imgs").Result;

        if (dto.Images != null)
            stadium.ImageUrls = (List<string>)dto.Images.Select(x => x.UploadAsync(_env.WebRootPath, "imgs").Result);

        await _repo.AddAsync(stadium);
    }

    public async Task UpdateAsync(int id, StadiumUpdateDto dto)
    {
        if (dto.Image != null)
            if (!dto.Image.IsValid())
                throw new TypeorSizeValidException();

        if (dto.Images != null)
            foreach (var item in dto.Images)
                if (!item.IsValid())
                    throw new TypeorSizeValidException();

        var stadium = await _repo.GetByIdAsync(id) ?? throw new NotFoundException<Stadium>();

        if (dto.Name != stadium.Name && _repo.GetByNameAsync(dto.Name).Result is not null)
            throw new ExistException<Stadium>();

        if (dto.Image != null)
            stadium.ImageUrl = dto.Image.UploadAsync(_env.WebRootPath, "imgs").Result;

        if (dto.Images != null)
            foreach (var item in dto.Images)
                stadium.ImageUrls.Append(item.UploadAsync(_env.WebRootPath, "imgs").Result);        

        _mapper.Map(dto, stadium);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Stadium? stadium = await _repo.GetByIdAsync(id);
        if (stadium != null)
        {
            if(stadium.ImageUrl != "Default.png")
                stadium.ImageUrl.DeleteImage(_env.WebRootPath, "imgs");

            foreach(var item in stadium.ImageUrls)
                item.DeleteImage(_env.WebRootPath, "imgs");

            await _repo.DeleteAsync(stadium);
        }
    }

    public async Task DeleteImgs(int id, IEnumerable<string> imgs)
    {
        var stadium = await _repo.GetByIdAsync(id);
        foreach (var item in imgs)
        {
            if (stadium.ImageUrls.Contains(item))
            {
                stadium.ImageUrls.Remove(item);
            }
            item.DeleteImage(_env.WebRootPath, "imgs");
        }
    }

    public async Task ToggleAsync(int id)
    {
        await _repo.ToggleAsync(id);
    }
}
