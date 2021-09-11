using UnityEngine;

public class ClimbCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float yOffset;

    private void LateUpdate() =>
        this.transform.position = new Vector3(transform.position.x, targetTransform.position.y + yOffset, transform.position.z);
}
