using Fiorello.Data;
using Fiorello.Services.Interfeices;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Fiorello.Services
{
    public class SettingService : ISettingService
    {
        private  readonly AppDbContext dbContext;

        public SettingService(AppDbContext context)
        {
            dbContext = context;

        }

        public async Task<Dictionary<string, string>>GetAllSettings()
        {
           var datas=await  dbContext.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return datas;
        }
    }
}
