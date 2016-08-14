using UnityEngine;
using System.Collections;

public class ScreenZoom : MonoBehaviour {

    public float deltaPinch = 1.1f;
    public Camera camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            deltaPinch = 1.01f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            deltaPinch = .99f;
        }

        camera.transform.position = new Vector3(camera.transform.position.x * deltaPinch,
            camera.transform.position.y * deltaPinch,
            camera.transform.position.z * deltaPinch);
    }
}
