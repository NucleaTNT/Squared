using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(0, -3.5f);
    }
}
