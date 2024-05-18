using BGMaterial.Application.Interfaces;
using BGMaterial.Domain.Entities;
using BGMaterial.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BGMaterial.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly MaterialContext _context;

        public AppUserRepository(MaterialContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByFilterAsync(Expression<Func<AppUser, bool>> filter)
        {
            var value = await _context.AppUsers.Where(filter).FirstOrDefaultAsync();
            return value;
        }
    }
}
