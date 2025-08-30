using UnityEngine;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public static class ModSettingsWindow
    {
        public static void Draw(Rect parent)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(parent);

            // Organ harvesting negative goodwill checkbox
            listing.CheckboxLabeled("SK.noprisonersympathy.ModSettings.organHarvsetingNegativeCheckboxLabel".Translate(),
                                   ref ModSettings.disableOrganHarvestingNegativeGoodwill,
                                   "SK.noprisonersympathy.ModSettings.organHarvsetingNegativeCheckboxTooltip".Translate());

            listing.Gap(12f);

            // Affect mood instead checkbox
            listing.CheckboxLabeled("SK.noprisonersympathy.ModSettings.affectMoodInsteadLabel".Translate(),
                                   ref ModSettings.affectMoodInstead,
                                   "SK.noprisonersympathy.ModSettings.affectMoodInsteadTooltip".Translate());

            // Mood reduction percentage slider (only show if affectMoodInstead is enabled)
            if (ModSettings.affectMoodInstead)
            {
                listing.Gap(6f);

                string sliderLabel = "SK.noprisonersympathy.ModSettings.moodReductionPercentageLabel".Translate() + ": " + ModSettings.moodReductionPercentage.ToString("F0") + "%";
                listing.Label(sliderLabel);

                ModSettings.moodReductionPercentage = listing.Slider(ModSettings.moodReductionPercentage, 10f, 90f);

                listing.Gap(6f);
                listing.Label("SK.noprisonersympathy.ModSettings.moodReductionPercentageTooltip".Translate(), -1f,
                             "SK.noprisonersympathy.ModSettings.moodReductionPercentageTooltip".Translate());
            }

            listing.End();
        }
    }
}