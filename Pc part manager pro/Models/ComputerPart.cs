using System;

namespace Pc_part_manager_pro.Models
{
    public abstract class ComputerPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category PartCategory { get; set; } = new Category();
        
        // Връща името на конкретния C# клас (напр. "Cpu", "Gpu", "Ram"), което е перфектно за таблицата
        public string PartType => GetType().Name;

        // Абстрактен метод за полиморфно извличане на технически данни
        public abstract string GetSpecifications();
    }
}