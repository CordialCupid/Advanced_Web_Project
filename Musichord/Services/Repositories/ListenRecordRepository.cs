using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Musichord.Services;

public class ListenRecordRepository : IListenRecordRepository
{
    private readonly ApplicationDbContext _db;

    public ListenRecordRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task CreateListenRecordsAsync(List<ListenRecord> records)
    {
        foreach (var rec in records)
        {
            var existence = GetRecordAsync(rec.Id);

            if (existence == null)
            {
                await CreateRecord(rec);
            }
            
        }
    }

    public async Task CreateRecord(ListenRecord record)
    {
        await _db.ListenRecords.AddAsync(record);
        await _db.SaveChangesAsync();
    }

    public async Task<ListenRecord?> GetRecordAsync(int id)
    {
        return await _db.ListenRecords
                        .Include(l => l.Track)
                        .Include(l => l.User)
                        .FirstOrDefaultAsync(l => l.Id == id);
    }
}