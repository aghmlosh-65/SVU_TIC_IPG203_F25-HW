using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceSystem
{
    //أنشاء واجهة تحدد العمليات اللأساسية التي يجب أن توفرها أي أجهزة صيانة
    public interface IDevice
    {
        void CheckDevice();
        void ShowInfo();
    }

    // تطبق الواجهة وتحتوي على الخصائص والخصائص المشتركة لجميع الأجهزة
    public abstract class Device : IDevice
    {
        public string Model;
        //رقم تسلسلي
        public string SerialNumber;
        public string Brand;
        public string OwnerName;
        public bool IsRepaired;

        protected Device(string model, string serialNumber)
        {
            this.Model = model;
            this.SerialNumber = serialNumber;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Device Model : " + Model);
            Console.WriteLine("Device Brand : " + Brand);
            Console.WriteLine("Owner Name   : " + OwnerName);
            Console.WriteLine("Repair Status: " + (IsRepaired ? "Repaired" : "Under Maintenance"));
            Console.WriteLine("Serial Number : " + SerialNumber);
        }

        public abstract void CheckDevice();
        public abstract double GetCost();
    }
}
