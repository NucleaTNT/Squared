using Photon.Pun;
using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting
{
	public class LeaveRoomButton : MonoBehaviour
	{
		public void OnClick_LeaveRoom()
		{
			if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
		}
	}
}
