using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        public ITimeEntryRepository _repo { get; set; }
        private IOperationCounter<TimeEntry> _counter;

        public TimeEntryController(ITimeEntryRepository repo, IOperationCounter<TimeEntry> counter)
        {
            _repo = repo;
            _counter = counter;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult  Read(int id)
        {
            _counter.Increment(TrackedOperation.Read);
           return _repo.Contains(id) ? (IActionResult) Ok(_repo.Find(id)) : NotFound();
        }

        [HttpPost]
        public IActionResult  Create([FromBody] TimeEntry timeEntry)
        {
            _counter.Increment(TrackedOperation.Create);
            var createdTimeEntry = _repo.Create(timeEntry);
            //return Created("", createdTimeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }
        
        [HttpGet]
        public IActionResult  List() 
        {
            _counter.Increment(TrackedOperation.List);
            return Ok(_repo.List());
        }
        
        [HttpPut("{id}")]
        public IActionResult  Update(int id, [FromBody] TimeEntry timeEntry)
        {
            _counter.Increment(TrackedOperation.Update);
            return _repo.Contains(id) ? (IActionResult) Ok(_repo.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{i}")]
        public IActionResult  Delete(int i)
        {
            _counter.Increment(TrackedOperation.Delete);
             if(!_repo.Contains(i))
             {  
                return NotFound();
             }
             _repo.Delete(i);
             return NoContent();
        }
    }
}