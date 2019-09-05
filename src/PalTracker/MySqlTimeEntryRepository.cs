using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private TimeEntryContext _context;
        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            _context = context;
        }
        public bool Contains(long id)
        {
            return _context.TimeEntryRecords.Any(o => o.Id == id);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var record = timeEntry.ToRecord();
            _context.TimeEntryRecords.Add(record);
            _context.SaveChanges();
            return Find(record.Id.Value);
        }

        public void Delete(long id)
        {
            _context.TimeEntryRecords.Remove(_context.TimeEntryRecords.Where(o => o.Id == id).First());
            _context.SaveChanges();
        }

        public TimeEntry Find(long id)
        {
            return _context.TimeEntryRecords.Where(o => o.Id == id).First().ToEntry();
        }

        public IEnumerable<TimeEntry> List()
        {
            return _context.TimeEntryRecords.Select(o => o.ToEntry());
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var recordToUpdate = timeEntry.ToRecord();
            recordToUpdate.Id = id;
            _context.Update(recordToUpdate);
            _context.SaveChanges();
            return Find(id);
        }
    }
}