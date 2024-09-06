using CommentKillerPOC.Models;
using CommentKillerPOC.Repositories;

namespace CommentKillerPOC.Services
{
    public class TagPlugCommentsService : BaseCommentsService, ICommentsService
    {
        public TagPlugCommentsService(ICommentRepository repository) : base(repository)
        {
        }

        public override Comment Add(Comment comment)
        {
            comment.Text = comment.Text + " - TagPlug";
            return base.Add(comment);
        }
    }
}
