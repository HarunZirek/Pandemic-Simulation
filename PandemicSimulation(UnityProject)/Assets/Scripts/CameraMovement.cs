using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSpeed = 20f;

    public Vector2 camLimit;
    public Vector2 zoomLimit;

    public float zoomSpeed = 2f;

    private void Update()
    {
        Vector3 pos = transform.position;

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if(horiz > 0f)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        else if(horiz < 0)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        if (vert > 0f)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
        else if (vert < 0)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -camLimit.x, camLimit.x);
        pos.z = Mathf.Clamp(pos.z, -camLimit.y, camLimit.y);

        float zoom = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= zoom * zoomSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, zoomLimit.y, zoomLimit.x);

        transform.position = pos;
    }
}