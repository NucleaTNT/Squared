using System.Collections.Generic;
using Dev.NucleaTNT.PUNTesting.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
	public class PlayerListingsMenu : RoomLobbyCanvasMonoBehaviourPunCallbacks
	{
		[SerializeField] private Transform _contentTransform;
		[SerializeField] private PlayerListing _playerListing;
		private readonly List<PlayerListing> _playerListingsList = new List<PlayerListing>();

		private void AddPlayerListing(Player player)
		{
			int index = _playerListingsList.FindIndex(listing => listing.Player.ActorNumber == player.ActorNumber);
			if (index == -1)
			{

				PlayerListing listing = Instantiate(_playerListing, _contentTransform);
				if (listing == null) return;
					
				listing.SetPlayerInfo(player);
				_playerListingsList.Add(listing);
			}
			else
			{
				_playerListingsList[index].SetPlayerInfo(player);
			}
		}
		
		public override void OnDisable() 
		{
		    base.OnDisable();

		    foreach (PlayerListing listing in _playerListingsList)
			    Destroy(listing.gameObject);
		    _playerListingsList.Clear();
		}

		public override void OnEnable()
		{
			base.OnEnable();
			GetCurrentRoomPlayers();
		}

		private void GetCurrentRoomPlayers()
		{
			if (PhotonNetwork.IsConnected == false || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
			foreach (KeyValuePair<int, Player> item in PhotonNetwork.CurrentRoom.Players)
				AddPlayerListing(item.Value);
		}
		
		public override void OnLeftRoom()
		{
			RoomLobbyCanvases.CurrentRoomCanvas.Hide();
			RoomLobbyCanvases.RoomActionCanvas.Show();
		}

		public override void OnPlayerEnteredRoom(Player player)
		{
			AddPlayerListing(player);
		}

		public override void OnPlayerLeftRoom(Player player)
		{
			int index = _playerListingsList.FindIndex(listing => listing.Player.ActorNumber == player.ActorNumber);
			if (index == -1) return;
					
			if (_playerListingsList[index] != null) Destroy(_playerListingsList[index].gameObject);
			_playerListingsList.RemoveAt(index);
		}
	}
}
