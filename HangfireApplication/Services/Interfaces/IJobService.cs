using System.Threading.Tasks;

namespace HangfireApplication.Services.Interfaces
{
    public interface IJobService
    {

        Task  WatchListJobAsync();

    }
}
