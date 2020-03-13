using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static readonly float PanSpeed = 40f;
    private static readonly float ZoomSpeedMouse = 18f;

    private static readonly float[] BoundsX = new float[] { -20f, 20f };
    private static readonly float[] BoundsZ = new float[] { -60f, 20f };
    private static readonly float[] ZoomBounds = new float[] { 20f, 60f };

    private Vector3 lastPanPosition;


    void Update()
    {
        HandleMouse();
    }

    void HandleMouse()
    {
        //Positioning
        if (Input.GetMouseButtonDown(1))
        {
            lastPanPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            PanCamera(Input.mousePosition);
        }

        // Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

        transform.Translate(move, Space.World);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;

        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }
        GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
