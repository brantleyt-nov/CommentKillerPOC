using CommentKillerPOC.Models;

namespace CommentKillerPOC.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private List<Comment> _comments = new List<Comment>
        {
            new Comment { Id = 1, Text = "This is a comment", Type = CommentType.Standard },
            new Comment { Id = 2, Text = "This is another comment", Type = CommentType.Wireline },
            new Comment { Id = 3, Text = "This is a third comment", Type = CommentType.TagPlug },
            new Comment { Id = 4, Text = "This is a fourth comment", Type = CommentType.WellState }
        };

        public Comment Add(Comment comment)
        {
            comment.Id = _comments.Max(c => c.Id) + 1;
            _comments.Add(comment);
            return comment;
        }

        public IEnumerable<Comment> Get()
        {
            return _comments;
        }

        public Comment? Get(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }
    }
}
