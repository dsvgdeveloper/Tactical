using UnityEngine;
using System.Collections;

public class Pan : MonoBehaviour
{
    [Tooltip("The distance from the main camera the world positions will be sampled from")]
    public float Distance = 10.0f;

    [Tooltip("The minimum X position, can be adjusted in GameSettings")]
    public float MinX = -40.0f;

    [Tooltip("The maximum X position, can be adjusted in GameSettings")]
    public float MaxX = 40.0f;

    [Tooltip("The minimum Y position, can be adjusted in GameSettings")]
    public float MinY = -5.0f;

    [Tooltip("The maximum Y position, can be adjusted in GameSettings")]
    public float MaxY = 5.0f;

    [Tooltip("The minimum Z position, can be adjusted in GameSettings")]
    public float MinZ = -40.0f;

    [Tooltip("The maximum Z position, can be adjusted in GameSettings")]
    public float MaxZ = 40.0f;

    [Tooltip("The speed the camera pans, can be adjusted in GameSettings")]
    public float CamPanSpeed = 5f;

    float startTime = 0f;

    Vector2 dragOrigin;
    float pinchThreshold;
    float rotateThreshold;

    //   void OnEnable ()
    //   {
    //       Lean.LeanTouch.OnFingerDown += OnFingerDown;
    //   }

    //   void OnDisable()
    //   {
    //       Lean.LeanTouch.OnFingerDown -= OnFingerDown;
    //   }

    //// Use this for initialization
    //void Start () {
    //       pinchThreshold = (FindObjectOfType<GameManager>()).pinchThreshold;
    //       rotateThreshold = (FindObjectOfType<GameManager>()).rotateThreshold;
    //   }

    //// Update is called once per frame
    //void Update ()
    //   {
    //       if (Lean.LeanTouch.Fingers == null || 
    //           Input.touchCount > 1 ||
    //           Lean.LeanTouch.DragDelta.magnitude < GameManager.GM.dragThreshold ||
    //           Time.time - startTime < GameManager.GM.swipeTime) return;

    //       Vector3 pos = Camera.main.ScreenToViewportPoint(
    //           Lean.LeanTouch.CenterOfFingers - Lean.LeanTouch.Fingers[0].StartScreenPosition);
    //       float x = pos.x * CamPanSpeed * Time.deltaTime;
    //       float z = pos.y * CamPanSpeed * Time.deltaTime;

    //       Vector3 move = new Vector3(x, 0, z);

    //       (FindObjectOfType<GameManager>()).transform.Translate(move, Space.World);

    //       boundCamera();
    //   }

    void boundCamera()
    {
        Vector3 pos = (FindObjectOfType<GameManager>()).transform.position;
        float x = 0;
        float z = 0;

        if (pos.x <= MinX)
        {
            x = MinX;
        }
        else if (pos.x >= MaxX)
        {
            x = MaxX;
        }

        if (pos.z <= MinZ)
        {
            z = MinZ;
        }
        else if (pos.z >= MaxZ)
        {
            z = MaxZ;
        }

        if (z != 0 && x != 0)
        {
            (FindObjectOfType<GameManager>()).transform.position = new Vector3(x, 0, z);
        }
        else if (x != 0)
        {
            (FindObjectOfType<GameManager>()).transform.position = new Vector3(x, 0, pos.z);
        }
        else if (z != 0)
        {
            (FindObjectOfType<GameManager>()).transform.position = new Vector3(pos.x, 0, z);
        }
    }

    //   public void OnFingerDown(Lean.LeanFinger finger)
    //   {
    //       startTime = Time.time;
    //   }

    //   void LateUpdate()
    //   {
    //       //// Does the main camera exist?
    //       //// && !Lean.LeanTouch.Fingers[0].StartedOverGui
    //       //if (Camera.main != null)
    //       //{
    //       //    // Store the current camera position in a temp variable
    //       //    Vector3 worldPosition = Camera.main.transform.position;

    //       //    // Modify the world position by the delta world position of all fingers
    //       //    worldPosition -= Lean.LeanTouch.GetDeltaWorldPosition(10.0f);

    //       //    // Clamp on all axes
    //       //    //worldPosition.x = Mathf.Clamp(worldPosition.x * CamPanSpeed, MinX, MaxX);
    //       //    worldPosition.x *= CamPanSpeed;
    //       //    worldPosition.y = Camera.main.transform.position.y;
    //       //    //worldPosition.z = Mathf.Clamp(worldPosition.z * CamPanSpeed, MinZ, MaxZ);
    //       //    worldPosition.z *= CamPanSpeed;

    //       //    // Set the new world position
    //       //    Camera.main.transform.position = worldPosition;




    //       //    //Vector3 pos = Camera.main.ScreenToViewportPoint(Lean.LeanTouch.Fingers[0].LastScreenPosition
    //       //    //    - Lean.LeanTouch.Fingers[0].StartScreenPosition);
    //       //    //Vector3 pos = Camera.main.ScreenToViewportPoint(Lean.LeanTouch.DragDelta);
    //       //    //Vector3 move = new Vector3(pos.x * CamPanSpeed, 0, pos.y * CamPanSpeed);

    //       //    //move.x = Mathf.Clamp(move.x, MinX, MaxX);
    //       //    //move.y = Mathf.Clamp(move.y, MinY, MaxY);
    //       //    //move.z = Mathf.Clamp(move.z, MinZ, MaxZ);

    //       //    //Camera.main.transform.Translate(move, Space.World);
    //       //}
    //   }

    public void OnEnable()
    {
        Lean.LeanTouch.OnDrag += OnDrag;
    }

    public void OnDisable ()
    {
        Lean.LeanTouch.OnDrag -= OnDrag;
    }

    public void OnDrag(Vector2 vec)
    {
        //if (Lean.LeanTouch.Fingers == null ||
        //    Input.touchCount > 1 ||
        //    Lean.LeanTouch.DragDelta.magnitude < GameManager.GM.dragThreshold ||
        //    Time.time - startTime < GameManager.GM.swipeTime) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(
        //    Lean.LeanTouch.CenterOfFingers - Lean.LeanTouch.Fingers[0].StartScreenPosition);
            vec);

        float x = pos.x * CamPanSpeed * Time.deltaTime;
        float z = pos.y * CamPanSpeed * Time.deltaTime;

        Vector3 move = new Vector3(x, 0, z);

        GameManager.GM.cameraAxis.transform.Translate(move, Space.World);

        boundCamera();
    }
}
