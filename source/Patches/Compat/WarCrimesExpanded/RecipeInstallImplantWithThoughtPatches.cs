using HarmonyLib;
using SK_No_Sympathy_For_Prisoners.Compat;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches.Compat.WarCrimesExpanded
{
    [HarmonyPatch()]
    public static class RecipeInstallImplantWithThoughtPatches
    {
        [HarmonyPatch("WarCrimesExpanded2.Recipe_InstallImplantWithThought", "ApplyOnPawn")]
        public static class ApplyOnPawn
        {
            public static bool Prepare()
            {
                return WarCrimesExpandedCompat.ShouldPatchType("WarCrimesExpanded2.Recipe_InstallImplantWithThought");
            }
            public static void Prefix(Pawn victim)
            {
                PatchState.lastVictim = victim;
            }
        }
    }
}