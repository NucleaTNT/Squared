using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
    public class CurrentRoomCanvas : RoomLobbyCanvasMonoBehaviourPunCallbacks
    {
        public new RoomLobbyCanvases RoomLobbyCanvases
        {
            protected get { return _roomLobbyCanvases; }
            set
            {
                if (_roomLobbyCanvases == null)
                {
                    _roomLobbyCanvases = value;
                    _playerListingsMenu.RoomLobbyCanvases = value;
                }
                else Debug.LogError("RoomsLobbyCanvases already initialized! Ignoring assignment.", value);
            }
        }
        [SerializeField] private PlayerListingsMenu _playerListingsMenu;
    }
}