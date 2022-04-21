using PlatformServcice.Dtos;
using System.Threading.Tasks;

namespace PlatformServcice.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}
