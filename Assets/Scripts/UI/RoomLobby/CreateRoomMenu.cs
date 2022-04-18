using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.NucleaTNT.PUNTesting
{
	public class CreateRoomMenu : RoomLobbyCanvasMonoBehaviourPunCallbacks
	{
		private static readonly Dictionary<int, bool> _availableColors = new Dictionary<int, bool> { 
			{0, false}, 
			{1, false},
			{2, false},
			{3, false}
		};
    	private readonly Hashtable _customRoomProperties = new Hashtable {{"AvailableColors", _availableColors}};
		
		[SerializeField] private InputField _roomNameInput;
		[SerializeField] private Text _roomNameInputPlaceText;

		public void OnClick_CreateRoom()
		{
			if (!PhotonNetwork.IsConnected) return;
			
			string attemptedRoomName = _roomNameInput.text;

			if (string.IsNullOrWhiteSpace(attemptedRoomName) || attemptedRoomName.Length < 3)
			{ 
				Debug.Log("Host: Invalid Room Name");

				_roomNameInputPlaceText.text = "Invalid [min 3 char]";
				_roomNameInputPlaceText.color = Color.red;
				_roomNameInput.text = null;
			} else PhotonNetwork.JoinOrCreateRoom(attemptedRoomName, new RoomOptions {MaxPlayers = 4, CustomRoomProperties = _customRoomProperties, PlayerTtl = 0}, TypedLobby.Default);
		}

		public override void OnCreatedRoom()
		{
			Debug.Log("Successfully created room.");

			RoomLobbyCanvases.RoomActionCanvas.Hide();
			RoomLobbyCanvases.CurrentRoomCanvas.Show();
		}

		public override void OnCreateRoomFailed(short returnCode, string message)
		{
			Debug.LogError($"Failed to create room. Reason[{returnCode}]: {message}", this);
		}
	}
}
