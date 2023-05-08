﻿using System;
namespace APIDemo.Model
{
	public class PointOfInterestDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }

		public PointOfInterestDTO()
		{
		}

        public PointOfInterestDTO(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}

