using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestAPI.TIT.DTOs.CourseDTOs;
using TestAPI.TIT.Models;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
      
        private readonly UnitOfWork<Course>  _unitOfWork;


        public CourseController(UnitOfWork<Course> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet(Name = "GetAllCurses")]
        public async Task<IActionResult> GetAll()
        {
           
             var Courses = _unitOfWork.Repository.GetAll();


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
            var res =  _unitOfWork.Repository.GetById(id);

            if (res == null)
                return BadRequest();
            return Ok(res);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
              var res =  _unitOfWork.Repository.GetAll().FirstOrDefault(c=>c.Crs_Name==name);

            if (res==null)
                return NotFound();
            return Ok(res);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = _unitOfWork.Repository.GetById(id);

            if (res == null)
                return NotFound();

            _unitOfWork.Repository.Delete(id);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(CreateCourseDTO course)
        {
          
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

             
                _unitOfWork.Repository.Create(cours);
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
            _unitOfWork.Repository.Update(updated);
            _unitOfWork.Save();
            return Ok("Updeted");
        }





    }
}
