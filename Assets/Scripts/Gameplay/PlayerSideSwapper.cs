using Dev.NucleaTNT.Squared.PlayerScripts;
using UnityEngine;

public class PlayerSideSwapper : MonoBehaviour
{
	[SerializeField] private float _cooldownTime = 3, _screenWidth;

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		ClimbGamePlayerMovement playerMovement = other.GetComponent<ClimbGamePlayerMovement>();
		if (playerMovement.IsSideSwapOnCooldown) return;

		Rigidbody2D otherRb = other.attachedRigidbody;
		float velocity = otherRb.velocity.x;
		Transform otherT = other.transform;

		otherT.position = new Vector3(_screenWidth * (velocity > 0 ? -1 : 1), otherT.position.y, otherT.position.z);
		
		other.attachedRigidbody.AddForce(new Vector2(0, playerMovement.JumpVelocity * 1.5f), ForceMode2D.Impulse);
		StartCoroutine(playerMovement.SideSwapCooldownCoroutine(_cooldownTime));
	}
}
