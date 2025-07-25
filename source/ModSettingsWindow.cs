using UnityEngine;
using LessUI;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public static class ModSettingsWindow
    {
        public static void Draw(Rect parent)
        {
            Canvas canvas = new Canvas(parent);
            Stack stack = new Stack();
            LabeledCheckbox organHarvestNegativeGoodwillCheckbox = new LabeledCheckbox("SK.noprisonersympathy.ModSettings.organHarvsetingNegativeCheckboxLabel".Translate(),  ModSettings.disableOrganHarvestingNegativeGoodwill);
            organHarvestNegativeGoodwillCheckbox.Tooltip = "SK.noprisonersympathy.ModSettings.organHarvsetingNegativeCheckboxTooltip".Translate();
            stack.AddChild(organHarvestNegativeGoodwillCheckbox);
            canvas.AddChild(stack);
            canvas.Render();
        }
    }
}
