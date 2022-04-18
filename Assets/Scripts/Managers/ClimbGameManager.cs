using System;
using Dev.NucleaTNT.Squared.CameraScripts;
using Dev.NucleaTNT.Squared.Gameplay;
using Dev.NucleaTNT.Squared.Networking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.Managers
{
    public class ClimbGameManager : BaseGameManager
    {
        public static new ClimbGameManager Instance => s_instance;

        [SerializeField] private BeamSpawner _beamSpawner;
        private static ClimbGameManager s_instance;

        public override bool CheckForGameEnd() 
        {
            for (int i = 0; i < PlayerList.Length; i++) 
            {
                if (!((GameObject)PlayerList[i].TagObject).GetComponent<PlayerDeathHandler>().IsDead) 
                    return false;
            }
            return true;
        }

        public void EndGame()
        {
            PhotonNetwork.LoadLevel("ClimbGameLobbyScene");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            GameObject leftPlayerObj = ((GameObject)otherPlayer.TagObject);

            if (leftPlayerObj.transform == ClimbCameraMovement.Instance.TargetTransform) ClimbCameraMovement.Instance.CycleTargetPlayer();
        }

        public void StartGame() 
        {
            for (int i = 0; i < 10; i++) _beamSpawner.GenerateNextBeamLayer();

            Debug.Log(PlayerList.ToStringFull());
            foreach (Player player in PlayerList)
            {
                GameObject obj = InstantiatePlayer();
                PhotonView view = obj.GetPhotonView();

                Debug.Log(player.ToStringFull());

                if (player.IsLocal) continue;
                Debug.Log("Transferring ownership.");

                obj.GetComponent<PlayerNetworking>().DisableNetworkScripts();
                view.TransferOwnership(player);
            }
        }

        private new void Awake()
        {           
            if (s_instance == null) s_instance = this;
            else if (s_instance != this) Destroy(this);
            
            base.Awake();
        }

        private new void Start()
        {
            _playerList = PhotonNetwork.PlayerList;
            
            if (PhotonNetwork.IsMasterClient) StartGame();
        }

        private new void Update() 
        {
            base.Update();
            // if (Input.GetKeyDown(KeyCode.L)) _beamSpawner.GenerateNextBeamLayer();
            if (Input.GetKeyDown(KeyCode.C)) ClimbCameraMovement.Instance.CycleTargetPlayer();
        }
    }
}
