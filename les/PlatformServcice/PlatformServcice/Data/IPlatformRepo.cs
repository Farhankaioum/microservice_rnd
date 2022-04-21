using PlatformServcice.Models;
using System.Collections.Generic;

namespace PlatformServcice.Data
{
    public interface IPlatformRepo
    {
        bool SaveCanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    }
}
