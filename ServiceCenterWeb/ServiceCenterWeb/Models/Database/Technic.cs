using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models.Database
{
    /// <summary>
    /// Техника
    /// </summary>
    public class Technic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TypeTechnicId { get; set; }
        public virtual TypeTechnic TypeTechnic { get; set; } = new TypeTechnic();

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; } = new Manufacturer();
    }
}
