using System;
using System.Text;
using Utility;
using IVehicles;

namespace Base
{
    public interface IVehicle
    {
        // ENUMS
        enum Terrain { Ground, Water, Air }
        enum Status { On, Off }
        enum Fuel { Gasoline, Oil, LPG, Electricity}

        // DECORATIONS
        string name { get; }

        // MOVEMENT
        double speed { get; }
        void ChangeSpeed(double changeBy);
        Terrain movingThrough { get; }
        Status status { get; }
        bool isStill { get; }
        bool isOn { get; }

        // ENGINE
        bool hasEngine { get; }
        double? horsepower { get; }
        Fuel? fuel { get; }


        // TURN ON/OFF
        void TurnOn();
        void TurnOff();

        // OTHER
        string ToString();
    }

    public abstract class BaseVehicle : IVehicle
    {
        #region DECORATIONS
        public string name { get; protected set; } = "Unidentified Vehicle";
        #endregion

        #region MOVEMENT
        public double speed { get; protected set; } = 0;
        public void ChangeSpeed(double changeBy)
        {
            if (!isOn) return;

            changeBy += speed;
            switch (movingThrough)
            {
                case IVehicle.Terrain.Ground:
                    if (this is IGroundVehicle) 
                    { 
                        speed = changeBy.RestrictBetween(0, 350);
                    }
                    break;
                case IVehicle.Terrain.Water:
                    if (this is IGroundVehicle) 
                    { 
                        speed = changeBy.RestrictBetween(0, 40);
                    }
                    break;
                case IVehicle.Terrain.Air:
                    if (this is IGroundVehicle) 
                    { 
                        speed = changeBy.RestrictBetween(20, 200); 
                    }
                    break;
                default: throw new ArgumentException($"Unhandled terrain type of {movingThrough}"!);
            }
        }
        public IVehicle.Terrain movingThrough { get; protected set; } = IVehicle.Terrain.Ground;
        public IVehicle.Status status {  get; protected set; } = IVehicle.Status.Off;
        public bool isStill { get => speed == 0; }
        public bool isOn { get => status == IVehicle.Status.On; }
        #endregion

        #region GO TO TERRAIN
        public void GoGround()
        {
            if (this is IGroundVehicle) 
            { 
                speed = speed.TranslateTerrainSpeed(movingThrough, IVehicle.Terrain.Ground).RestrictBetween(0,350);
                movingThrough = IVehicle.Terrain.Ground;
            }
        }
        public void GoWater()
        {
            if (this is IWaterVehicle) 
            { 
                speed = speed.TranslateTerrainSpeed(movingThrough, IVehicle.Terrain.Water).RestrictBetween(0, 40);
                movingThrough = IVehicle.Terrain.Water;
            }
        }
        public void GoAir()
        {
            if (this is IAirVehicle && speed.TranslateTerrainSpeed(movingThrough, IVehicle.Terrain.Air)>=20)
            {
                speed = speed.TranslateTerrainSpeed(movingThrough, IVehicle.Terrain.Air).RestrictBetween(20,200);
                movingThrough = IVehicle.Terrain.Air;
            }
        }
        #endregion

        #region ENGINE
        public bool hasEngine { get => horsepower != null; }
        public double? horsepower { get; protected set; } = null;
        public IVehicle.Fuel? fuel { get; protected set; } = null;
        #endregion

        #region TURN ON/OFF
        public void TurnOff() { if (isOn && isStill) status = IVehicle.Status.Off; }
        public void TurnOn() { if (!isOn) status = IVehicle.Status.On; }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // NAME
            sb.AppendLine($"\n --==## {name} ##==--");
            
            // WHAT TERRAIN CAN IT MOVE IN
            sb.Append($"Can move through: ");
            if (this is IGroundVehicle) sb.Append("Ground, ");
            if (this is IWaterVehicle) sb.Append("Water, ");
            if (this is IAirVehicle) sb.Append("Air, ");
            sb.AppendLine();

            // STILL OR MOVING
            sb.AppendLine($"Currently on/in: {movingThrough}");
            sb.AppendLine($"Is moving? {!isStill}");

            // IF MOVING, AT WHAT SPEED
            if (!isStill)
            {
                sb.Append($"Current Speed: {speed} ");
                switch (movingThrough)
                {
                    case IVehicle.Terrain.Ground:
                        sb.AppendLine($"km/h (0-350)"); break;
                    case IVehicle.Terrain.Water:
                        sb.AppendLine($"knots (0-40)"); break;
                    case IVehicle.Terrain.Air:
                        sb.AppendLine($"m/s (20-200)"); break;
                }
            }

            // ENGINE
            if (!hasEngine) sb.AppendLine("No Engine");
            else
            {
                sb.AppendLine($"{horsepower} Horsepower");
                sb.AppendLine($"Fuel: {fuel}");
            }

            // EXCLUSIVE PROPERTIES
            if (this is IGroundVehicle gVehicle) sb.AppendLine($"Number of Wheels: {gVehicle.numOfWheels}");
            if (this is IWaterVehicle wVehicle) sb.AppendLine($"Displacement: {wVehicle.displacement} m^3");

            // FINALIZE
            return sb.ToString();
        }
        #endregion
    }
}