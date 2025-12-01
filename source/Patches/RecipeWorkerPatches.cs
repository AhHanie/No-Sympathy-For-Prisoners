using HarmonyLib;
using RimWorld;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class RecipeWorkerPatches
    {
        [HarmonyPatch(typeof(RecipeWorker), "ReportViolation")]
        public static class ReportViolation
        {
            public static bool Prepare()
            {
                return ModSettings.disableOrganHarvestingNegativeGoodwill;
            }

            public static bool Prefix(Pawn pawn, Pawn billDoer, Faction factionToInform, int goodwillImpact, HistoryEventDef overrideEventDef)
            {
                if (overrideEventDef == null && factionToInform != null && billDoer != null && billDoer.Faction == Faction.OfPlayer && pawn.IsPrisoner)
                {
                    return false;
                }

                return true;
            }
        }
    }
}