using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tap : MonoBehaviour
{
    [Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
    public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    [Tooltip("The previously selected GameObject")]
    public GameObject SelectedGameObject;

    public void OnEnable()
    {
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
    }

    public void OnDisable()
    {
        Lean.LeanTouch.OnFingerTap -= OnFingerTap;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFingerTap (Lean.LeanFinger finger)
    {
        if (Input.touchCount > 1) return;

        if (GameManager.GM.movingChar)
        {
            OnFingerTapMoving(finger);
        }
        else
        {
            OnFingerTapActionSelect(finger);
        }
    }

    public void OnFingerTapActionSelect(Lean.LeanFinger finger)
    {
        Ray ray = finger.GetRay();
        RaycastHit hit = default(RaycastHit);

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        //pointer.position = Input.GetTouch(0).position;

        System.Collections.Generic.List<RaycastResult> raycastResults =
            new System.Collections.Generic.List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (RaycastResult result in raycastResults)
            {
                Debug.Log(result.gameObject.name);
            }
            //if (raycastResults[0].gameObject.tag == "InventoryItem")
        }
        else
        {
            ray = GameManager.GM.camera.ScreenPointToRay(finger.StartScreenPosition);

            // Raycast information

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
                try
                {
                    GameManager.GM.focusOn(hit.collider.gameObject.GetComponent<CharacterStats>());
                }
                catch (MissingReferenceException e)
                {
                    Debug.LogError(e);
                }
                catch (MissingComponentException e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public void OnFingerTapMoving(Lean.LeanFinger finger)
    {
        Ray ray = finger.GetRay();
        RaycastHit hit = default(RaycastHit);

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        //pointer.position = Input.GetTouch(0).position;

        System.Collections.Generic.List<RaycastResult> raycastResults =
            new System.Collections.Generic.List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (RaycastResult result in raycastResults)
            {
                Debug.Log(result.gameObject.name);
            }
            //if (raycastResults[0].gameObject.tag == "InventoryItem")
        }
        else
        {
            ray = GameManager.GM.camera.ScreenPointToRay(finger.StartScreenPosition);

            // Raycast information

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
                try
                {
                    GameManager.GM.focusOn(hit.collider.gameObject.GetComponent<CharacterStats>());
                }
                catch (MissingReferenceException e)
                {
                    Debug.LogError(e);
                }
                catch (MissingComponentException e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
