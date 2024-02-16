using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Incident
    {
        public int Id { get; set; } 
        public int VehicleId {get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int RepairCost { get; set; }
        public bool IsRepaired;

        public Incident(int id,int vehicleId,DateTime date, string description, int repairCost,bool isRepaired)
        {
            Id = id;
            VehicleId = vehicleId;
            Date = date;
            Description = description;
            RepairCost = repairCost;
            IsRepaired = isRepaired;    
        }
    }

