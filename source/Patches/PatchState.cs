using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class PatchState
    {
        public static Pawn lastVictim;

        public static readonly List<PreceptDef> blacklistExecutionPrecepts = new List<PreceptDef>();
        public static readonly List<HistoryEventDef> targetExecutionHistoryEvents = new List<HistoryEventDef>();

        public static readonly List<PreceptDef> blacklistSlaveryPrecepts = new List<PreceptDef>();
        public static readonly List<HistoryEventDef> targetSlaveryHistoryEvents = new List<HistoryEventDef>();

        public static readonly List<PreceptDef> blacklistOraganHarvestingPrecepts = new List<PreceptDef>();
        public static readonly List<HistoryEventDef> targetOraganHarvestingHistoryEvents = new List<HistoryEventDef>();

        public static readonly List<PreceptDef> blacklistCannibalismPrecepts = new List<PreceptDef>();
        public static readonly List<HistoryEventDef> targetCannibalismHistoryEvents = new List<HistoryEventDef>();

        public static readonly Dictionary<string, (List<PreceptDef> Precepts, List<HistoryEventDef> Events)> modDefined = new Dictionary<string, (List<PreceptDef>, List<HistoryEventDef>)>();

        public static bool modifyMoodOffset;

        public static bool ShouldSuppressThought(HistoryEvent ev, Precept precept)
        {
            if (precept == null)
            {
                return false;
            }

            HistoryEventDef eventDef = ev.def;
            PreceptDef preceptDef = precept.def;
            bool lastVictimIsPrisoner = lastVictim != null && lastVictim.IsPrisoner;

            if (targetExecutionHistoryEvents.Contains(eventDef) && blacklistExecutionPrecepts.Contains(preceptDef))
            {
                return true;
            }

            if (targetSlaveryHistoryEvents.Contains(eventDef) && blacklistSlaveryPrecepts.Contains(preceptDef))
            {
                return true;
            }

            if (targetOraganHarvestingHistoryEvents.Contains(eventDef) && blacklistOraganHarvestingPrecepts.Contains(preceptDef) && lastVictimIsPrisoner)
            {
                return true;
            }

            if (targetCannibalismHistoryEvents.Contains(eventDef) && blacklistCannibalismPrecepts.Contains(preceptDef) && lastVictimIsPrisoner)
            {
                return true;
            }

            if (lastVictimIsPrisoner)
            {
                foreach ((List<PreceptDef> precepts, List<HistoryEventDef> historyEvents) in modDefined.Values)
                {
                    if (historyEvents.Contains(eventDef) && precepts.Contains(preceptDef))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}