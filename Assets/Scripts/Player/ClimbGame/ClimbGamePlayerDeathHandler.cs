using Dev.NucleaTNT.Squared.CameraScripts;
using Dev.NucleaTNT.Squared.Gameplay;
using Dev.NucleaTNT.Squared.Managers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.PlayerScripts 
{
    public class ClimbGamePlayerDeathHandler : PlayerDeathHandler
    {
        public override void HandleDeathBarrier() 
        {
            if (!PhotonNetwork.InRoom || !GetComponent<PhotonView>().IsMine) return;
            Debug.Log($"{PhotonNetwork.NickName} died to the death barrier!");


            Player[] playerList = ClimbGameManager.Instance.PlayerList;
            if (playerList.Length < 2) 
            {
                PhotonNetwork.LeaveRoom();
                return;
            }
            
            _isDead = true;
            if (ClimbGameManager.Instance.CheckForGameEnd()) ClimbGameManager.Instance.EndGame();
            else ClimbCameraMovement.Instance.CycleTargetPlayer();
        }
    }
}