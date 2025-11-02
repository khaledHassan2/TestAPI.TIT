using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.TIT.Models;

namespace TestAPI.TIT.DTOs.Stu_CourDTOs
{
    public class CreatStuCoursDTO
    {
        
        public int Crs_Id { get; set; }

        
        public int St_Id { get; set; }

        public int? Grade { get; set; }

       
    }
}
