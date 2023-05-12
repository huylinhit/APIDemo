using System;
using System.Xml.Linq;
using APIDemo.Models;

namespace APIDemo
{
	public class CitiesDataStore
	{
		public List<CityDTO> Cities { get; set; }

		public CitiesDataStore()
		{
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name = "Ho Chi Minh",
                    Description = "",
                    PointOfInterests = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id = 1,
                            Name = "Hoc Mon",
                        },
                        new PointOfInterestDTO()
                        {
                            Id = 2,
                            Name = "Cu Chi",
                        }
                    }
                },
                new CityDTO()
                {
                    Id = 2,
                    Name = "Ha Noi",
                    Description = "",
                    PointOfInterests = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id = 3,
                            Name = "Nha Be",
                        },
                        new PointOfInterestDTO()
                        {
                            Id = 4,
                            Name = "Dong Thap",
                        }
                    }
                },
                new CityDTO()
                {
                    Id = 3,
                    Name = "Hai Phong",
                    Description = "a"
                }
            };
		}
	}
}

