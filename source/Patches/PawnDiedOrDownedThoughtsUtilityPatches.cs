using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class PawnDiedOrDownedThoughtsUtilityPatches
    {
        [HarmonyPatch(typeof(PawnDiedOrDownedThoughtsUtility))]
        public static class TryGiveThoughts
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                yield return AccessTools.FirstMethod(typeof(PawnDiedOrDownedThoughtsUtility), method => method.Name.Contains("TryGiveThoughts"));
            }

            public static bool Prefix(Pawn victim, PawnDiedOrDownedThoughtsKind thoughtsKind)
            {
                if (thoughtsKind == PawnDiedOrDownedThoughtsKind.BanishedToDie && victim.IsPrisoner)
                {
                    return false;
                }

                return true;
            }
        }
    }
}