namespace PalTracker
{
public static class MappingExtensions
{
        public static TimeEntry ToEntry(this TimeEntryRecord record)
        {
            TimeEntry entry = new TimeEntry
            {
                Id = record.Id,
                ProjectId= record.ProjectId,
                UserId= record.UserId,
                Date =record.Date,
                Hours =record.Hours
            };
            return entry;
        }
        public static TimeEntryRecord ToRecord(this TimeEntry record)
        {
            TimeEntryRecord entry = new TimeEntryRecord
            {
                Id = record.Id,
                ProjectId = record.ProjectId,
                UserId = record.UserId,
                Date = record.Date,
                Hours = record.Hours
            };
            return entry;
        }

    }
}