using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gesture : MonoBehaviour
{
    // This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
    public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    [Tooltip("The minimum field of view angle we want to zoom to")]
    public float Minimum = 40.0f;

    [Tooltip("The maximum field of view angle we want to zoom to")]
    public float Maximum = 90.0f;

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
    public float CamPanSpeed = 15f;

    float startTime = 0f;
    Vector2 touchStart;
    private float pinchThreshold = 1f;
    private float twistThreshold = 30f;
    private float swipeDistThreshold = 50f;
    private float swipeTimeThreshold = .3f;
    bool swipe = false;

    [Tooltip("The text element we will display the swipe information in")]
    public Text InfoText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.touchCount == 1) {
            InfoText.text = "single";
            doSingleTouch();
        } else if (Input.touchCount == 2) {
            InfoText.text = "multi";
            //doMultiTouch();
        } else
        {
            swipe = false;
        }
	}

    public void doSingleTouch()
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            //check for tap
            touchStart = Input.touches[0].position;
            startTime = Time.time;
        } else if ((Input.touches[0].phase == TouchPhase.Moved ||
            Input.touches[0].phase == TouchPhase.Stationary) && !swipe) {
            if (Time.time - startTime < swipeTimeThreshold && 
                    Mathf.Abs(Input.touches[0].position.magnitude - touchStart.magnitude) > swipeDistThreshold) {
                swipe = true;
                OnFingerSwipe(Lean.LeanTouch.Fingers[0]);
            } else if (!swipe && Time.time - startTime >= swipeTimeThreshold) {
                if (GameManager.GM.movingChar)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Lean.LeanTouch.Fingers[0].StartScreenPosition);
                }

                Vector3 pos = Camera.main.ScreenToViewportPoint(
                        Lean.LeanTouch.CenterOfFingers - Lean.LeanTouch.Fingers[0].StartScreenPosition);

                float x = pos.x * CamPanSpeed * Time.deltaTime;
                float z = pos.y * CamPanSpeed * Time.deltaTime;

                Vector3 move = new Vector3(x, 0, z);

                GameManager.GM.cameraAxis.transform.Translate(move, Space.World);

                boundCamera();
            }
        }
    }

    public void doMultiTouch()
    {
        if (Lean.LeanTouch.PinchScale > pinchThreshold)
        {
            InfoText.text += "pinch occured";
            // Store the old FOV in a temp variable
            var fieldOfView = Camera.main.fieldOfView;

            // Scale the FOV based on the pinch scale
            fieldOfView /= Lean.LeanTouch.PinchScale;

            // Clamp the FOV to out min/max values
            fieldOfView = Mathf.Clamp(fieldOfView, Minimum, Maximum);

            // Set the new FOV
            Camera.main.fieldOfView = fieldOfView;
        }
    }

    public void OnFingerSwipe(Lean.LeanFinger finger)
    {
        InfoText.text += "swipe entered";
        if (Input.touchCount != 1 ||
            Lean.LeanTouch.TwistDegrees >= GameManager.GM.rotateThreshold ||
            GameManager.GM.rotating) return;
        InfoText.text += "you swiped";

        if (finger == null)
        {
            InfoText.text += "finger was null";
        }

        if (finger.StartedOverGui)
        {
            // check directions
            UISwipe(finger);
            InfoText.text += "ui swipe";
        }
        else {
            InfoText.text += "not ui swipe";
            // check that it didnt start over a character/ Raycast information
            var ray = finger.GetStartRay();
            var hit = default(RaycastHit);

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
                InfoText.text += "something was hit";
                // Was that collider this one?
                if (hit.collider.gameObject.layer == 9)
                {
                    // check left right
                    nonUISipe(finger);
                }
            }
        }
    }

    public void UISwipe(Lean.LeanFinger finger)
    {
        // Store the swipe delta in a temp variable
        Vector2 swipe = finger.SwipeDelta;

        if (swipe.x < -Mathf.Abs(swipe.y))
        {
            InfoText.text += "UI Swipe Left";
        }
        else if (swipe.x > Mathf.Abs(swipe.y))
        {
            InfoText.text += "UI Swipe Right";
        }
        else if (swipe.y < -Mathf.Abs(swipe.x))
        {
            InfoText.text += "UI Swipe Down";
        }
        else if (swipe.y > Mathf.Abs(swipe.x))
        {
            InfoText.text += "UI Swipe Up";
        }
    }

    public void nonUISipe(Lean.LeanFinger finger)
    {
        // Store the swipe delta in a temp variable
        Vector2 swipe = finger.SwipeDelta;

        if (swipe.x < -Mathf.Abs(swipe.y))
        {
            InfoText.text += "swipe left";
            GameManager.GM.cycle();
        } else if (swipe.x > Mathf.Abs(swipe.y)) {
            InfoText.text += "swipe right";
            int top = GameManager.GM.mostReady();
            GameManager.GM.goToTopOfList();
        } else {
            InfoText.text += "nothing found";
        }
    }

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

    public void rotateLeft()
    {
        if (!GameManager.GM.rotating)
        {
            GameManager.GM.rotating = true;
            GameManager.GM.rotDir = 1;
        }
    }

    public void rotateRight()
    {
        if (!GameManager.GM.rotating)
        {
            GameManager.GM.rotating = true;
            GameManager.GM.rotDir = -1;
        }
    }
}
