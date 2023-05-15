using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Utility
    {
        public static double RestrictBetween(this double speed, double min, double max) => Math.Max(min, Math.Min(max, speed));
        public static double TranslateTerrainSpeed(this double speed, IVehicle.Terrain from, IVehicle.Terrain to)
        {
            // translate to km/h
            switch (from)
            {
                case IVehicle.Terrain.Water:
                    speed *= 0.54f;
                    break;
                case IVehicle.Terrain.Air:
                    speed *= (5.0f / 18.0f);
                    break;
            }

            switch (to)
            {
                case IVehicle.Terrain.Ground:
                    return speed;
                case IVehicle.Terrain.Water:
                    return speed * 1.852f;
                case IVehicle.Terrain.Air:
                    return speed * (18.0f / 5.0f);
            }
            throw new ArgumentException($"Unhandled terrain types: {to}");
        }
    }
}
