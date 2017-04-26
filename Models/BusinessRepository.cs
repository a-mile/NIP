using System;
using System.Linq;

namespace NIP.Models
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly NIPDbContext _context;

        public BusinessRepository(NIPDbContext context)
        {
            _context = context;
        }

        public Business FindByKRS(string KRS)
        {
            return _context.Businesses.FirstOrDefault(x=>x.KRS == KRS);
        }

        public Business FindByNIP(string NIP)
        {
            return _context.Businesses.FirstOrDefault(x=>x.NIP == NIP);
        }

        public Business FindByREGON(string REGON)
        {
            return _context.Businesses.FirstOrDefault(x=>x.REGON == REGON);
        }
    }
}