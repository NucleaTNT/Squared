using Dev.NucleaTNT.Squared.PlayerScripts;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.Gameplay
{
    public class JumpableObject : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) other.GetComponent<ClimbGamePlayerMovement>().ResetJump();
        }
    }
}
