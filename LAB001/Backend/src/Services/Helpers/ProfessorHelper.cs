using Backend.src.Data;
using Backend.src.Middlewares;

namespace Backend.src.services.Helpers
{
    public class ProfessorHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
    }
}
