using HugsLib.Source.Detour;
using RimWorld;
using System.Reflection;
using Verse;
using LockableDoors.Buildings;

namespace LockableDoors.Detoured
{
    internal static class _Pawn_Ownership
    {
        [DetourMethod(typeof(Pawn_Ownership), "UnclaimAll")]
        internal static void UnclaimAll(this Pawn_Ownership self)
        {
            self.UnclaimBed();
            self.UnclaimGrave();
            self.UnclaimDoors();
        }

        internal static void UnclaimDoors(this Pawn_Ownership self)
        {
            FieldInfo field = typeof(Pawn_Ownership).GetField("pawn", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
            {
                Pawn pawn = (Pawn)field.GetValue(self);

                // At this point chances are Pawn.Destroy was invoked to call UnclaimAll -> UnclaimDoors anyway
                // Pawn.Destroy will invoke its parent Thing.Destroy which in turn sets the Thing.mapIndexOrState of the pawn to -2
                // As Pawn.Map is actually a property that retrieves the Map using Thing.mapIndexOrState, it is not possible to get
                // the pawn's map anymore. Thus we must loop through every map... unless someone has a better idea?
                foreach(Map map in Find.Maps)
                    foreach (Building_LockableDoor building in map.listerBuildings.AllBuildingsColonistOfClass<Building_LockableDoor>())
                        building.TryUnassignPawn(pawn);
            }
            else
                Log.Error("[LockableDoors] Could not reflect private pawn member at UnclaimDoors.");
        }
    }
}
