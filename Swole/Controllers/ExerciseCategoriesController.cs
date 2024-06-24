using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ExerciseCategoriesController : ControllerBase
    {
        private readonly DbContextClass _context;

        public ExerciseCategoriesController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/ExerciseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseCategoryDTO>>> GetExerciseCategories()
        {
            var categories = await _context.ExerciseCategories
                .Select(ec => new ExerciseCategoryDTO
                {
                    Category_Id = ec.Category_Id,
                    Category_Name = ec.Category_Name
                })
                .ToListAsync();

            return categories;
        }

        // GET: api/ExerciseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseCategoryDTO>> GetExerciseCategory(int id)
        {
            var exerciseCategory = await _context.ExerciseCategories.FindAsync(id);

            if (exerciseCategory == null)
            {
                return NotFound();
            }

            var exerciseCategoryDTO = new ExerciseCategoryDTO
            {
                Category_Id = exerciseCategory.Category_Id,
                Category_Name = exerciseCategory.Category_Name
            };

            return exerciseCategoryDTO;
        }

        // POST: api/ExerciseCategories
        [HttpPost]
        public async Task<ActionResult<ExerciseCategoryDTO>> PostExerciseCategory(ExerciseCategoryDTO exerciseCategoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exerciseCategory = new ExerciseCategory
            {
                Category_Name = exerciseCategoryDTO.Category_Name
            };

            _context.ExerciseCategories.Add(exerciseCategory);
            await _context.SaveChangesAsync();

            exerciseCategoryDTO.Category_Id = exerciseCategory.Category_Id; // Update DTO with the generated Category_Id

            return CreatedAtAction(nameof(GetExerciseCategory), new { id = exerciseCategoryDTO.Category_Id }, exerciseCategoryDTO);
        }

        // PUT: api/ExerciseCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseCategory(int id, ExerciseCategoryDTO exerciseCategoryDTO)
        {
            if (id != exerciseCategoryDTO.Category_Id)
            {
                return BadRequest();
            }

            var exerciseCategory = new ExerciseCategory
            {
                Category_Id = exerciseCategoryDTO.Category_Id,
                Category_Name = exerciseCategoryDTO.Category_Name
            };

            _context.Entry(exerciseCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseCategoryExists(id))
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

        // DELETE: api/ExerciseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseCategory(int id)
        {
            var exerciseCategory = await _context.ExerciseCategories.FindAsync(id);
            if (exerciseCategory == null)
            {
                return NotFound();
            }

            _context.ExerciseCategories.Remove(exerciseCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseCategoryExists(int id)
        {
            return _context.ExerciseCategories.Any(e => e.Category_Id == id);
        }
    }
}
