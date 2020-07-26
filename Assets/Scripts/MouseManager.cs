using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    Vector3 lastFramePosition;
    Vector3 currFramePosition;
    Vector3 dragStartPosition;

    public GameObject circleCursorPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        CameraPan();
        //CursorUpdate();
        DragigingUpdate();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    private void DragigingUpdate()
    {
        int start_x = Mathf.FloorToInt(dragStartPosition.x);
        int end_x = Mathf.FloorToInt(currFramePosition.x);
        int start_y = Mathf.FloorToInt(dragStartPosition.y);
        int end_y = Mathf.FloorToInt(currFramePosition.y);
        if (end_x < start_x)
            if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }
        if (Input.GetMouseButton(0))
        {
            {
                int tmp = end_x;
                end_x = start_x;
                start_x = tmp;
            }
            if (end_y < start_y)
            {
                int tmp = end_y;
                end_y = start_y;
                start_y = tmp;
            }
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldManager.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        //Display the hint on Top of this Tile
                        Instantiate(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldManager.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        t.Type = Tile.TileType.Empty;
                    }
                }
            }
        }
    }

    //private void CursorUpdate()
    //{
    //    Tile tileUnderMouse = WorldManager.Instance.GetTileAtWorldCoord(currFramePosition);
    //    if (tileUnderMouse != null)
    //    {
    //        circleCursor.SetActive(true);
    //        Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
    //        circleCursor.transform.position = cursorPosition;
    //    }
    //    else
    //    {
    //        circleCursor.SetActive(false);
    //    }
    //}

    public void CameraPan()
    {
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

    }
}
