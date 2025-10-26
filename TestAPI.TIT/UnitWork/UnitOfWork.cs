using TestAPI.TIT.Models;
using TestAPI.TIT.Repository;

namespace TestAPI.TIT.UnitWork
{
    public class UnitOfWork
    {
        private readonly ITI_newContext _context;
        private GenaricRepository<Course>? _courseRepo;

        public UnitOfWork(ITI_newContext context)
        {
            _context = context;
        }

        public GenaricRepository<Course> CourseRepo
        {
            get
            {
                if (_courseRepo == null)
                    _courseRepo = new GenaricRepository<Course>(_context);

                return _courseRepo;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
