﻿using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models.Database
{
    /// <summary>
    /// Мастера
    /// </summary>
    public class Master
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Required]
        public int Reputation { get; set; }

        public string Guid { get; set; }
    }
}