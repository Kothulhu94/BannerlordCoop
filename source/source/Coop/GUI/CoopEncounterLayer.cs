using System; // For Action
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace Coop.GUI
{
    public class CoopEncounterLayer
    {
        private GauntletLayer _layer;
        private GauntletMovieIdentifier _movie;
        private CoopEncounterVM _datasource;

        public void LoadLayer(CoopEncounterVM vm)
        {
            // Create a new layer with categoryId, order, and shouldClear
            _layer = new GauntletLayer("CoopEncounter", 100, false);
            
            // Initialize the ViewModel (Brain)
            _datasource = vm;

            // This is the "Magic Line" that connects XML to C#
            _movie = _layer.LoadMovie("CoopEncounterDashboard", _datasource);

            // Make the layer visible on the current screen (MapScreen)
            _layer.IsFocusLayer = true;
            ScreenManager.TopScreen?.AddLayer(_layer);
            ScreenManager.TrySetFocus(_layer);
        }

        public void UnloadLayer()
        {
            if (_layer != null)
            {
                // Remove the layer from the screen
                ScreenManager.TopScreen?.RemoveLayer(_layer);

                // Release the movie and clear references
                if (_movie != null)
                {
                    _layer.ReleaseMovie(_movie);
                    _movie = null;
                }

                _layer = null;
                _datasource = null;
            }
        }
    }

    // ViewModel Implementation
    public class CoopEncounterVM : ViewModel
    {
        private readonly Action _onClose;

        public CoopEncounterVM(Action onClose)
        {
            _onClose = onClose;
        }

        // Connects to <ButtonWidget Command="ExecuteTrade" ... /> in XML
        public void ExecuteTrade()
        {
            // TODO: Send Network Packet
            // NetworkManager.Send(new RequestTradePacket(targetPlayerId)); 
            
            Debug.Print("DEBUG: Trade Requested via UI");
        }

        public void ExecuteDuel()
        {
             // TODO: Send Network Packet
             
             Debug.Print("DEBUG: Duel Requested via UI");
        }

        public void ExecuteClose()
        {
            _onClose?.Invoke(); // Tells the Layer to destroy itself
        }
    }
}
