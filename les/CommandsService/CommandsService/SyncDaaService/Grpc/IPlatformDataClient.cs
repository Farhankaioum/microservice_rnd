using CommandsService.Models;
using System.Collections.Generic;

namespace CommandsService.SyncDaaService.Grpc
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}
