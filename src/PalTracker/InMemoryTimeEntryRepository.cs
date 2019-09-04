using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

 namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private readonly IDictionary<long, TimeEntry> _timeEntries = new Dictionary<long, TimeEntry>();

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var id = _timeEntries.Count + 1;
            timeEntry.Id = id;
            _timeEntries.Add(id, timeEntry);
            return timeEntry;
        }

        public TimeEntry Find(long i) => _timeEntries[i];

        public bool Contains(long i) => _timeEntries.ContainsKey(i);
        
        public IEnumerable<TimeEntry> List() => _timeEntries.Values.ToList();

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;
            _timeEntries[id] = timeEntry;
            return timeEntry;
        }
        public void Delete(long i)
        {
            _timeEntries.Remove(i);
        }
    }
}