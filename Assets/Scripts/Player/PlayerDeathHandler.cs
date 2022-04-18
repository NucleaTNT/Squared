using Photon.Pun;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerDeathHandler : MonoBehaviour
    {
        public bool IsDead => _isDead;

        [SerializeField] private Transform _respawnPoint;
        [SerializeField] protected bool _isDead = false;

        public virtual void HandleDeathBarrier() 
        {
            _isDead = true;
            Debug.Log($"{PhotonNetwork.NickName} died to the death barrier!");

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.position = new Vector2(_respawnPoint.position.x, _respawnPoint.position.y);
        }
    }
}
