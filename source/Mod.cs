using HarmonyLib;
using RimWorld;
using SK_No_Sympathy_For_Prisoners.Compat;
using SK_No_Sympathy_For_Prisoners.Patches;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public class Mod: Verse.Mod
    {
        public static Harmony instance;
        public Mod(ModContentPack content) : base(content)
        {
            instance = new Harmony("rimworld.sk.noprisonersympathy");

            LongEventHandler.QueueLongEvent(Init, "SK_No_Sympathy_For_Prisoners.Init", true, null);
        }

        public override string SettingsCategory()
        {
            return "No Sympathy For Prisoners";
        }

        public override void DoSettingsWindowContents(Rect rect)
        {
            ModSettingsWindow.Draw(rect);
            base.DoSettingsWindowContents(rect);
        }

        public void Init()
        {
            GetSettings<ModSettings>();
            if (WarCrimesExpandedCompat.IsActive())
            {
                WarCrimesExpandedCompat.Init();
            }

            instance.PatchAll();

            // Executio
            List<string> blacklistedExecutionPreceptDefNames = new List<string>() { "Execution_Classic", "Execution_HorribleIfInnocent", "Execution_Horrible", "Execution_Abhorrent" };
            PatchState.blacklistExecutionPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedExecutionPreceptDefNames.Contains(def.defName)));
            PatchState.targetExecutionHistoryEvents.AddRange(new List<HistoryEventDef>() { HistoryEventDefOf.ExecutedPrisoner, HistoryEventDefOf.ExecutedPrisonerGuilty, HistoryEventDefOf.ExecutedPrisonerInnocent, HistoryEventDefOf.InnocentPrisonerDied });

            // Slavery
            List<string> blacklistedSlaveryPreceptDefNames = new List<string>() { "Slavery_Classic", "Slavery_Abhorrent", "Slavery_Horrible", "Slavery_Disapproved" };
            PatchState.blacklistSlaveryPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedSlaveryPreceptDefNames.Contains(def.defName)));
            PatchState.targetSlaveryHistoryEvents.AddRange(new List<HistoryEventDef>() { HistoryEventDefOf.SoldSlave, HistoryEventDefOf.EnslavedPrisoner, HistoryEventDefOf.EnslavedPrisonerNotPreviouslyEnslaved });

            // Organ Harvesting
            List<string> blacklistedOrganHarvestingPreceptDefNames = new List<string>() { "OrganUse_Classic", "OrganUse_HorribleNoSell", "OrganUse_HorribleSellOK", "OrganUse_Abhorrent" };
            PatchState.blacklistOraganHarvestingPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedOrganHarvestingPreceptDefNames.Contains(def.defName)));
            PatchState.targetOraganHarvestingHistoryEvents.AddRange(new List<HistoryEventDef>() { HistoryEventDefOf.HarvestedOrgan, HistoryEventDefOf.HarvestedOrganFromGuest });

            // Cannibalism
            List<string> blacklistedCannibalismPreceptDefNames = new List<string>() { "Cannibalism_Classic", "Cannibalism_Abhorrent", "Cannibalism_Horrible", "Cannibalism_Disapproved" };
            PatchState.blacklistCannibalismPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedCannibalismPreceptDefNames.Contains(def.defName)));
            PatchState.targetCannibalismHistoryEvents.Add(HistoryEventDefOf.ButcheredHuman);
        }
    }
}
