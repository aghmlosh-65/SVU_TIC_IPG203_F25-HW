using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceSystem
{
    //يحدد شكل الطريقة التي ستستقبل الإشعار
    public delegate void Notify(string message);
    //أنشاء واجهة تحدد العمليات اللأساسية التي يجب أن توفرها أي أجهزة صيانة
    public interface IDevice
    {
        void CheckDevice();
        void ShowInfo();
    }
    //استثتاء للبيانات الخاطئة المدخلة 
    public class InvalidDeviceException : ApplicationException
    {
        public InvalidDeviceException(string message) : base(message) { }
    }
    // تطبق الواجهة وتحتوي على الخصائص والخصائص المشتركة لجميع الأجهزة
    public abstract class Device : IDevice
    {
        //حقل خاص لا يستطيع أحد خارج الصف التعديل عليه 
        private string model;
        //للقراءة فقط
        public string Model
        {
            get { return model; }
        }
        //رقم تسلسلي
        private string serialNumber;
        public string SerialNumber
        {
            get { return serialNumber; }
        }
        private string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        private string ownerName;
        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }
        //حالة الإصلاح للقراءة فقط
        private bool isRepaired;
        public bool IsRepaired
        {
            get { return isRepaired; }
        }
        //عداد ساكن لحساب التعداد الاجمالي للاجهزة المصنوعة
        private static int totalAppliancesCount = 0;
        public static int TotalAppliancesCount
        {
            get { return totalAppliancesCount; }
        }
        private static int repairedCount = 0;
        public static int RepairedCount
        {
            get { return repairedCount; }
        }
        //حدث ينطلق عند الانتهاء من إصلاح الجهاز
        public event Notify RepairCompleted;
        //الباني: يتحقق من البيانات ويعطي استثناء لو حصل خطأ
        protected Device(string model, string serialNumber)
        {
            if (!Validator.IsValidModel(model))
                throw new InvalidDeviceException("Invalid model");
            if (!Validator.IsValidSerial(serialNumber))
                throw new InvalidDeviceException("Invalid serial number");
            this.model = model;
            this.serialNumber = serialNumber;
            totalAppliancesCount++;// يزيد العداد بإضافة جهاز جديد
        }
        //مسؤولة عن تغيير حالة الجهاز إلى مصلّح وتطلق الحدث للإبلاغ
        public void CompleteRepair()

        {
            if (!isRepaired)//شرط لتفادي غلط عد الجهاز مرتين
            {
                isRepaired = true;
                repairedCount++;
                if (RepairCompleted != null)
                    RepairCompleted("Repair completed for device:  " + Model);
            }
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
        private bool hasFreezer;
        public bool HasFreezer
        {
            get { return hasFreezer; }
            set { hasFreezer = value; }
        }

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
        private bool isInverter;
        public bool IsInverter
        {
            get { return isInverter; }
            set { isInverter = value; }
        } //خاصية لتحديد نظام الانفرتر

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
        private bool hasAutomaticDryer;
        public bool HasAutomaticDryer
        {
            get { return hasAutomaticDryer;}
            set { hasAutomaticDryer = value; }
        }//خاصية لتحديد وجود مجفف آلي

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
    //طرائق للتحقق
    public static class Validator
    {
        //تحقق من عدم خلو الموديل وطوله حرفين أو اكثر وتعود True
        public static bool IsValidModel(string model)
        {
            if (model != null && model != "")
            {
                if (model.Length >= 2)
                {
                    return true;
                }
            }
            return false;
        }
        //تحقق من عدم خلو الرقم التسلسلي وطوله خمسة احرف او اكثر وتعود True
        public static bool IsValidSerial (string serial)
        {
            if (serial != null && serial != "")
            {
                if(serial.Length >=5)
                {
                    return true;
                }
            }
            return false;
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

    //البرنامج الرئيسي (Main)لتشغيل النظام
     class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Home Appliance Maintenance System ***");
            Console.WriteLine();

            //  انشاء مركز صيانة وتطبيق تعدد الأشكال
            RepairCenter center = new RepairCenter();
            // تعبئة بيانات الثلاجة 
            Refrigerator fridge = new Refrigerator("RT43","SN10001");
            fridge.Brand = "Samsung";
            fridge.OwnerName = "Ahmad";
            fridge.HasFreezer = true;
            center.AddDevice(fridge);

            // تعبئة بيانات المكيف  
            AirConditioner ac = new AirConditioner("Dual Cool","SN10002");
            ac.Brand = "LG";
            ac.OwnerName = "Sara";
            ac.IsInverter = true;
            center.AddDevice(ac);

            // تعبئة بيانات الغسالة 
            WashingMachine wm = new WashingMachine("Front Load","SN10003");
            wm.Brand = "Bosch";
            wm.OwnerName = "omar";
            wm.HasAutomaticDryer = false;
            center.AddDevice(wm);
            ac.RepairCompleted += ShowMessage;//يظهر عند الانتهاء من اصلاح المكيف
            ac.CompleteRepair();//تم اصلاح المكيف فانطلق الحدث وظهر الاشعار

            center.ShowAllDevices();
            double totalCost = center.GetTotalCost();

            // طباعة العدد الإجمالي والتكلفة الكلية
            Console.WriteLine();
            Console.WriteLine("Total number of devices: " + Device.TotalAppliancesCount);
            Console.WriteLine("Total cost of all devices:  $" + totalCost);
            Console.WriteLine("Repaired devices: " + Device.RepairedCount);
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine("Notification: " + message);
        }
    }
}
