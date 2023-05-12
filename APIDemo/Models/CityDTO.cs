using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace APIDemo.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CityDTO
    {
        public CityDTO()
        {
            
        }
        public CityDTO(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int PointOfInterestsCount
        {
            get
            {
                return PointOfInterests.Count();
            }
        }

        public ICollection<PointOfInterestDTO> PointOfInterests { get; set; }
            = new List<PointOfInterestDTO>();
    }
}

