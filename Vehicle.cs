using DrivingSchoolManagement;
using System;
using System.Collections.Generic;


    public enum TechnicalCondition
    {
        VeryGood,
        Good,
        Average,
        Bad
    }

    public class Vehicle
    {
        public int Id;
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public string DrivingLicenseCategory { get; set; }
        public TechnicalCondition Condition { get; set; }

        public List<Incident> Incidents { get; set; }

        public Vehicle(int id, string brand, string model, int year, double price, TechnicalCondition condition, string drivingLicenseCategory)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Condition = condition;
            DrivingLicenseCategory = drivingLicenseCategory;
            Incidents = new List<Incident>();
        }

        public void AddIncident(Incident incident)
        {
            Incidents.Add(incident);
        }

        public void UpdateCondition(TechnicalCondition newCondition)
        {
            Condition = newCondition;
        }




    }

