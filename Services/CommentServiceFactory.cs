using CommentKillerPOC.Models;

namespace CommentKillerPOC.Services
{
    public interface ICommentServiceFactory
    {
        public ICommentsService GetService(CommentType type);
    }

    public class CommentServiceFactory : ICommentServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommentServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommentsService GetService(CommentType type)
        {
            // inelegant solution
            switch (type)
            {
                case CommentType.TagPlug:
                    return (ICommentsService)_serviceProvider.GetService(typeof(TagPlugCommentsService))!;
                default:
                    return (ICommentsService)_serviceProvider.GetService(typeof(BaseCommentsService))!;
            }
        }
    }
}
