using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public class Mod: Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            Harmony instance = new Harmony("rimworld.sk.noprisonersympathy");
            HarmonyPatcher.instance = instance;

            // Fires when all Defs are loaded
            LongEventHandler.ExecuteWhenFinished(Init);
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
            HarmonyPatcher.PatchVanillaMethods();

            // Executio
            List<string> blacklistedExecutionPreceptDefNames = new List<string>() { "Execution_Classic", "Execution_HorribleIfInnocent", "Execution_Horrible", "Execution_Abhorrent" };
            Patches.blacklistExecutionPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedExecutionPreceptDefNames.Contains(def.defName)));
            Patches.targetExecutionHistoryEvents = new List<HistoryEventDef>() { HistoryEventDefOf.ExecutedPrisoner, HistoryEventDefOf.ExecutedPrisonerGuilty, HistoryEventDefOf.ExecutedPrisonerInnocent, HistoryEventDefOf.InnocentPrisonerDied };

            // Slavery
            List<string> blacklistedSlaveryPreceptDefNames = new List<string>() { "Slavery_Classic", "Slavery_Abhorrent", "Slavery_Horrible", "Slavery_Disapproved" };
            Patches.blacklistSlaveryPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedSlaveryPreceptDefNames.Contains(def.defName)));
            Patches.targetSlaveryHistoryEvents = new List<HistoryEventDef>() { HistoryEventDefOf.SoldSlave, HistoryEventDefOf.EnslavedPrisoner, HistoryEventDefOf.EnslavedPrisonerNotPreviouslyEnslaved };

            // Organ Harvesting
            List<string> blacklistedOrganHarvestingPreceptDefNames = new List<string>() { "OrganUse_Classic", "OrganUse_HorribleNoSell", "OrganUse_HorribleSellOK", "OrganUse_Abhorrent" };
            Patches.blacklistOraganHarvestingPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedOrganHarvestingPreceptDefNames.Contains(def.defName)));
            Patches.targetOraganHarvestingHistoryEvents = new List<HistoryEventDef>() { HistoryEventDefOf.HarvestedOrgan, HistoryEventDefOf.HarvestedOrganFromGuest };

            // Cannibalism
            List<string> blacklistedCannibalismPreceptDefNames = new List<string>() { "Cannibalism_Classic", "Cannibalism_Abhorrent", "Cannibalism_Horrible", "Cannibalism_Disapproved" };
            Patches.blacklistCannibalismPrecepts.AddRange(DefDatabase<PreceptDef>.AllDefsListForReading.FindAll(def => blacklistedCannibalismPreceptDefNames.Contains(def.defName)));
            Patches.targetCannibalismHistoryEvents = new List<HistoryEventDef>() { HistoryEventDefOf.ButcheredHuman };
        }
    }
}
