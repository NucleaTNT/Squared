using UnityEngine;

namespace Dev.NucleaTNT.Squared.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeathBarrier : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;

        protected void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (other.TryGetComponent<PlayerDeathHandler>(out PlayerDeathHandler handler))
            {
                handler.HandleDeathBarrier();
            } else 
            {
                Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.position = new Vector2(_respawnPoint.position.x, _respawnPoint.position.y);
            }
        }
    }
}