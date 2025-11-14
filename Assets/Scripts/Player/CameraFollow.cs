using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    public float smoothTime = 0f; // 0 = hard lock (best for Pixel Perfect)

    void LateUpdate()
    {
        if (!target) return;
        Vector3 targetPos = new Vector3(target.position.x + offset.x,
                                        target.position.y + offset.y,
                                        transform.position.z);
        if (smoothTime <= 0f) transform.position = targetPos;
        else transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime / smoothTime);
    }
}