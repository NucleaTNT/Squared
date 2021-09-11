using Photon.Pun;
using UnityEngine;

public class ClimbGamePlayerNetworking : PlayerNetworking
{
    protected override void SetInstantiationData(PhotonMessageInfo photonMessageInfo)
    {
        Debug.Log((int)photonMessageInfo.photonView.InstantiationData[1]);
    }
}
