﻿namespace RareCrewAssignment.Models
{
    public class Employee
    {
        public string? EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public int TotalMinutes { get; set; } = 0;
        public int TotalHours { get; set; } = 0;
    }
}
