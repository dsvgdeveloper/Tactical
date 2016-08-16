using UnityEngine;
using System.Collections;

public class Pinch : MonoBehaviour {

    [Tooltip("The minimum field of view angle we want to zoom to")]
    public float Minimum = 40.0f;

    [Tooltip("The maximum field of view angle we want to zoom to")]
    public float Maximum = 90.0f;

    float pinchThreshold;
    float rotateThreshold;
    float dragThreshold;

    // Use this for initialization
    void Start() {
        pinchThreshold = (FindObjectOfType<GameManager>()).pinchThreshold;
        rotateThreshold = (FindObjectOfType<GameManager>()).rotateThreshold;
        dragThreshold = (FindObjectOfType<GameManager>()).dragThreshold;
    }

    protected virtual void LateUpdate() {

        // Does the main camera exist?
        if (Camera.main != null)
        {
            ////Make sure the pinch scale is valid
            //if (Lean.LeanTouch.PinchScale > pinchThreshold)
            //{
            //    // Store the old size in a temp variable
            //    var orthographicSize = Camera.main.orthographicSize;

            //    // Scale the size based on the pinch scale
            //    orthographicSize /= Lean.LeanTouch.PinchScale;

            //    // Clamp the size to out min/max values
            //    orthographicSize = Mathf.Clamp(orthographicSize, Minimum, Maximum);

            //    // Set the new size
            //    Camera.main.orthographicSize = orthographicSize;
            //}

            //// Make sure the pinch scale is valid
            //if (Lean.LeanTouch.PinchScale > 0.0f)
            //{
            //// Store the old FOV in a temp variable
            //float fieldOfView = Camera.main.fieldOfView;

            //// Scale the FOV based on the pinch scale
            //fieldOfView /= Lean.LeanTouch.PinchScale;

            //// Clamp the FOV to out min/max values
            //fieldOfView = Mathf.Clamp(fieldOfView, Minimum, Maximum);

            //// Set the new FOV
            //Camera.main.fieldOfView = fieldOfView;
            //}

            Vector3 camPos = GameManager.GM.camera.transform.position;

            GameManager.GM.camera.transform.position =
                new Vector3(camPos.x * Lean.LeanTouch.PinchScale,
                camPos.y * Lean.LeanTouch.PinchScale,
                camPos.z * Lean.LeanTouch.PinchScale);
        }
    }
}
