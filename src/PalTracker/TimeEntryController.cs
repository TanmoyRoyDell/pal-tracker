using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/TimeEntry")]
    public class TimeEntryController : ControllerBase
    {
        public ITimeEntryRepository _repo { get; set; }

        public TimeEntryController(ITimeEntryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{i}")]
        public IActionResult  Read(int i)
        {
           return _repo.Contains(i) ? (IActionResult) Ok(_repo.Find(i)) : NotFound();
        }

        [HttpPost]
        public IActionResult  Create([FromBody] TimeEntry timeEntry)
        {
            var x = _repo.Create(timeEntry);
            return Ok(x);
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
             return Ok();
        }
    }
}