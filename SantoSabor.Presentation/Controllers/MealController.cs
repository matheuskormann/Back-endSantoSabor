using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoSabor.Application.DTOS;
using SantoSabor.Application.Interfaces;
using SantoSabor.Core.Entities;

namespace SantoSabor.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        /// <summary> Retorna todos os pratos cadastrados no sistema.</summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetAllMeals()
        {
            try
            {
                var meals = await _mealService.GetAllMealsAsync();
                return Ok(meals);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retorna um prato específico pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMealById(Guid id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if(meal == null)
            {
                return NotFound("Nenhum prato com o id passado foi encontrado!");
            }
            return Ok(meal);
        }

        /// <summary>
        /// Cria um novo prato no sistema.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateMeal(MealCreateDTO mealCreateDto)
        {
            try
            {
                await _mealService.CreateMealAsync(mealCreateDto);
                return Ok(mealCreateDto);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao criar prato: {ex.Message}");
            }
        }


        /// <summary>
        /// Atualiza os dados de um prato pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(Guid id, MealUpdateDTO mealUpdateDto)
        {
            try
            {
                var result = await _mealService.UpdateMealAsync(id, mealUpdateDto);
                return result ? Ok("Prato atualizado com sucesso!") : BadRequest("Erro ao atualizar o prato!");

            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao atulizar o prato: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um prato do sistema pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMeal(Guid id)
        {
            var result = await _mealService.RemoveMealAsync(id);
            if (!result)
            {
                return BadRequest("Erro ao remover prato!");
            }
            return Ok("Prato removido com sucesso");
        }

        /// <summary>
        /// Altera o status de um prato do sistema pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleMealStatus(Guid id)
        {
            var result = await _mealService.ChangeStatusMealAsync(id);
            if (!result)
                return NotFound("Prato com a id passada não foi encontrado!");

            return Ok("Status do prato alterado com sucesso.");
        }

    }
}
