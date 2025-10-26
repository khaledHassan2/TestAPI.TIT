using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestAPI.TIT.DTOs;
using TestAPI.TIT.Models;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        //private readonly ITI_newContext _context;
        private readonly UnitOfWork  _unitOfWork;


        public CourseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet(Name = "GetAllCurses")]
        public async Task<IActionResult> GetAll()
        {
            //var res=await _context.Courses.ToListAsync();
            //return Ok(res);
            // var Courses =await _context.Courses.ToListAsync();
             var Courses = _unitOfWork.CourseRepo.GetAll();


            List<GetAllCursesDTO> _courses = new List<GetAllCursesDTO>();
            foreach (var item in Courses)
            {
                var cours = new GetAllCursesDTO()
                {
                    Crs_Id = item.Crs_Id,
                    Crs_Name = item.Crs_Name,
                    Crs_Duration = item.Crs_Duration,
                    Top_Id = item.Top_Id

                };
                _courses.Add(cours);
            }
            return Ok(_courses);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {

            //var res = await _context.Courses.FindAsync(id);
            var res =  _unitOfWork.CourseRepo.GetById(id);

            if (res == null)
                return BadRequest();
            return Ok(res);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {

            //  var res = await _context.Courses.FirstOrDefaultAsync(c=>c.Crs_Name==name);
              var res =  _unitOfWork.CourseRepo.GetAll().FirstOrDefault(c=>c.Crs_Name==name);

            if (res==null)
                return NotFound();
            return Ok(res);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            // var res = await _context.Courses.FindAsync(id);
            var res = _unitOfWork.CourseRepo.GetById(id);

            if (res == null)
                return NotFound();

            // _context.Courses.Remove(res);
            //await _context.SaveChangesAsync();
            _unitOfWork.CourseRepo.Delete(id);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(CreateCourseDTO course)
        {
            //if (course == null)
            //    return BadRequest();
            
            //_context.Courses.Add(course);
            //_context.SaveChanges();
           // return Created();
            if (course == null)
                return BadRequest("null");
            if (!ModelState.IsValid)
                return BadRequest("modelstate");
            else
            {
                Course cours = new()
                {
                    Crs_Id = course.Crs_Id,
                    Crs_Name = course.Crs_Name,
                    Crs_Duration = course.Crs_Duration,
                    Top_Id = course.Top_Id
                };

                //_context.Courses.Add(cours);
                //_context.SaveChanges();
                _unitOfWork.CourseRepo.Create(cours);
                _unitOfWork.Save();
                return Created();
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Course updated)
        {
            
            if (id == null)
                return NotFound();
            
            if (updated == null)
                return BadRequest();
            
            //_context.Update(updated);
            //_context.SaveChanges();
            _unitOfWork.CourseRepo.Update(updated);
            _unitOfWork.Save();
            return Ok("Updeted");
        }





    }
}
