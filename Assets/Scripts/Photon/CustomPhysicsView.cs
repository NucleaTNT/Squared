using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomPhysicsView : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D objectRb;
    private Vector2 networkPosition;
    
    [Range(0, 50)] public float TeleportDistance;

    private void Awake() 
    {
        objectRb = GetComponent<Rigidbody2D>();
        networkPosition = objectRb.position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (objectRb != null)
            if (stream.IsWriting)
            {
                stream.SendNext(objectRb.position);
                stream.SendNext(objectRb.velocity);
            } else
            {
                networkPosition = (Vector2)stream.ReceiveNext();
                objectRb.velocity = (Vector2)stream.ReceiveNext();

                float lag = Mathf.Abs((float)((PhotonNetwork.Time - info.SentServerTime)));
                networkPosition += objectRb.velocity * lag;
            }
        else objectRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if (!photonView.IsMine) 
        {
            if (Vector2.Distance(objectRb.position, networkPosition) > TeleportDistance) objectRb.position = networkPosition;
            objectRb.position = Vector2.MoveTowards(objectRb.position, networkPosition, Time.fixedDeltaTime);
        }
    }
}
