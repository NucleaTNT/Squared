using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.NucleaTNT.Squared.Networking
{
    public struct PlayerInfo
    {
        public int PlayerColorIndex;
        public int PlayerID;
        public string PlayerName;
    }

    public class PlayerNetworking : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
    {
        public static GameObject LocalPlayerInstance;
        public PlayerInfo PlayerInfo;

        [SerializeField] private MonoBehaviour[] _scriptsToIgnore;
        private static readonly Color[] _playerColors = {Color.red, Color.blue, Color.green, Color.yellow, Color.white};

        public void DisableNetworkScripts()
        {
            if (!photonView.IsMine) foreach (MonoBehaviour script in _scriptsToIgnore) script.enabled = false;
        }

        public void OnPhotonInstantiate(PhotonMessageInfo photonMessageInfo)
        {
            photonMessageInfo.Sender.TagObject = gameObject;
            PlayerInfo.PlayerColorIndex = (int) photonMessageInfo.photonView.InstantiationData[0];
            PlayerInfo.PlayerID = photonMessageInfo.Sender.ActorNumber;
            PlayerInfo.PlayerName = photonMessageInfo.Sender.NickName;
            gameObject.name = $"{PlayerInfo.PlayerName} (PlayerObject)";

            SetInstantiationData(photonMessageInfo);
            if (photonMessageInfo.Sender.IsLocal) LocalPlayerInstance = gameObject;

            Debug.Log($"Instantiated New Player: [{PlayerInfo.PlayerID}]{PlayerInfo.PlayerName}[{PlayerInfo.PlayerColorIndex}]");
            // photonView.RPC("UpdatePlayerAppearance", RpcTarget.AllBuffered, PlayerInfo.PlayerID);
            gameObject.GetComponent<SpriteRenderer>().color = _playerColors[PlayerInfo.PlayerColorIndex];
            gameObject.GetComponentInChildren<Text>().text = PlayerInfo.PlayerName;
        }

        protected virtual void SetInstantiationData(PhotonMessageInfo photonMessageInfo) { }

        [PunRPC]
        protected void UpdatePlayerAppearance(int playerID)
        {
            GameObject playerGO = (GameObject)PhotonNetwork.CurrentRoom.GetPlayer(playerID).TagObject;
            PlayerInfo playerInfo = playerGO.GetComponent<PlayerNetworking>().PlayerInfo;
            
            Debug.Log($"RPC Received: UpdatePlayerAppearance -> [{playerInfo.PlayerID}]{playerInfo.PlayerName}[{playerInfo.PlayerColorIndex}]");
            
            playerGO.GetComponent<SpriteRenderer>().color = _playerColors[playerInfo.PlayerColorIndex];
            playerGO.GetComponentInChildren<Text>().text = playerInfo.PlayerName;
        }
    }
}
