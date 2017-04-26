using System;

namespace NIP.Models
{
    public class LogsRepository : ILogsRepository
    {
        private readonly NIPDbContext _context;

        public LogsRepository(NIPDbContext context)
        {
            _context = context;
        }

        public void AddLog(Log log)
        {
            _context.Logs.Add(log);            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}