﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIDemo.Entities
{
	public class City
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
		public string Name { get; set; }

        [MaxLength(50)]
		public string? Description { get; set; }


        public City(string name)
        {
            Name = name;
        }
    }
}
