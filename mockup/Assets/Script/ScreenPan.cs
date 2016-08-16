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
    }
}
