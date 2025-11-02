using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.TIT.Models;

namespace TestAPI.TIT.DTOs.StudentDTOs
{
    public class AllStudentDTO
    {
      //  [Key]
        public int St_Id { get; set; }

      
        public string St_Fname { get; set; }

       
        public string St_Lname { get; set; }

      
        public string? St_Address { get; set; }

        public int? St_Age { get; set; }

        public int? Dept_Id { get; set; }

        public int? St_super { get; set; }

      
      //  public virtual ICollection<Student> InverseSt_superNavigation { get; set; } = new List<Student>();

       // [ForeignKey("St_super")]
       // [InverseProperty("InverseSt_superNavigation")]
      //  public virtual Student St_superNavigation { get; set; }

     //   [InverseProperty("St")]
     //   public virtual ICollection<Stud_Course> Stud_Courses { get; set; } = new List<Stud_Course>();
    }
}
