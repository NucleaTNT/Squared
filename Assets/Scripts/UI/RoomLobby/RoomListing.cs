using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.NucleaTNT.PUNTesting
{
	public class RoomListing : MonoBehaviour
	{
		public RoomInfo RoomInfo { get; private set; }
		[SerializeField] private Text _label;

		public void OnClick_JoinRoom() => PhotonNetwork.JoinRoom(RoomInfo.Name);

		public void SetRoomInfo(RoomInfo roomInfo)
		{
			RoomInfo = roomInfo;
			_label.text = $"{roomInfo.Name} [{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}]";	
		}
	}
}
