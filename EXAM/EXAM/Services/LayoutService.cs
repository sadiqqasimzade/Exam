using EXAM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXAM.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string,string>  GetLayout()
        {
            return _context.Settings.ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
