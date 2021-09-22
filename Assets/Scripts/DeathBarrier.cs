using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathBarrier : MonoBehaviour
{
    [SerializeField] Vector2 respawnPos = new Vector2(0, 0);

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<Rigidbody2D>().position = respawnPos;
    }
}
