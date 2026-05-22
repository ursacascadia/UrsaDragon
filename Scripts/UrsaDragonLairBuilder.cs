using Genkit;
using System;
using XRL;
using XRL.World;
using XRL.World.Parts;
using XRL.World.WorldBuilders;
using XRL.World.ZoneBuilders;

namespace Ursa.Dragon
{
    [JoppaWorldBuilderExtension]
    public class UrsaDragonLairBuilder : IJoppaWorldBuilderExtension
    {
        public override void OnAfterMutableInit(JoppaWorldBuilder builder)
        {
            Zone WorldZone = The.ZoneManager.GetZone("JoppaWorld");
            MutabilityMap mutableMap = builder.mutableMap;

            // define array of valid locations

            int[] locations_x = {24,43,64,65,70,77};
            int[] locations_y = {0, 0, 0, 0, 4, 0 };

            Random rnd = new Random();
            int rndint = rnd.Next(6);

            Location2D loc = Location2D.Get(locations_x[rndint]*3, locations_y[rndint]*3);

            // Failsafe cases
            if (mutableMap.GetMutable(loc) == 0){ // If location isn't mutable (0), cycle through locations until we find one that is.
                for (int i=0; i<6; i++)
                {
                    loc = Location2D.Get(locations_x[i]*3, locations_y[i]*3);
                    if (mutableMap.GetMutable(loc) == 1)
                    {
                        break;
                    }
                }
                if (mutableMap.GetMutable(loc) == 0){ // if all else fails, just pop random mountain terrain.
                    loc = builder.popMutableBlockOfTerrain("Mountains");
                }
            }

            // Place the den in a random cardinality to the center of the parasang
            loc = Location2D.Get(loc.X + rnd.Next(3), loc.Y);
            loc = Location2D.Get(loc.X, loc.Y + rnd.Next(3));

            string zoneid = builder.ZoneIDFromXY("JoppaWorld", loc.X, loc.Y);

            The.ZoneManager.AddZoneBuilder(zoneid, 4900, "ClearAll");
            The.ZoneManager.AddZonePostBuilder(zoneid, "MapBuilder", "FileName", "Ursa_Dragon_Lair.rpm");
            The.ZoneManager.SetZoneName(zoneid, "Azhdahak's den", null, null, null, null, Proper: true);

            string lair_secret = builder.AddSecret(
                zoneid,
                "Azhdahak's den",
                new string[2] { "lair", "dragon" },
                "Lairs",
                "$Ursa_Dragon_Lair"
            );
            
            builder.mutableMap.SetMutable(loc, 0);
        }

    }
}