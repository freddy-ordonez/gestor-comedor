﻿namespace ComedorInfantil.Gestion.Application.DTOs.Activity
{
    public class CreateActivityDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
