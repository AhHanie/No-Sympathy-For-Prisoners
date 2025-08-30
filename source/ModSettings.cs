using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool disableOrganHarvestingNegativeGoodwill = false;
        public static bool affectMoodInstead = false;
        public static float moodReductionPercentage = 40f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref disableOrganHarvestingNegativeGoodwill, "disableOrganHarvestingNegativeGoodwill", false);
            Scribe_Values.Look(ref affectMoodInstead, "affectMoodInstead", false);
            Scribe_Values.Look(ref moodReductionPercentage, "moodReductionPercentage", 40f);
        }
    }
}