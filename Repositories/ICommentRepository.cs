using CommentKillerPOC.Models;

namespace CommentKillerPOC.Repositories
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> Get();
        public Comment? Get(int id);
        public Comment Add(Comment comment);
    }
}
