using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using Coop.GUI;

namespace BannerlordCoop.HarmonyPatches
{
    [HarmonyPatch(typeof(PlayerEncounter), "Init")]
    public static class CoopEncounterPatch
    {
        // "Prefix" runs BEFORE the game's original code.
        public static bool Prefix(PartyBase attackerParty, PartyBase defenderParty, Settlement settlement)
        {
            // 1. Check if both sides are players (You will need to implement the actual check later)
            bool isAttackerPlayer = CoopManager.IsPlayerParty(attackerParty);
            bool isDefenderPlayer = CoopManager.IsPlayerParty(defenderParty);

            if (isAttackerPlayer && isDefenderPlayer)
            {
                if (Campaign.Current != null && Campaign.Current.GameStarted)
                {
                    // THE NEW CODE:
                    var layer = new CoopEncounterLayer();

                    // The VM needs to know how to close the window
                    var vm = new CoopEncounterVM(() =>
                    {
                        layer.UnloadLayer();
                        // Resume game map interaction if it was paused
                        // PlayerEncounter.LeaveEncounter = true; 
                    });

                    // Glue them together
                    layer.LoadLayer(vm);

                    CoopLog.Info("Coop Encounter UI Launched");
                }

                return false; // Skip original method
            }

            // 3. Let the game proceed as normal for AI
            return true;
        }
    }

    // Placeholder classes to satisfy the compiler until real implementation is available.
    public static class CoopManager
    {
        public static bool IsPlayerParty(PartyBase party) { return false; }
    }

    public static class CoopLog
    {
        public static void Info(string message) { }
    }
}
