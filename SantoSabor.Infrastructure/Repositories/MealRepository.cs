using Microsoft.EntityFrameworkCore;
using SantoSabor.Core.Entities;
using SantoSabor.Core.Interfaces;
using SantoSabor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly MyDbContext _context;

        public MealRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Meal>> GetAllMealAsync()
        {
            return await _context.Meals.ToListAsync();
        }

        public async Task<Meal?> getByNameAsync(string name)
        {
            return await _context.Meals.FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower());
        }

        public async Task<Meal?> GetMealByIdAsync(Guid id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task RegisterMealAsync(Meal meal)
        {
            await _context.AddAsync(meal);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMealAsync(Meal meal)
        {
            _context.Remove(meal);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }
    }
}
