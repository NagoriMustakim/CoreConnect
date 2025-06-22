using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Comment;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly IMapper _mapper;

        public CommentRepository(LinkwayDbContext dbContextLinkway, UserManager<UsrUser> managerUser, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _managerUser = managerUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentListDTO>> GetAllCommentsAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var commentList = await _dbContextLinkway.UsrComments.Include(c => c.Commenter).Where(comment => comment.UserId == user.EmployeeCode).ToListAsync();
                return _mapper.Map<IEnumerable<CommentListDTO>>(commentList);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateCommentAsync(string userId, string commenterId, string comment)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null)
                    return false;

                var commenter = await _managerUser.FindByEmailAsync(commenterId);
                if (commenter == null)
                    return false;

                var commentObj = new UsrComment
                {
                    CommentGuid = Guid.NewGuid(),
                    CommenterId = commenter.EmployeeCode,
                    UserId = user.EmployeeCode,
                    Comment = comment,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                };

                await _dbContextLinkway.UsrComments.AddAsync(commentObj);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditCommentAsync(CommentEditDTO dtoCommentEdit)
        {
            try
            {
                var existingComment = await _dbContextLinkway.UsrComments.FirstOrDefaultAsync(comment => comment.CommentGuid == dtoCommentEdit.CommentGuid);
                if (existingComment != null)
                {
                    existingComment.Comment = dtoCommentEdit.Comment;
                    existingComment.ModificationDate = DateTime.UtcNow;
                }
                else
                {
                    return false;
                }
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            try
            {
                var existingComment = await _dbContextLinkway.UsrComments.FirstOrDefaultAsync(comment => comment.CommentGuid == commentId);
                if (existingComment != null)
                    _dbContextLinkway.Remove(existingComment);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
