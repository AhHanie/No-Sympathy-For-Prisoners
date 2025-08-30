using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public class Patches
    {
        public static Pawn lastVictim;

        // Execution
        public static List<PreceptDef> blacklistExecutionPrecepts = new List<PreceptDef>();
        public static List<HistoryEventDef> targetExecutionHistoryEvents;

        // Slavery
        public static List<PreceptDef> blacklistSlaveryPrecepts = new List<PreceptDef>();
        public static List<HistoryEventDef> targetSlaveryHistoryEvents;

        // Organ Harvesting
        public static List<PreceptDef> blacklistOraganHarvestingPrecepts = new List<PreceptDef>();
        public static List<HistoryEventDef> targetOraganHarvestingHistoryEvents;

        // Cannibalism
        public static List<PreceptDef> blacklistCannibalismPrecepts = new List<PreceptDef>();
        public static List<HistoryEventDef> targetCannibalismHistoryEvents;

        // Mod Defined
        public static Dictionary<string, (List<PreceptDef>, List<HistoryEventDef>)> modDefined = new Dictionary<string, (List<PreceptDef>, List<HistoryEventDef>)>();

        public static bool modifyMoodOffset = false;

        public static void GiveThoughtsForPawnOrganHarvestedPrefix(Pawn victim)
        {
            lastVictim = victim;
        }

        public static bool NotifyMemberWitnessedActionPrefix(HistoryEvent ev, Precept precept)
        {
            if (targetExecutionHistoryEvents.Contains(ev.def) && blacklistExecutionPrecepts.Contains(precept.def))
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetSlaveryHistoryEvents.Contains(ev.def) && blacklistSlaveryPrecepts.Contains(precept.def))
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetOraganHarvestingHistoryEvents.Contains(ev.def) && blacklistOraganHarvestingPrecepts.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetCannibalismHistoryEvents.Contains(ev.def) && blacklistCannibalismPrecepts.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            foreach ((List<PreceptDef>, List<HistoryEventDef>) preceptsHistory in modDefined.Values)
            {
                if (preceptsHistory.Item2.Contains(ev.def) && preceptsHistory.Item1.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
                {
                    if (ModSettings.affectMoodInstead)
                    {
                        modifyMoodOffset = true;
                        return true;
                    }
                    return false;
                }
            }

            return true;
        }

        public static void NotifyMemberWitnessedActionPostfix()
        {
            modifyMoodOffset = false;
        }

        public static bool NotifyMemberTookActionPrefix(HistoryEvent ev, Precept precept)
        {
            if (targetExecutionHistoryEvents.Contains(ev.def) && blacklistExecutionPrecepts.Contains(precept.def))
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetSlaveryHistoryEvents.Contains(ev.def) && blacklistSlaveryPrecepts.Contains(precept.def))
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetOraganHarvestingHistoryEvents.Contains(ev.def) && blacklistOraganHarvestingPrecepts.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            if (targetCannibalismHistoryEvents.Contains(ev.def) && blacklistCannibalismPrecepts.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
            {
                if (ModSettings.affectMoodInstead)
                {
                    modifyMoodOffset = true;
                    return true;
                }
                return false;
            }

            foreach ((List<PreceptDef>, List<HistoryEventDef>) preceptsHistory in modDefined.Values)
            {
                if (preceptsHistory.Item2.Contains(ev.def) && preceptsHistory.Item1.Contains(precept.def) && lastVictim != null && lastVictim.IsPrisoner)
                {
                    if (ModSettings.affectMoodInstead)
                    {
                        modifyMoodOffset = true;
                        return true;
                    }
                    return false;
                }
            }

            return true;
        }

        public static void NotifyMemberTookActionPostfix()
        {
            modifyMoodOffset = false;
        }

        public static void TryGainMemoryPostfix(MemoryThoughtHandler __instance, Thought_Memory newThought)
        {
            if (modifyMoodOffset)
            {
                Thought_Memory mem = __instance.GetFirstMemoryOfDef(newThought.def);
                float currentOffset = mem.MoodOffset();
                if (currentOffset < 0)
                {
                    // Calculate counteract value to make final offset = currentOffset * (percentage/100)
                    float counteract = -currentOffset * (ModSettings.moodReductionPercentage / 100f);
                    newThought.moodOffset = Mathf.RoundToInt(counteract);
                }
            }
        }


        public static void ButcherProductsPrefixPatch(Corpse __instance)
        {
            if (__instance.InnerPawn.IsPrisoner)
            {
                lastVictim = __instance.InnerPawn;
            }
        }

        // Banished Patches
        public static IEnumerable<CodeInstruction> GetBanishPawnDialogTextTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            // Find the IsQuestLodger call and insert our prisoner check
            for (int i = 0; i < codes.Count; i++)
            {
                // Look for the IsQuestLodger method call
                if (codes[i].opcode == OpCodes.Call &&
                    codes[i].operand is MethodInfo method &&
                    method.Name == "IsQuestLodger")
                {
                    // The next instruction should be brtrue.s that branches to skip the if block
                    if (i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Brtrue_S)
                    {
                        var skipLabel = codes[i + 1].operand; // This is the label to skip the if block

                        // Insert our prisoner check after the IsQuestLodger branch
                        var insertIndex = i + 2;

                        var newInstructions = new List<CodeInstruction>
                        {
                            new CodeInstruction(OpCodes.Ldarg_0), // Load banishedPawn parameter
                            new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Pawn), "IsPrisoner")), // Call get_IsPrisoner()
                            new CodeInstruction(OpCodes.Brtrue_S, skipLabel) // If IsPrisoner is true, skip to the same label as IsQuestLodger
                        };

                        codes.InsertRange(insertIndex, newInstructions);
                        break;
                    }
                }
            }

            return codes;
        }

        // Banished Patches
        public static bool TryGiveThoughtsPrefixPatch(Pawn victim, PawnDiedOrDownedThoughtsKind thoughtsKind)
        {
            if (thoughtsKind == PawnDiedOrDownedThoughtsKind.BanishedToDie && victim.IsPrisoner)
            {
                return false;
            }
            return true;
        }

        public static bool ReportViolationPrefixPatch(Pawn pawn, Pawn billDoer, Faction factionToInform, int goodwillImpact, HistoryEventDef overrideEventDef)
        {
            if (overrideEventDef == null && factionToInform != null && billDoer != null && billDoer.Faction == Faction.OfPlayer && pawn.IsPrisoner)
            {
                return false;
            }
            return true;
        }

    }
}