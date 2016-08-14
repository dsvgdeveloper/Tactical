using UnityEngine;
using System.Collections;

public class ScreenPan : MonoBehaviour {

    public float camPanSpeed = 15f;
    Vector3 dragOrigin;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.moving)
        {
            if (GameManager.GM.cameraAxis.transform.position == GameManager.GM.moveToTarget.transform.position)
            {
                GameManager.GM.moving = false;
                return;
            }

            GameManager.GM.cameraAxis.transform.position = Vector3.Lerp(
                GameManager.GM.cameraAxis.transform.position,
                GameManager.GM.moveToTarget.transform.position,
                camPanSpeed * Time.deltaTime);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    dragOrigin = Input.mousePosition;
        //    return;
        //}

        //if (!Input.GetMouseButton(0)) return;

        //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //Vector3 move = new Vector3(pos.x * camPanSpeed, 0, pos.y * camPanSpeed);

        //transform.Translate(move, Space.World);
    }
}
