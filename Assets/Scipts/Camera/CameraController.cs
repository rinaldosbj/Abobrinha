using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public bool isCrowded = false;
    private Vector3 velocity = Vector3.one;
    public bool isFixed = true;

    private Vector3 getSmoothPosition()
    {
        float positionY = player.position.y;
            float positionX = player.position.x;
            if (isCrowded)
            {
                positionY = player.position.y - 3.5f;
            }

            if (positionY < minPosition.y)
            {
                positionY = minPosition.y;
            }
            else if (positionY > maxPosition.y)
            {
                positionY = maxPosition.y;
            }

            if (positionX < minPosition.x)
            {
                positionX = minPosition.x;
            }
            else if (positionX > maxPosition.x)
            {
                positionX = maxPosition.x;
            }
            Vector3 desiredPosition = new Vector3(positionX, positionY, transform.position.z) + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            return smoothedPosition;
    }

    private void LateUpdate()
    {
        if (!isFixed)
        {
            transform.position = getSmoothPosition();
        }
    }

    private void FixedUpdate()
    {
        if (isFixed)
        {
            transform.position = getSmoothPosition();
        }
    }

}
