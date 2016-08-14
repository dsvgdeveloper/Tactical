using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour
{
    // This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
    public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    void OnEnable()
    {
        Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
    }

    void OnDisable()
    {
        Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFingerSwipe(Lean.LeanFinger finger)
    {
        if (Input.touchCount != 1 ||
            Lean.LeanTouch.TwistDegrees >= GameManager.GM.rotateThreshold ||
            GameManager.GM.rotating) return;

        if (finger.StartedOverGui)
        {
            // check directions
            UISwipe(finger);
        } else {
            // check that it didnt start over a character/ Raycast information
            var ray = finger.GetStartRay();
            var hit = default(RaycastHit);

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
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

        if (swipe.x < -Mathf.Abs(swipe.y)) {
            Debug.Log("UI Swipe Left");
        } else if (swipe.x > Mathf.Abs(swipe.y)) {
            Debug.Log("UI Swipe Right");
        } else if (swipe.y < -Mathf.Abs(swipe.x)) {
            Debug.Log("UI Swipe Down");
        } else if (swipe.y > Mathf.Abs(swipe.x)) {
            Debug.Log("UI Swipe Up");
        }
    }

    public void nonUISipe(Lean.LeanFinger finger)
    {
        // Store the swipe delta in a temp variable
        Vector2 swipe = finger.SwipeDelta;

        if (swipe.x < -Mathf.Abs(swipe.y))
        {
            Debug.Log("swipe left");
            GameManager.GM.cycle();
        }
        else if (swipe.x > Mathf.Abs(swipe.y))
        {
            Debug.Log("swipe right");
            int top = GameManager.GM.mostReady();
            GameManager.GM.goToTopOfList();
        }
    }
}
