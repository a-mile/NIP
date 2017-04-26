namespace NIP.Models
{
    public interface ILogsRepository
    {
        void AddLog(Log log);
        void SaveChanges();
    }
}