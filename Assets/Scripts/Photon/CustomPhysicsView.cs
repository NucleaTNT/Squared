using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomPhysicsView : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D _objectRb;
    private Vector2 _networkPosition;

    [Range(0, 50)] public float TeleportDistance;

    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody2D>();
        _networkPosition = _objectRb.position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (_objectRb != null)
            if (stream.IsWriting)
            {
                stream.SendNext(_objectRb.position);
                stream.SendNext(_objectRb.velocity);
            } else
            {
                _networkPosition = (Vector2)stream.ReceiveNext();
                _objectRb.velocity = (Vector2)stream.ReceiveNext();

                float lag = Mathf.Abs((float)((PhotonNetwork.Time - info.SentServerTime)));
                _networkPosition += _objectRb.velocity * lag;
            }
        else _objectRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine) return;
       
        if (Vector2.Distance(_objectRb.position, _networkPosition) > TeleportDistance) _objectRb.position = _networkPosition;
        _objectRb.position = Vector2.MoveTowards(_objectRb.position, _networkPosition, Time.fixedDeltaTime);
    }
}
