using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.NucleaTNT.PUNTesting
{
	public class PlayerListing : MonoBehaviour
	{
		public Player Player { get; private set; }
		
		[SerializeField] private Text _label;

		public void SetPlayerInfo(Player player)
		{
			Player = player;
			_label.text = $"{player.NickName}[{player.ActorNumber}]";	
		}
	}
}
