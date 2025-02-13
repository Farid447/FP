using AutoMapper;
using FP.BL.Dtos.Stadium;
using FP.BL.Exceptions.Common;
using FP.BL.Exceptions.FileExceptions;
using FP.BL.Extentions;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;

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
            stadium.ImageUrl = dto.Image.UploadAsync().Result;

        if (dto.Images != null)
        {
            stadium.ImageUrls = dto.Images.Select(x => x.UploadAsync().Result);
        }

        await _repo.AddAsync(stadium);
    }

    public async Task DeleteAsync(int id)
    {
        Stadium? stadium = await _repo.GetByIdAsync(id);
        if (stadium != null)
            await _repo.DeleteAsync(stadium);
    }


    public Task UpdateAsync(StadiumUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
