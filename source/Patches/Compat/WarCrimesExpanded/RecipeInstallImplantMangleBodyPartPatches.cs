using HarmonyLib;
using SK_No_Sympathy_For_Prisoners.Compat;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches.Compat.WarCrimesExpanded
{
    public static class RecipeInstallImplantMangleBodyPartPatches
    {
        [HarmonyPatch("WarCrimesExpanded2.Recipe_InstallImplant_MangleBodyPart", "ApplyOnPawn")]
        public static class ApplyOnPawn
        {
            public static bool Prepare()
            {
                return WarCrimesExpandedCompat.ShouldPatchType("WarCrimesExpanded2.Recipe_InstallImplant_MangleBodyPart");
            }
            public static void Prefix(Pawn victim)
            {
                PatchState.lastVictim = victim;
            }
        }
    }
}