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