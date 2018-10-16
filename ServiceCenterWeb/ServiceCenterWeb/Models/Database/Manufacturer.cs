using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models.Database
{
    /// <summary>
    /// Производители
    /// </summary>
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
