using BGMaterial.Domain.Entities;
using System.Linq.Expressions;

namespace BGMaterial.Application.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetByFilterAsync(Expression<Func<AppUser, bool>> filter);
    }
}
