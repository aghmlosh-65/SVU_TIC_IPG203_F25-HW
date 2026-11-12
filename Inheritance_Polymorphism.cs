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

    // الفئات الثلاث 

    // صنف الثلاجة
    public class Refrigerator : Device
    {
        public Refrigerator(string model, string serialNumber) : base(model, serialNumber) { }
        public bool HasFreezer;

        public override void CheckDevice()
        {
            if (HasFreezer)
            {
                Console.WriteLine("Inspecting refrigerator: Checking dual cooling system and freezer.");
            }
            else
            {
                Console.WriteLine("Inspecting refrigerator: Checking single door cooling.");
            }
        }
        //تطبيق دالة حساب التكلفة 
        public override double GetCost()
        {
            if (HasFreezer)
            {
                return 150.0;
            }
            else
            {
                return 100.0;
            }
        }
    }

    // صنف المكيف
    public class AirConditioner : Device
    {
        public AirConditioner(string model, string serialNumber) : base(model, serialNumber) { }
        public bool IsInverter; //خاصية لتحديد نظام الانفرتر

        public override void CheckDevice()
        {
            if (IsInverter)
            {
                Console.WriteLine("Inspecting air conditioner: Checking inverter system and gas.");
            }
            else
            {
                Console.WriteLine("Inspecting air conditioner: Checking standard compressor.");
            }
        }

        public override double GetCost()
        {
            if (IsInverter)
            {
                return 200.0;
            }
            else
            {
                return 120.0;
            }
        }
    }

    // صنف الغسالة 
    public class WashingMachine : Device
    {
        public WashingMachine(string model, string serialNumber) : base(model, serialNumber) { }
        public bool HasAutomaticDryer; //خاصية لتحديد وجود مجفف آلي

        public override void CheckDevice()
        {
            if (HasAutomaticDryer)
            {
                Console.WriteLine("Inspecting washing machine: Checking automatic dryer drum.");
            }
            else
            {
                Console.WriteLine("Inspecting washing machine: Checking standard water pump.");
            }
        }
public override double GetCost()
        {
            if (HasAutomaticDryer)
            {
                return 140.0;
            }
            else
            {
                return 90.0;
            }
        }
    }

    public class RepairCenter //قائمة الأجهزة ومعالجتها بتعدد الأشكال
{
        private List<Device> devices = new List<Device>();
       public void AddDevice(Device d) //إضافة جهاز للقائمة
        {
            devices.Add(d);
        }
    public void ShowAllDevices() //عرض معلومات الأجهزة في القائمة وتفحصه وتطبع تكلفةإصلاحه 
        {
            foreach(Device d in devices)
            {
                d.ShowInfo();
                d.CheckDevice();
                Console.WriteLine("Repair Cost:  $" + d.GetCost());
                Console.WriteLine();
            }
        }
    public double GetTotalCost() // جمع تكلفة كل الأجهزة 
        {
            double total = 0;
            foreach(Device d in devices)
            {
                total += d.GetCost();
            }
            return total;
        }
    }
}