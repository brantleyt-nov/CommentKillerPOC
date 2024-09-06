using CommentKillerPOC.Models;
using CommentKillerPOC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentKillerPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly ICommentServiceFactory _commentsServiceFactory;

        public CommentsController(ILogger<CommentsController> logger, ICommentServiceFactory commentServiceFactory)
        {
            _logger = logger;
            _commentsServiceFactory = commentServiceFactory;
        }

        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return Execute(service => service.Get());
        }

        [HttpGet("{id}")]
        public Comment? Get(int id)
        {
            return Execute(service => service.Get(id));
        }

        [HttpPost]
        public Comment Add(Comment comment)
        {
            return Execute(service => service.Add(comment), comment.Type);
        }

        private T Execute<T>(Func<ICommentsService, T> func, CommentType type = CommentType.Standard)
        {
            var service = _commentsServiceFactory.GetService(type);
            return func(service);
        }
    }
}
