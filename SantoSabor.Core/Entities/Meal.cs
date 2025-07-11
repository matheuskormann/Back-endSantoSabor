namespace SantoSabor.Core.Entities
{
    public class Meal
    {
        public Guid MealId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}