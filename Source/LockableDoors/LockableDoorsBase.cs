using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using HugsLib;
using System.Reflection;
using HugsLib.Utils;
using HugsLib.Settings;
using HugsLib.Source.Detour;
using LockableDoors.Detoured;

namespace LockableDoors
{
    public class LockableDoorsBase : HugsLib.ModBase
    {
        public override string ModIdentifier
        {
            get
            {
                return "LockableDoors";
            }
        }
    }
}
