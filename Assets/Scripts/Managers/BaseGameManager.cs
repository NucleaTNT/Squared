using System.Collections.Generic;
using System.Linq;
using Dev.NucleaTNT.Squared.Networking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Dev.NucleaTNT.Squared.Managers
{
    public class BaseGameManager : MonoBehaviourPunCallbacks
    {
        public static BaseGameManager Instance => s_instance;
        public Transform PlayerInstantiatePosition;
        public Player[] PlayerList => _playerList;
        
        protected Player[] _playerList;
        [SerializeField] protected GameObject _playerPrefab;
        private static BaseGameManager s_instance;

        public virtual bool CheckForGameEnd() 
        {   
            return false;
        }

        #region Private Methods

        protected GameObject InstantiatePlayer()
        {
            string username = PhotonNetwork.LocalPlayer.NickName;
            int playerID = PhotonNetwork.LocalPlayer.ActorNumber;
            Debug.Log($"Instantiating Player: [{playerID}]{username}");

            int colorIndex = -1;
            Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            
            // Loop till the first open color
            foreach (KeyValuePair<int, bool> colorEntry in ((Dictionary<int, bool>)roomProperties["AvailableColors"]).Where(colorEntry => !colorEntry.Value))
            {
                colorIndex = colorEntry.Key;
                ((Dictionary<int, bool>)(roomProperties["AvailableColors"]))[colorIndex] = true;

                break;
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);

            object[] playerInitData = GetPlayerInitData(); 
            object[] playerInstantiateData = new object[playerInitData.Length + 1]; // Allow for arbitrary amount of game-specific info to be added after colorIndex
            playerInstantiateData[0] = colorIndex;
            playerInitData.CopyTo(playerInstantiateData, 1);

            return PhotonNetwork.Instantiate($"Prefabs/Player/{_playerPrefab.name}", PlayerInstantiatePosition.position, Quaternion.identity, data: playerInstantiateData);
        }

        protected virtual object[] GetPlayerInitData()
        {
            return new object[] { };
        }

        protected void UpdatePlayerList() => _playerList = PhotonNetwork.PlayerList;

        #endregion

        #region PUN Callbacks

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"Player Left: {otherPlayer.NickName}[{otherPlayer.ActorNumber}]");

            UpdatePlayerList();
            Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            GameObject leftPlayerObj = ((GameObject)otherPlayer.TagObject);

            int index = leftPlayerObj.GetComponent<PlayerNetworking>().PlayerInfo.PlayerColorIndex;
            if (index < 0 || index >= ((Dictionary<int, bool>)roomProperties["AvailableColors"]).Count) return;
            
            ((Dictionary<int, bool>)(roomProperties["AvailableColors"]))[index] = false;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        }

        public override void OnLeftRoom() 
        {
            PhotonManager.UsernameConfirmButton.IsInteractable = true;
            PhotonNetwork.LoadLevel(PhotonManager.PhotonConnector.LobbySceneIndex);
        }

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            if (s_instance == null) s_instance = this;
            else if (s_instance != this) Destroy(this);
        }

        protected void Start()
        {
            _playerList = PhotonNetwork.PlayerList;
            InstantiatePlayer();
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }

        #endregion
    }
}
