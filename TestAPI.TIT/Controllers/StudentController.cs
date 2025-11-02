using Microsoft.AspNetCore.Mvc;
using TestAPI.TIT.DTOs.StudentDTOs;
using TestAPI.TIT.Models;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        private readonly UnitOfWork<Student> _unitOfWork;


        public StudentController(UnitOfWork<Student> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetAllStudent")]
        public async Task<IActionResult> GetAll()
        {

            var students = _unitOfWork.Repository.GetAll();


            List<AllStudentDTO> _students = new List<AllStudentDTO>();
            foreach (var item in students)
            {
                var student = new AllStudentDTO()
                {
                    St_Id = item.St_Id,
                    St_Fname = item.St_Fname,
                    St_Lname = item.St_Lname,
                    St_Address = item.St_Address,
                    Dept_Id = item.Dept_Id,
                    St_Age = item.St_Age,
                    St_super = item.St_super,

                };
                _students.Add(student);
            }
            return Ok(_students);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = _unitOfWork.Repository.GetById(id);

            if (res == null)
                return BadRequest();
            return Ok(res);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByFName(string name)
        {
            var res = _unitOfWork.Repository.GetAll()
        .Where(s => s.St_Fname == name)
        .Select(s => new
        {
            s.St_Id,
            s.St_Fname,
            s.St_Lname,
            s.St_Address,
            s.St_Age,
            s.Dept_Id,
            Super = s.St_superNavigation == null ? null : new
            {
                s.St_superNavigation.St_Id,
                s.St_superNavigation.St_Fname,
                s.St_superNavigation.St_Lname
            }
        })
        .FirstOrDefault();

            if (res == null)
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
        public async Task<IActionResult> Create(CreateStudentDTO stu)
        {

            if (stu == null)
                return BadRequest("null");
            if (!ModelState.IsValid)
                return BadRequest("modelstate");
            else
            {
                var stus = _unitOfWork.Repository.GetAll();

                int newId = stus.Any() ? stus.Max(s => s.St_Id) + 1 : 1;
                Student newStu = new()
                {
                    St_Id = newId,
                    St_Fname = stu.St_Fname,
                    St_Lname = stu.St_Lname,
                    St_Address = stu.St_Address,
                    Dept_Id = stu.Dept_Id,
                    St_Age = stu.St_Age,
                    St_super = stu.St_super,
                };
                _unitOfWork.Repository.Create(newStu);
                _unitOfWork.Save();
                return Created();
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CreateStudentDTO updated)
        {

            if (id == null)
                return NotFound();

            if (updated == null)
                return BadRequest();
            Student newStu = new()
            {
                St_Id = id,
                St_Fname = updated.St_Fname,
                St_Lname = updated.St_Lname,
                St_Address = updated.St_Address,
                Dept_Id = updated.Dept_Id,
                St_Age = updated.St_Age,
                St_super = updated.St_super,
            };

            _unitOfWork.Repository.Update(newStu);
            _unitOfWork.Save();
            return Ok("Updeted");
        }





    }

}


