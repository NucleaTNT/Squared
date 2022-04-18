using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
	public class RoomLobbyCanvases : MonoBehaviour
	{
		public CurrentRoomCanvas CurrentRoomCanvas => _currentRoomCanvas;
		public RoomActionCanvas RoomActionCanvas => _roomActionCanvas;

		[SerializeField] private CurrentRoomCanvas _currentRoomCanvas;
		[SerializeField] private RoomActionCanvas _roomActionCanvas;

		private void Awake()
		{
			InitializeCanvases();
		}

		private void InitializeCanvases()
		{
			RoomActionCanvas.RoomLobbyCanvases = this;
			CurrentRoomCanvas.RoomLobbyCanvases = this;
		}
	}
}
