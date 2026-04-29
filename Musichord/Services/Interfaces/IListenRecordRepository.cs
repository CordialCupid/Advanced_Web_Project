using Musichord.Models.Entities;

namespace Musichord.Services;

public interface IListenRecordRepository
{
    Task CreateListenRecordsAsync(List<ListenRecord> records);
}