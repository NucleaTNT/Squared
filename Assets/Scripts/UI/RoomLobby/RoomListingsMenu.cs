using System.Collections.Generic;
using Dev.NucleaTNT.PUNTesting.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
	public class RoomListingsMenu : RoomLobbyCanvasMonoBehaviourPunCallbacks
	{
		[SerializeField] private Transform _contentTransform;
		[SerializeField] private RoomListing _roomListing;
		private readonly List<RoomListing> _roomListingsList = new List<RoomListing>();

		public override void OnJoinedRoom()
		{
			RoomLobbyCanvases.RoomActionCanvas.Hide();
			RoomLobbyCanvases.CurrentRoomCanvas.Show();
			_contentTransform.DestroyChildren();
			_roomListingsList.Clear();
		}

		public override void OnRoomListUpdate(List<RoomInfo> roomList)
		{
			foreach (RoomInfo roomInfo in roomList)
			{
				if (roomInfo.RemovedFromList)
				{
					int index = _roomListingsList.FindIndex(listing => listing.RoomInfo.Name == roomInfo.Name);
					if (index == -1) continue;
					
					if (_roomListingsList[index] != null) Destroy(_roomListingsList[index].gameObject);
					_roomListingsList.RemoveAt(index);
				}
				else
				{
					int index = _roomListingsList.FindIndex(listing => listing.RoomInfo.Name == roomInfo.Name);
					if (index == -1)	// Listing doesn't exist - needs to be added
					{	
						RoomListing listing = Instantiate(_roomListing, _contentTransform);
						if (listing == null) continue;

						listing.SetRoomInfo(roomInfo);
						_roomListingsList.Add(listing);
					}
					else				// Listing already exists - needs updating
					{
						_roomListingsList[index].SetRoomInfo(roomInfo);
					}
				}
			}
		}
	}
}
