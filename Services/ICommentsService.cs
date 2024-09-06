using CommentKillerPOC.Models;

namespace CommentKillerPOC.Services
{
    public interface ICommentsService
    {
        public IEnumerable<Comment> Get();
        public Comment? Get(int id);
        public Comment Add(Comment comment);
    }
}
