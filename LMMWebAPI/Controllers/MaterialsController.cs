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
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.StaticFiles;

namespace LMMWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private IConfiguration _config;
        private readonly LmmAssignmentContext _context;
        private readonly IMapper mapper;

        public MaterialsController(LmmAssignmentContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            this.mapper = mapconfig.CreateMapper();
        }
        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
          if (_context.Materials == null)
          {
              return NotFound();
          }
            return await _context.Materials.ToListAsync();
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(int id)
        {
          if (_context.Materials == null)
          {
              return NotFound();
          }
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, Material material)
        {
            if (id != material.MaterialId)
            {
                return BadRequest();
            }

            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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

        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] int classId)
        {
            var result = await WriteFile(file, classId);
            return Ok(result);
        }

        private async Task<Material> WriteFile(IFormFile file, int classId)
        {

            Material material = new Material();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var name = file.FileName.Split('.')[0];
                LmmAssignmentContext context = new LmmAssignmentContext();

                var date = DateTime.Now;
                filename = name + "_" + date.Ticks.ToString() + extension;


                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\Material" + classId);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\Material" + classId, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                material.Title = filename;
                material.ClassId = classId;
                material.FilePath = exactpath;

                context.Materials.Add(material);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
            }
            return material;
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = MaterialExists(id);
            if (p == false)
                return NotFound();
            var p1 = _context.Materials.SingleOrDefault(c => c.MaterialId == id);
            _context.Materials.Remove(p1);
            _context.SaveChanges();
            return NoContent();
        }

        private bool MaterialExists(int id)
        {
            return (_context.Materials?.Any(e => e.MaterialId == id)).GetValueOrDefault();
        }

        [HttpGet("getbyclass/{classId}")]
        public IActionResult GetMaterialByClass(int classId)
        {
            if (_context.Materials == null)
            {
                return NotFound();
            }
            var material = _context.Materials.Include(c=>c.Class).Where(m => m.ClassId == classId).ToList();

            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }

        [HttpGet("downloadfile/{filename}")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
