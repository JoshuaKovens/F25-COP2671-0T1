using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public BoxCollider2D bounds; // drag your CameraBounds here

    private float minX, maxX, minY, maxY;

    void Start()
    {
        minX = bounds.bounds.min.x;
        maxX = bounds.bounds.max.x;
        minY = bounds.bounds.min.y;
        maxY = bounds.bounds.max.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Clamp the camera center so the edges stay inside bounds
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float targetX = Mathf.Clamp(player.position.x, minX + horzExtent, maxX - horzExtent);
        float targetY = Mathf.Clamp(player.position.y, minY + vertExtent, maxY - vertExtent);

        Vector3 targetPos = new Vector3(targetX, targetY, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}

