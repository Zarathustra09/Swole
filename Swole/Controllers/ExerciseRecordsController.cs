using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swole.DataConnection;
using Swole.DTOs;
using Swole.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseRecordsController : ControllerBase
    {
        private readonly DbContextClass _context;

        public ExerciseRecordsController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/ExerciseRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseRecordDTO>>> GetExerciseRecords()
        {
            var exerciseRecords = await _context.ExerciseRecords
                .Select(er => new ExerciseRecordDTO
                {
                    Record_Id = er.Record_Id,
                    Exercise_Id = er.Exercise_Id,
                    Date_Recorded = er.Date_Recorded,
                    Sets = er.Sets,
                    Reps = er.Reps,
                    Weight = er.Weight,
                    Duration_Minutes = er.Duration_Minutes,
                    Notes = er.Notes
                })
                .ToListAsync();

            return exerciseRecords;
        }

        // GET: api/ExerciseRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseRecordDTO>> GetExerciseRecord(int id)
        {
            var exerciseRecord = await _context.ExerciseRecords.FindAsync(id);

            if (exerciseRecord == null)
            {
                return NotFound();
            }

            var exerciseRecordDTO = new ExerciseRecordDTO
            {
                Record_Id = exerciseRecord.Record_Id,
                Exercise_Id = exerciseRecord.Exercise_Id,
                Date_Recorded = exerciseRecord.Date_Recorded,
                Sets = exerciseRecord.Sets,
                Reps = exerciseRecord.Reps,
                Weight = exerciseRecord.Weight,
                Duration_Minutes = exerciseRecord.Duration_Minutes,
                Notes = exerciseRecord.Notes
            };

            return exerciseRecordDTO;
        }

        // POST: api/ExerciseRecords
        [HttpPost]
        public async Task<ActionResult<ExerciseRecordDTO>> PostExerciseRecord(ExerciseRecordDTO exerciseRecordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exerciseRecord = new ExerciseRecord
            {
                Exercise_Id = exerciseRecordDTO.Exercise_Id,
                Date_Recorded = exerciseRecordDTO.Date_Recorded,
                Sets = exerciseRecordDTO.Sets,
                Reps = exerciseRecordDTO.Reps,
                Weight = exerciseRecordDTO.Weight,
                Duration_Minutes = exerciseRecordDTO.Duration_Minutes,
                Notes = exerciseRecordDTO.Notes
            };

            _context.ExerciseRecords.Add(exerciseRecord);
            await _context.SaveChangesAsync();

            exerciseRecordDTO.Record_Id = exerciseRecord.Record_Id; // Update DTO with the generated Record_Id

            return CreatedAtAction(nameof(GetExerciseRecord), new { id = exerciseRecordDTO.Record_Id }, exerciseRecordDTO);
        }

        // PUT: api/ExerciseRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseRecord(int id, ExerciseRecordDTO exerciseRecordDTO)
        {
            if (id != exerciseRecordDTO.Record_Id)
            {
                return BadRequest();
            }

            var exerciseRecord = new ExerciseRecord
            {
                Record_Id = exerciseRecordDTO.Record_Id,
                Exercise_Id = exerciseRecordDTO.Exercise_Id,
                Date_Recorded = exerciseRecordDTO.Date_Recorded,
                Sets = exerciseRecordDTO.Sets,
                Reps = exerciseRecordDTO.Reps,
                Weight = exerciseRecordDTO.Weight,
                Duration_Minutes = exerciseRecordDTO.Duration_Minutes,
                Notes = exerciseRecordDTO.Notes
            };

            _context.Entry(exerciseRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ExerciseRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseRecord(int id)
        {
            var exerciseRecord = await _context.ExerciseRecords.FindAsync(id);
            if (exerciseRecord == null)
            {
                return NotFound();
            }

            _context.ExerciseRecords.Remove(exerciseRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseRecordExists(int id)
        {
            return _context.ExerciseRecords.Any(e => e.Record_Id == id);
        }
    }
}
