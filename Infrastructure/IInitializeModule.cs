using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IInitializeModule
    {
        Task Initialize();
    }
}
