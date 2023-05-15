using SampleVehicles;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var mercedes = new Car("Mercedes", 4, 1000, Base.IVehicle.Fuel.Gasoline);
            mercedes.TurnOn();
            mercedes.ChangeSpeed(30);
            Console.WriteLine(mercedes);

            var polonez = new Car("Polonez", 4, 1200, Base.IVehicle.Fuel.Oil);
            polonez.TurnOn();
            polonez.ChangeSpeed(130);
            polonez.ChangeSpeed(-30);
            polonez.ChangeSpeed(-100);
            polonez.TurnOff();
            Console.WriteLine(polonez);

            var fiat = new Car("Fiat", 4, 1300, Base.IVehicle.Fuel.Electricity);
            fiat.TurnOn();
            fiat.ChangeSpeed(230);
            fiat.TurnOff(); // won't work, because the vehicle is not still
            Console.WriteLine(fiat);

            var plane = new Plane("Small Plane", 6, 12300, Base.IVehicle.Fuel.LPG);
            plane.TurnOn();
            plane.ChangeSpeed(6);
            plane.GoAir();
            plane.ChangeSpeed(-plane.speed);
            Console.WriteLine(plane);

            var atv = new ATV("ATV", 10, 100, 5000, Base.IVehicle.Fuel.Electricity);
            atv.TurnOn();
            atv.ChangeSpeed(10);
            atv.GoWater();
            atv.ChangeSpeed(-atv.speed);
            atv.TurnOff();
            Console.WriteLine(atv);
        }
    }
}