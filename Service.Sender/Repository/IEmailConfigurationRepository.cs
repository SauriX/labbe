using Microsoft.EntityFrameworkCore;
using Service.Sender.Context;
using Service.Sender.Domain.EmailConfiguration;
using Service.Sender.Repository.IRepository;
using System.Threading.Tasks;

namespace Service.Sender.Repository
{
    public class EmailConfigurationRepository : IEmailConfigurationRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmailConfiguration> GetEmail()
        {
            return await _context.CAT_Email.FirstOrDefaultAsync();
        }

        public async Task UpdateEmail(EmailConfiguration email)
        {
            var conf = await GetEmail();

            if (conf == null)
            {
                _context.Add(email);
            }
            else
            {
                email.Id = conf.Id;
                _context.Update(email);
            }

            await _context.SaveChangesAsync();
        }
    }
}
