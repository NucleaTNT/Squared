using UnityEngine;

public class ClimbCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float yOffset;

    private static ClimbCameraMovement instance;
    public static ClimbCameraMovement Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        instance = this;
    }

    private void LateUpdate()
    {
        if (targetTransform == null) return;
        
        float targetYPos = targetTransform.position.y + yOffset;
        Vector3 currentPosition = this.transform.position;

        this.transform.position = new Vector3(
                                    currentPosition.x,
                                    Mathf.Lerp(currentPosition.y, Mathf.Clamp(targetYPos, 0, targetYPos), Time.deltaTime * 2),
                                    currentPosition.z
                                );
    }

    public void SetTargetTransform(Transform transform) => targetTransform = transform;
}
