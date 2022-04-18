using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
	public class RoomActionCanvas : RoomLobbyCanvasMonoBehaviourPunCallbacks
	{
		[SerializeField] private CreateRoomMenu _createRoomMenu;
		[SerializeField] private RoomListingsMenu _roomListingsMenu;

		public new RoomLobbyCanvases RoomLobbyCanvases
		{
			private get { return _roomLobbyCanvases; }

			set
			{
				if (_roomLobbyCanvases == null)
				{
					_roomLobbyCanvases = value;
					_createRoomMenu.RoomLobbyCanvases = value;
					_roomListingsMenu.RoomLobbyCanvases = value;
				}
				else Debug.LogError("RoomsLobbyCanvases already initialized! Ignoring assignment.", value);
			}
		} 
	}
}
