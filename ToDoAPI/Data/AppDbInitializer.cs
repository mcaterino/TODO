using ToDoAPI.Models;

namespace ToDoAPI.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Todos.Any())
                {
                    context.Todos.AddRange(
                      new Todo
                      {
                          Title = "Learn ASP.NET Core",
                          Description = "Learn ASP.NET Core Web API",
                          CreatedAt = DateTime.Now
                      },
                      new Todo
                       {
                           Title = "Learn Angular",
                           Description = "Learn Angular 12",
                           CreatedAt = DateTime.Now
                      },
                      new Todo
                      {
                          Title = "Learn Azure",
                          Description = "Learn Azure DevOps",
                          CreatedAt = DateTime.Now
                      });
                    context.SaveChanges();
                }
            }
        }
    }
}
