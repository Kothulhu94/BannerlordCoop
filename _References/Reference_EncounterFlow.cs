// PSEUDO-CODE REFERENCE for Antigravity Analysis
// Do not compile. Use this to trace the Base Game logic.

namespace TaleWorlds.CampaignSystem
{
    public class PlayerEncounter
    {
        // 1. The entry point when parties touch on the map
        public void Init(PartyBase attackerParty, PartyBase defenderParty, Settlement settlement)
        {
            // Logic to determine if this is a battle, conversation, or siege
            this.SetupContext();
        }

        // 2. The method that decides which menu opens
        public void Start()
        {
             // Base game checks if we should jump straight to battle or open a menu
             // FOR COOP: This must be intercepted to check IsCoopPlayer(defenderParty)
             GameMenu.ActivateGameMenu("encounter"); 
        }
    }

    public class CampaignGameStarter
    {
        // 3. How the "Attack/Talk/Leave" buttons are defined
        protected void AddGameMenuOption(string menuId, string optionId, string text, GameMenuOption.OnConditionDelegate condition, GameMenuOption.OnConsequenceDelegate consequence)
        {
            // "menuId" is usually "encounter" for map meetings.
        }
    }
}
