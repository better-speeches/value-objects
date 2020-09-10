using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.ConsoleApp
{
    public static partial class Program
    {
        private static async Task Main()
        {
            using var scope = InitApplication().CreateScope();
            await MigrateDatabase(scope);
            await ExecuteJob(scope);
        }

        private static async Task ExecuteJob(IServiceScope scope)
        {
            var controller = scope.ServiceProvider.GetRequiredService<PersonController>();
            
            await controller.Create("Alex", "Katrynets", 23, Suffix.Jr.Id);
            await controller.PresentPerson(1);
            
            await controller.UpdateGithubUrl(1, "http://github.com/itkerry");
            await controller.PresentPerson(1);

            await controller.UpdateAge(1, 24);
            await controller.PresentPerson(1);

            await controller.Rename(1, "Oleksandr", "Katrynets", Suffix.Sr.Id);
            await controller.PresentPerson(1);

            await controller.PresentWithGithub("http://github.com/itkerry");

            await controller.Remove(1);
            await controller.PresentPerson(1);
        }
    }
}
