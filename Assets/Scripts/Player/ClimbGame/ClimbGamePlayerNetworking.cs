using Dev.NucleaTNT.Squared.CameraScripts;
using Photon.Pun;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.Networking
{
    public class ClimbGamePlayerNetworking : PlayerNetworking
    {
        protected override void SetInstantiationData(PhotonMessageInfo photonMessageInfo)
        {
            if (!photonMessageInfo.Sender.IsLocal) return;
            if (ClimbCameraMovement.Instance == null) ClimbCameraMovement.Instance = Camera.main!.GetComponent<ClimbCameraMovement>(); 
                    
            ClimbCameraMovement.Instance!.TargetTransform = (((GameObject)photonMessageInfo.Sender.TagObject).transform);
        }
    }
}