﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Package
{
    public class PackageDasboardRes
    {
        public int Id { get; set; } 
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedAgo { get; set; } = $"No content";
        public IEnumerable<int> Levels { get; set; }  = new List<int>();
        public int NumberParticipants { get; set; } = 0;
        public int GetDaysSinceUpdated()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = currentDate - UpdatedAt;
            return timeSpan.Days;
        }
        public void UpdateUpdatedAgo()
        {
            int daysSinceUpdated = GetDaysSinceUpdated();
            UpdatedAgo = daysSinceUpdated > 0 ? $"Updated {daysSinceUpdated} day{(daysSinceUpdated > 1 ? "s" : "")} ago" : "Updated today";
        }



    }
}
