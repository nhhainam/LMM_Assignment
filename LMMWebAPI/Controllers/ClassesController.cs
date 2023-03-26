using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMMWebAPI.DataAccess;
using AutoMapper;
using LMMWebAPI.Models;

namespace LMMWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private IConfiguration _config;
        private readonly LmmAssignmentContext _context;
        private readonly IMapper mapper;

        public ClassesController(LmmAssignmentContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            this.mapper = mapconfig.CreateMapper();
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
          if (_context.Classes == null)
          {
              return NotFound();
          }
            return await _context.Classes.ToListAsync();
		}

		// GET: api/Classes
		[HttpGet("[action]")]
		public async Task<ActionResult<IEnumerable<Class>>> GetClassesByUserId(int userId)
		{
			if (_context.Classes == null)
			{
				return NotFound();
			}


			return await _context.Users.Include(u => u.Classes)
					.Where(u => u.UserId == userId)
					.SelectMany(u => u.Classes)
					.ToListAsync();
		}

		// GET: api/Classes
		[HttpGet("[action]")]
		public async Task<ActionResult<IEnumerable<ClassDTO>>> Search(string classCode, int userId)
		{
			if (_context.Classes == null)
			{
				return NotFound();
			}

			var classes = await _context.Classes
				.Where(c => c.ClassCode.ToLower().Contains(classCode.ToLower()))
				.ToListAsync();

			var classDTOs = new List<ClassDTO>();
			foreach (var @class in classes)
			{
				var isEnrolled = await _context.Users.AnyAsync(u => u.UserId == userId && u.Classes.Any(c => c.ClassId == @class.ClassId));

				var classDTO = new ClassDTO
				{
					Class = @class,
					IsEnrolled = isEnrolled
				};
				classDTOs.Add(classDTO);
			}

			return classDTOs;
		}

		// GET: api/Classes/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
          if (_context.Classes == null)
          {
              return NotFound();
          }
            var @class = await _context.Classes.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }

		[HttpPost("[action]")]
		public async Task<ActionResult> JoinClass([FromBody] JoinClassDTO joinClassDTO)
		{
            int userId = joinClassDTO.UserId;
            int classId = joinClassDTO.ClassId;
			// Get the user making the request
			var user = await _context.Users.FindAsync(userId);
			if (user == null)
			{
				return NotFound("User not found");
			}

			// Get the class to join
			var @class = await _context.Classes.FindAsync(classId);
			if (@class == null)
			{
				return NotFound("Class not found");
			}

			// Check if the user is already enrolled in the class
			var isEnrolled = await _context.Users.AnyAsync(u => u.UserId == userId && u.Classes.Any(c => c.ClassId == @class.ClassId));
			if (isEnrolled)
			{
				return BadRequest("User is already enrolled in the class");
			}


			try
			{
				user.Classes.Add(@class);
				await _context.SaveChangesAsync();
				return Ok("User joined class successfully");
			}
			catch (DbUpdateException)
			{
				// If there's a problem saving the changes to the database, return an error
				return StatusCode(StatusCodes.Status500InternalServerError, "Failed to join class");
			}

		}

		// PUT: api/Classes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
          if (_context.Classes == null)
          {
              return Problem("Entity set 'LmmAssignmentContext.Classes'  is null.");
          }
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.ClassId }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (_context.Classes == null)
            {
                return NotFound();
            }
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return (_context.Classes?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}
