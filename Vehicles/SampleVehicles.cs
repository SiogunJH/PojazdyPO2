using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVehicles;
using Base;

namespace SampleVehicles
{
    public class Car : BaseVehicle, IGroundVehicle
    {
        public int numOfWheels { get; private set; }
        
        public Car(string name, int numOfWheels, double horsepower, IVehicle.Fuel fuel)
        {
            this.name = name;
            this.numOfWheels = numOfWheels;
            this.horsepower = horsepower;
            this.fuel = fuel;
        }
    }

    public class Plane : BaseVehicle, IGroundVehicle, IAirVehicle
    {
        public int numOfWheels { get; private set; }
        public Plane(string name, int numOfWheels, double horsepower, IVehicle.Fuel fuel)
        {
            this.name = name;
            this.numOfWheels = numOfWheels;
            this.horsepower = horsepower;
            this.fuel = fuel;
        }
    }

    public class ATV : BaseVehicle, IGroundVehicle, IWaterVehicle
    {
        public int numOfWheels { get; private set; }
        public double displacement { get; private set; }
        public ATV(string name, int numOfWheels, double displacement, double horsepower, IVehicle.Fuel fuel)
        {
            this.name = name;
            this.numOfWheels = numOfWheels;
            this.displacement = displacement;
            this.horsepower = horsepower;
            this.fuel = fuel;
        }
    }
}
