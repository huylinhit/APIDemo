using System;
using System.ComponentModel.DataAnnotations;

namespace APIDemo.Models
{
	public class PointOfInterestCreationDTO
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
		public string Name { get; set; }
        [MaxLength(100)]
		public string? Description { get; set; }

    }
}

