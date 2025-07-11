using SantoSabor.Application.DTOS;
using SantoSabor.Application.Interfaces;
using SantoSabor.Core.Entities;
using SantoSabor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Application.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<bool> ChangeStatusMealAsync(Guid id)
        {
            var meal = await _mealRepository.GetMealByIdAsync(id);
            if(meal == null)
            {
                return false;
            }
            meal.Active = !meal.Active;
            await _mealRepository.UpdateMealAsync(meal);
            await _mealRepository.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> CreateMealAsync(MealCreateDTO mealCreateDto)
        {
            var Existingmeal = await _mealRepository.getByNameAsync(mealCreateDto.Name);
            if (Existingmeal !=null)
            {
                throw new Exception("Esse prato já foi criado!");
            }

            var meal = new Meal
            {
                MealId = Guid.NewGuid(),
                Name = mealCreateDto.Name,
                Price = mealCreateDto.Price,
                CreatedAt = DateTime.UtcNow,
            };
            await _mealRepository.RegisterMealAsync(meal);
            return meal.MealId;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            var meals = await _mealRepository.GetAllMealAsync();
            if (!meals.Any())
            {
                throw new Exception("A lista de Pratos está vazia");
            }

            return meals.Select(meal => new Meal
            {
                MealId = meal.MealId,
                Name = meal.Name,
                Price = meal.Price,
                Active = meal.Active,
                CreatedAt = DateTime.UtcNow,
            });
        }

        public async Task<Meal> GetMealByIdAsync(Guid id)
        {
            var meal = await _mealRepository.GetMealByIdAsync(id);
            if(meal == null)
            {
                return null;
            }

            return new Meal
            {
                MealId = meal.MealId,
                Name = meal.Name,
                Price = meal.Price,
                Active = meal.Active,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public async Task<bool> RemoveMealAsync(Guid id)
        {
            var meal = await _mealRepository.GetMealByIdAsync(id);
            if(meal == null)
            {
                throw new Exception("Prato com a id passada não foi encontrado!");
            }

            await _mealRepository.RemoveMealAsync(meal);
            return true;
        }

        public async Task<bool> UpdateMealAsync(Guid id, MealUpdateDTO mealUpdateDto)
        {
            var meal = await _mealRepository.GetMealByIdAsync(id);
            if(meal == null)
            {
                throw new Exception("Prato com a id passada não foi encontrado!"); 
            }

            meal.Name = mealUpdateDto.Name;
            meal.Price = mealUpdateDto.Price;

            await _mealRepository.UpdateMealAsync(meal);
            return true;
        }
    }
}
