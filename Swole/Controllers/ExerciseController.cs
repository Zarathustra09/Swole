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
    public class ExercisesController : ControllerBase
    {
        private readonly DbContextClass _context;

        public ExercisesController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            var exercises = await _context.Exercises
                .Select(e => new ExerciseDTO
                {
                    Exercise_Id = e.Exercise_Id,
                    Exercise_Name = e.Exercise_Name,
                    Category_Id = e.Category_Id
                })
                .ToListAsync();

            return exercises;
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseDTO = new ExerciseDTO
            {
                Exercise_Id = exercise.Exercise_Id,
                Exercise_Name = exercise.Exercise_Name,
                Category_Id = exercise.Category_Id
            };

            return exerciseDTO;
        }

        // POST: api/Exercises
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> PostExercise(ExerciseDTO exerciseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exercise = new Exercise
            {
                Exercise_Name = exerciseDTO.Exercise_Name,
                Category_Id = exerciseDTO.Category_Id
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            exerciseDTO.Exercise_Id = exercise.Exercise_Id; // Update DTO with the generated Exercise_Id

            return CreatedAtAction(nameof(GetExercise), new { id = exerciseDTO.Exercise_Id }, exerciseDTO);
        }

        // PUT: api/Exercises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, ExerciseDTO exerciseDTO)
        {
            if (id != exerciseDTO.Exercise_Id)
            {
                return BadRequest();
            }

            var exercise = new Exercise
            {
                Exercise_Id = exerciseDTO.Exercise_Id,
                Exercise_Name = exerciseDTO.Exercise_Name,
                Category_Id = exerciseDTO.Category_Id
            };

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // DELETE: api/Exercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Exercise_Id == id);
        }
    }
}
