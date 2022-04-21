using PlatformServcice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformServcice.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
                throw new ArgumentNullException(nameof(plat));

            _context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveCanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }
}
