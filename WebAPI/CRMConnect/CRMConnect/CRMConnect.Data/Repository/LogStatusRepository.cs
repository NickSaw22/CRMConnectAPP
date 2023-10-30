using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class LogStatusRepository : ILogStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public LogStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LogStatus> AddLogStatusAsync(LogStatus logStatus)
        {
            _context.LogStatus.Add(logStatus);
            await _context.SaveChangesAsync();
            return logStatus;
        }

        public async Task<List<LogStatus>> GetLogStatusAsync()
        {
            var logs = await _context.LogStatus.ToListAsync();
            return logs;
        }
    }
}
