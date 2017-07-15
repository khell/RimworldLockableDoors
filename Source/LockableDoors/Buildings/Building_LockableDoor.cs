using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI.Group;
using System.Text;

namespace LockableDoors.Buildings
{
    class Building_LockableDoor : Building_Door, IAssignableBuilding
    {
        public List<Pawn> owners = new List<Pawn>();

        public IEnumerable<Pawn> AssignedPawns
        {
            get
            {
                return this.owners;
            }
        }

        public IEnumerable<Pawn> AssigningCandidates
        {
            get
            {
                if (!base.Spawned)
                    return Enumerable.Empty<Pawn>();
                return base.Map.mapPawns.FreeColonists;
            }
        }

        public int MaxAssignedPawnsCount
        {
            get
            {
                return base.Map.mapPawns.FreeColonistsSpawnedCount;
            }
        }

        public void TryAssignPawn(Pawn pawn)
        {
            this.owners.Add(pawn);
        }

        public void TryUnassignPawn(Pawn pawn)
        {
            this.owners.Remove(pawn);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
                yield return g;

            yield return new Command_Action
            {
                defaultLabel = "LockableDoors_DoorSetUnlockableLabel".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/AssignOwner", true),
                defaultDesc = "LockableDoors_DoorSetUnlockableDesc".Translate(),
                action = delegate
                {
                    Find.WindowStack.Add(new Dialog_AssignBuildingOwner(this));
                },
                hotKey = KeyBindingDefOf.Misc3
            };
        }

        public override bool PawnCanOpen(Pawn pawn)
        {
            // A pawn can open if normal door conditions are met, and no owners are assigned or they are assigned as an owner.
            return base.PawnCanOpen(pawn) && (this.owners.Contains(pawn) || !this.owners.Any());
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look<Pawn>(ref this.owners, "owners", LookMode.Reference, new object[0]);
        }

        public override string GetInspectString()
        {
            var builder = new StringBuilder(base.GetInspectString());

            builder.Append("LockableDoors_InspectUnlockableLabel".Translate());

            if (!this.owners.Any())
                builder.Append("LockableDoors_InspectUnlockableEveryoneLabel".Translate());
            else
            {
                bool flag = false;
                for (int i = 0; i < this.owners.Count; i++)
                {
                    if (flag)
                        builder.Append(", ");
                    flag = true;

                    builder.Append(this.owners[i].LabelShort);
                }
            }

            return builder.ToString();
        }
    }
}
