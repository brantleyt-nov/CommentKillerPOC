using CommentKillerPOC.Models;
using CommentKillerPOC.Repositories;

namespace CommentKillerPOC.Services
{
    public class BaseCommentsService : ICommentsService
    {
        protected readonly ICommentRepository _repository;

        public BaseCommentsService(ICommentRepository repository) 
        {
            _repository = repository;
        }

        public virtual Comment Add(Comment comment)
        {
            return _repository.Add(comment);
        }

        public virtual IEnumerable<Comment> Get()
        {
            return _repository.Get();
        }

        public virtual Comment? Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
