using LinkwayAPI.DTOs.Comment;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentListDTO>> GetAllCommentsAsync(string userId);
        Task<bool> CreateCommentAsync(string userId, string commenterId, string comment);
        Task<bool> EditCommentAsync(CommentEditDTO dtoCommentEdit);
        Task<bool> DeleteCommentAsync(Guid commentId);
    }
}
