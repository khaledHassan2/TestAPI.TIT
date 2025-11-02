using System.ComponentModel.DataAnnotations;

namespace TestAPI.TIT.DTOs.CourseDTOs
{
    public class GetAllCursesDTO
    {
        public int Crs_Id { get; set; }

        public string Crs_Name { get; set; }

        public int? Crs_Duration { get; set; }

        public int? Top_Id { get; set; }
    }
}
