using Service.Identity.Context;
using Service.Identity.Repository.IRepository;

namespace Service.Identity.Repository
{
    public class BranchRepository:IBranchRepository
    {
        private readonly IndentityContext _context;
        public BranchRepository(IndentityContext context)
        {
            _context = context;
        }
    }
}
