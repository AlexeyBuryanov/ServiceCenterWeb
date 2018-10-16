using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models.Database
{
    /// <summary>
    /// Тип техники
    /// </summary>
    public class TypeTechnic
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
