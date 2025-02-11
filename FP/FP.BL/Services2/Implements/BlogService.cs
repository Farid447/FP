using AutoMapper;
using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.BL.DTOs.CommentDtos;
using BlogApp.BL.Exceptions.BlogExceptions;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;

namespace BlogApp.BL.Services.Implements;

public class BlogService(IBlogRepository _repo, 
    IMapper _mapper,
    ICategoryRepository _catRepo,
    ICurrentUser _user,
    IBlogReactionRepository _reaction,
    ICommentRepository _comment) : IBlogService
{
    private int _userId = _user.GetId();

    public async Task<int> Comment(CommentCreateDto dto)
    {
        Comment? parent = null;
        if (dto.ParentId.HasValue)
        {
            parent = await _comment.GetByIdAsync(dto.ParentId.Value);
            if (parent is null)
                throw new NotFoundException<Comment>();
        }
        var entity = _mapper.Map<Comment>(dto);
        entity.UserId = _userId;
        entity.BlogId = parent?.BlogId ?? dto.BlogId;
        await _comment.AddAsync(entity);
        await _comment.SaveAsync();
        return entity.Id;
    }

    public async Task<int> CreateAsync(BlogCreateDto dto)
    {
        Blog blog = _mapper.Map<Blog>(dto);
        if (!await _catRepo.IsExistAsync(dto.CatId))
            throw new NotFoundException<Category>();
        blog.CoverImage = "default.jpg";
        blog.UserId = _userId;
        await _repo.AddAsync(blog);
        await _repo.SaveAsync();
        return blog.Id;
    }

    public async Task<IEnumerable<BlogGetDto>> GetAllAsync()
    {
        //var entities = await _repo.GetAllAsync(x => new BlogGetDto
        //{
        //    Category = x.Category.Name,
        //    Content = x.Content,
        //    CoverImage = x.CoverImage,
        //    Fullname = x.User.Name + " " + x.User.Surname,
        //    PublishedDate = x.CreatedTime,
        //    ReactionCount = x.Reactions.Count(),
        //    ViewCount = x.ViewCount,
        //    Title = x.Title,
        //    Id = x.Id,
        //    Comments = x.Comments.Where(y => y.ParentId == null)
        //    .Select(a => convertCGD(a))
        //});
        var entities = await _repo.GetAllAsync("Category", "User", "Reactions", "Comments");
        var datas = _mapper.Map<IEnumerable<BlogGetDto>>(entities);
        return datas.Select(x=> new BlogGetDto
        {
            Comments = x.Comments.Where(x=> x.ParentId == 0),
            Category = x.Category,
            Content = x.Content,
            CoverImage = x.CoverImage,
            Fullname = x.Fullname,
            Id = x.Id,
            PublishedDate = x.PublishedDate,
            ReactionCount = x.ReactionCount,
            Title = x.Title,
            ViewCount = x.ViewCount,
        });
    }
    private CommentGetDto convertCGD(Comment a)
    {
        return new CommentGetDto
        {
            Content = a.Content,
            Id = a.Id,
            Children = a.Children.Select(z => convertCGD(z))
        };
    }
    public async Task ReactAsync(BlogReactDto dto)
    {
        if (!await _repo.IsExistAsync(dto.BlogId))
            throw new NotFoundException<Blog>();
        var entity = await _reaction.GetFirstAsync(x => x.UserId == _userId && x.BlogId == dto.BlogId);
        if (entity is null)
        {
            entity = _mapper.Map<BlogReaction>(dto);
            entity.UserId = _userId;
            await _reaction.AddAsync(entity);
        }
        else
        {
            _mapper.Map(dto, entity);
        }
        await _reaction.SaveAsync();
    }

    public async Task UndoReactAsync(int blogId)
    {
        if (!await _repo.IsExistAsync(blogId))
            throw new NotFoundException<Blog>();
        var entity =await _reaction.GetFirstAsync(x => x.UserId == _userId && x.BlogId == blogId);
        if (entity is null)
            throw new NotFoundException<BlogReaction>();
        _reaction.Delete(entity);
        await _reaction.SaveAsync();
    }
}
