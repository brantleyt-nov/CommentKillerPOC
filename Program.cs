
using CommentKillerPOC.Repositories;
using CommentKillerPOC.Services;

namespace CommentKillerPOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
            builder.Services.AddSingleton<ICommentServiceFactory, CommentServiceFactory>();

            builder.Services.AddSingleton<BaseCommentsService>();
            builder.Services.AddSingleton<TagPlugCommentsService>();

            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddSingleton<IDataMonitoringService, DataMonitoringService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
