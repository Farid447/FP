using AutoMapper;
using FP.BL.Dtos.Rating;
using FP.BL.Dtos.Rating;
using FP.BL.Exceptions.Common;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Repositories;

namespace FP.BL.Services.Implements;

public class RatingService(IGenericRepository<Rating> _repo, IMapper _mapper) : IRatingService
{
    public async Task<IEnumerable<Rating>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task CreateAsync(RatingCreateDto dto)
    {
        var data = await _repo.GetFirstAsync(x=> x.UserId == dto.UserId && x.StadiumId == dto.StadiumId);
        if(data is not null)
        {
            await _repo.DeleteAsync(data);
        }
        Rating Rating = _mapper.Map<Rating>(dto);

        await _repo.AddAsync(Rating);
    }

    public async Task UpdateAsync(int id, RatingUpdateDto dto)
    {
        var Rating = await _repo.GetByIdAsync(id) ?? throw new NotFoundException<Rating>();

        _mapper.Map(dto, Rating);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Rating? Rating = await _repo.GetByIdAsync(id);
        if (Rating != null)
        {
            await _repo.DeleteAsync(Rating);
        }
    }
}
