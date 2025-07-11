using SantoSabor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Core.Interfaces
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllMealAsync();
        Task<Meal?> GetMealByIdAsync(Guid id);
        Task RegisterMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task RemoveMealAsync(Meal meal);
        Task SaveChangesAsync();

        Task<Meal?> getByNameAsync(string name);
    }
}
