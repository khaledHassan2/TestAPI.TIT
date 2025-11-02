using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestAPI.TIT.DTOs.CourseDTOs;
using TestAPI.TIT.DTOs.Stu_CourDTOs;
using TestAPI.TIT.Models;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudCourseController : ControllerBase
    {
        private readonly UnitOfWork<Stud_Course> _unitOfWork;

        public StudCourseController(UnitOfWork<Stud_Course> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      
        [HttpGet(Name = "GetAllStudCourses")]
        public IActionResult GetAll()
        {
            var studCourses = _unitOfWork.Repository.GetAll();
            List<CreatStuCoursDTO> _Stucourses = new List<CreatStuCoursDTO>();
            foreach (var item in studCourses)
            {
                var cours = new CreatStuCoursDTO()
                {
                    Crs_Id = item.Crs_Id,
                     St_Id = item.St_Id,
                      Grade = item.Grade,
                     

                };
                _Stucourses.Add(cours);
            }


            return Ok(_Stucourses);
        }

      
        [HttpGet("{stId:int}/{crsId:int}")]
        public IActionResult GetById(int stId, int crsId)
        {
            var record = _unitOfWork.Repository
                .GetAll().FirstOrDefault(sc=>sc.St_Id == stId &&sc.Crs_Id==crsId);
            var stucours = new CreatStuCoursDTO
            {
                St_Id = stId,
                Crs_Id = crsId,
                Grade = record.Grade

            };

            if (record == null)
                return NotFound("Record not found");

            return Ok(stucours);
        }

       
        [HttpPost]
        public IActionResult Create([FromBody] CreatStuCoursDTO model)
        {
            if (model == null)
                return BadRequest("Data is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stuCours = new Stud_Course
            {
                St_Id = model.St_Id,
                Crs_Id = model.Crs_Id,
                Grade = model.Grade,
            };
            _unitOfWork.Repository.Create(stuCours);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { stId = model.St_Id, crsId = model.Crs_Id }, model);
        }

        
        [HttpPut("{stId:int}/{crsId:int}")]
        public IActionResult Update(int stId, int crsId, [FromBody] UpStuCourseDTO updated)
        {
            if (updated == null)
                return BadRequest("Invalid data");

            var existing = _unitOfWork.Repository
                .GetAll()
                .FirstOrDefault(sc => sc.St_Id == stId && sc.Crs_Id == crsId);

            if (existing == null)
                return NotFound("Record not found");

           
            existing.Grade = updated.Grade;

            _unitOfWork.Repository.Update(existing);
            _unitOfWork.Save();

            return Ok("Updated successfully");
        }

        [HttpDelete("{stId:int}/{crsId:int}")]
        public IActionResult Delete(int stId, int crsId)
        {
            var existing = _unitOfWork.Repository
                .GetAll()
                .FirstOrDefault(sc => sc.St_Id == stId && sc.Crs_Id == crsId);

            if (existing == null)
                return NotFound("Record not found");

           
            _unitOfWork.Repository.Delete(existing);
            _unitOfWork.Save();

            return Ok("Deleted successfully");
        }

    }
}
