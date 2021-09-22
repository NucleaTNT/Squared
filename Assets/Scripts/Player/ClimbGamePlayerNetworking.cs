using Photon.Pun;
using UnityEngine;

public class ClimbGamePlayerNetworking : PlayerNetworking
{
    private static ClimbCameraMovement cameraInstance;

    protected override void SetInstantiationData(PhotonMessageInfo photonMessageInfo)
    {
        if (cameraInstance == null) cameraInstance = Camera.main.GetComponent<ClimbCameraMovement>();
        if (photonMessageInfo.Sender.IsLocal) cameraInstance.SetTargetTransform(((GameObject)photonMessageInfo.Sender.TagObject).transform);
    }
}
