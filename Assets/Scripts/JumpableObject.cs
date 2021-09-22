using UnityEngine;

public class JumpableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<ClimbGamePlayerMovement>().ResetJump();
    }
}