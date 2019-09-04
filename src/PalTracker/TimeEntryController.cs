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

        public TimeEntryController(ITimeEntryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult  Read(int id)
        {
           return _repo.Contains(id) ? (IActionResult) Ok(_repo.Find(id)) : NotFound();
        }

        [HttpPost]
        public IActionResult  Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _repo.Create(timeEntry);
            //return Created("", createdTimeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }
        
        [HttpGet]
        public IActionResult  List() 
        {
            return Ok(_repo.List());
        }
        
        [HttpPut("{id}")]
        public IActionResult  Update(int id, [FromBody] TimeEntry timeEntry)
        {
            return _repo.Contains(id) ? (IActionResult) Ok(_repo.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{i}")]
        public IActionResult  Delete(int i)
        {
             if(!_repo.Contains(i))
             {  
                return NotFound();
             }
             _repo.Delete(i);
             return NoContent();
        }
    }
}