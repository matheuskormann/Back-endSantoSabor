using SantoSabor.Application.DTOS;
using SantoSabor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Application.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal> GetMealByIdAsync(Guid id);
        Task<Guid> CreateMealAsync(MealCreateDTO mealCreateDto);
        Task<bool> UpdateMealAsync(Guid id, MealUpdateDTO mealUpdateDto);
        Task<bool> RemoveMealAsync(Guid id);
        Task<bool> ChangeStatusMealAsync(Guid id);
    }
}
