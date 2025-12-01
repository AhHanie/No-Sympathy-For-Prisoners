
using HarmonyLib;
using RimWorld;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class ThoughtUtilityPatches
    {
        [HarmonyPatch(typeof(ThoughtUtility), "GiveThoughtsForPawnOrganHarvested")]
        public static class GiveThoughtsForPawnOrganHarvested
        {
            public static void Prefix(Pawn victim)
            {
                PatchState.lastVictim = victim;
            }
        }
    }
}
