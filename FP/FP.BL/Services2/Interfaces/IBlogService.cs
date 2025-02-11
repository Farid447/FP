using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.BL.DTOs.CommentDtos;

namespace BlogApp.BL.Services.Interfaces;
public interface IBlogService
{
    Task<int> CreateAsync(BlogCreateDto dto);
    Task<IEnumerable<BlogGetDto>> GetAllAsync();
    Task ReactAsync(BlogReactDto dto);
    Task UndoReactAsync(int blogId);
    Task<int> Comment(CommentCreateDto dto);
}
