using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace APIDemo.Model
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
                return PointOfInterest.Count();
            }
        }

        public ICollection<PointOfInterestDTO> PointOfInterest { get; set; }
            = new List<PointOfInterestDTO>();
    }
}

