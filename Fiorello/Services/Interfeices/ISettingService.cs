namespace Fiorello.Services.Interfeices
{
    public interface ISettingService
    {
        public Task<Dictionary<string, string>> GetAllSettings();
    }
}
