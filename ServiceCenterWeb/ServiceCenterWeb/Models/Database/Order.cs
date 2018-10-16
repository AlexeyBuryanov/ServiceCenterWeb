using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models.Database
{
    /// <summary>
    /// Заказы
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = new Client();

        public int TechnicId { get; set; }
        public virtual Technic Technic { get; set; } = new Technic();

        public int MasterId { get; set; }
        public virtual Master Master { get; set; } = new Master();

        public int TypeWorkId { get; set; }
        public virtual TypeWork TypeWork { get; set; } = new TypeWork();

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
