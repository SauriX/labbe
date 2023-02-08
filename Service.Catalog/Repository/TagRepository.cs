using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Tapon;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tag>> GetAll()
        {
            return await _context.CAT_Etiqueta.ToListAsync();
        }
    }
}
