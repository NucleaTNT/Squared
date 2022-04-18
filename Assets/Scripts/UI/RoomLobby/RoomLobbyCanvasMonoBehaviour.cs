using Photon.Pun;
using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
    public class RoomLobbyCanvasMonoBehaviourPunCallbacks : MonoBehaviourPunCallbacks
    {
        public RoomLobbyCanvases RoomLobbyCanvases
        {
            protected get { return _roomLobbyCanvases; }
            set 
            {
                if (_roomLobbyCanvases == null) _roomLobbyCanvases = value;
                else Debug.LogError("RoomsLobbyCanvases already initialized! Ignoring assignment.", value);
            }
        }
        protected RoomLobbyCanvases _roomLobbyCanvases;

        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show() => gameObject.SetActive(true);
    }
}