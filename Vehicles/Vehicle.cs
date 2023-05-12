using System;

namespace Vehicles
{
    public interface IVehicle
    {
        // ENUMS
        enum Terrain { Still, Ground, Water, Air }
        enum Status { On, Off }

        // DECORATIONS
        string name { get; }

        // MOVEMENT
        List<Terrain> canMoveThorough { get; }
        Terrain movingThrough { get; }
        int speed { get; }
        Status status { get; }

        // TURN ON/OFF
        void TurnOn();
        void TurnOff();

        // OTHER
        string GetSpeed(); // take speed and terrain though which the vehicle is traversing and display it as string
    }
}