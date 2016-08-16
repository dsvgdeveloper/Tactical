using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public Image moveBGImg;
    public Image moveJSImg;
    public bool fingerOnMoveJS = false;
    
    public Vector3 InputDirectionM { set; get; }
    public Vector3 moveStart;

    public Image actionBGImg;
    public Image actionJSImg;
    public bool fingerOnActionJS = false;
    
    public Vector3 InputDirectionA { set; get; }
    public Vector3 actionStart;

    public struct Finger
    {
        public bool boolean;
        public Touch touch;
        public PointerEventData pointer;
    }

    // Use this for initialization
    void Start () {
        InputDirectionM = Vector3.zero;
        InputDirectionA = Vector3.zero;
        moveStart = Vector3.zero;
        actionStart = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.GM.movingChar)
        {
            InfoText.text = "movingChar";
            Finger moveFinger = fingerOnMoveJoystick();
            Finger actionFinger = fingerOnActionJoystick();
            if (moveFinger.boolean)
            {
                InfoText.text += "moveJS";
                actMoveJoyStick(moveFinger);
                return;
            } else
            {
                fingerOnMoveJS = false;
            }

            if (actionFinger.boolean)
            {
                InfoText.text += "actJS";
                actActionJoyStick(actionFinger);
                return;
            } else
            {
                fingerOnActionJS = false;
            }
        }

        if (!fingerOnMoveJS && !fingerOnActionJS)
        {
            if (Input.touchCount == 1)
            {
                InfoText.text = "single";
                doSingleTouch();
            }
            else if (Input.touchCount == 2)
            {
                InfoText.text = "multi";
                doMultiTouch();
            }
        }
        else if (Lean.LeanTouch.Fingers.Count  < 1)
        {
            fingerOnMoveJS = false;
            fingerOnActionJS = false;
            swipe = false;
        }
	}

    public void doSingleTouch()
    {
        foreach (Lean.LeanFinger obj in Lean.LeanTouch.Fingers)
        {
            if (obj.Down)
            {
                //check for tap
                touchStart = obj.StartScreenPosition;
                startTime = Time.time;
                InfoText.text += "began";
            }
            else if (!swipe && obj.DeltaScreenPosition != Vector2.zero)
            //if ((t.phase == TouchPhase.Moved ||
            //  t.phase == TouchPhase.Stationary) && !swipe)
            {

                InfoText.text += "moving";

                if (!GameManager.GM.movingChar)
                {
                    foreach (CharacterStats chr in FindObjectsOfType<CharacterStats>())
                    {
                        chr.moveCircle.enabled = false;
                        chr.actionCircle.enabled = false;
                    }
                }

                if (Time.time - startTime < swipeTimeThreshold &&
                        Mathf.Abs(Input.touches[0].position.magnitude - touchStart.magnitude) > swipeDistThreshold)
                {
                    swipe = true;
                    OnFingerSwipe(obj);
                    InfoText.text += "swiped";
                }
                else if (!swipe && Time.time - startTime >= swipeTimeThreshold)
                {
                    //if (GameManager.GM.movingChar)
                    //{
                    //    Ray ray = Camera.main.ScreenPointToRay(Lean.LeanTouch.Fingers[0].StartScreenPosition);
                    //}
                    InfoText.text += "drag";

                    Vector3 pos = Camera.main.ScreenToViewportPoint(
                            obj.ScreenPosition - touchStart);

                    float x = pos.x * CamPanSpeed * Time.deltaTime;
                    float z = pos.y * CamPanSpeed * Time.deltaTime;

                    Vector3 move = new Vector3(x, 0, z);

                    GameManager.GM.cameraAxis.transform.Translate(move, Space.World);

                    boundCamera();
                }
            }
        }
    }

    public Finger fingerOnMoveJoystick()
    {
        Finger ret = new Finger();
        ret.boolean = false;

        foreach (Lean.LeanFinger obj in Lean.LeanTouch.Fingers)
        {
            if (obj.StartedOverGui)
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                //pointer.position = Input.mousePosition;
                pointer.position = obj.ScreenPosition;

                System.Collections.Generic.List<RaycastResult> raycastResults =
                    new System.Collections.Generic.List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (RaycastResult result in raycastResults)
                    {
                        if (result.gameObject.name.Equals("MoveJoyStickBG"))
                        {
                            ret.touch = Input.touches[obj.Index];
                            ret.pointer = pointer;
                            ret.boolean = true;
                            return ret;
                        }
                    }
                }
            }
        }
        return ret;
    }

    public void actMoveJoyStick(Finger moveFinger)
    {
        if (moveFinger.touch.phase == TouchPhase.Began)
        {
            moveStart = GameManager.GM.getCurrent().transform.position;
            fingerOnMoveJS = true;
        } else if (moveFinger.touch.phase == TouchPhase.Moved ||
            moveFinger.touch.phase == TouchPhase.Stationary)
        {
            Vector2 pos = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                moveBGImg.rectTransform,
                moveFinger.pointer.position,
                moveFinger.pointer.pressEventCamera,
                out pos))
            {
                pos.x = (pos.x / moveBGImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / moveBGImg.rectTransform.sizeDelta.y);

                float x = (moveBGImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (moveBGImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                InputDirectionM = new Vector3(x, 0, y);
                InputDirectionM = (InputDirectionM.magnitude > 1) ? InputDirectionM.normalized : InputDirectionM;

                moveJSImg.rectTransform.anchoredPosition = new Vector3(
                    InputDirectionM.x * (moveBGImg.rectTransform.sizeDelta.x / 3),
                    InputDirectionM.z * (moveBGImg.rectTransform.sizeDelta.y / 3));

                InfoText.text = InputDirectionM.x + ", " + InputDirectionM.z + ", " + InputDirectionM.z;
            }
        } else
        {
            InputDirectionM = Vector3.zero;
            moveJSImg.rectTransform.anchoredPosition = Vector3.zero;
            fingerOnMoveJS = false;
        }
    }

    public Finger fingerOnActionJoystick()
    {
        Finger ret = new Finger();
        ret.boolean = false;

        foreach (Lean.LeanFinger obj in Lean.LeanTouch.Fingers)
        {
            if (obj.StartedOverGui)
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                //pointer.position = Input.mousePosition;
                pointer.position = obj.ScreenPosition;

                System.Collections.Generic.List<RaycastResult> raycastResults =
                    new System.Collections.Generic.List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (RaycastResult result in raycastResults)
                    {
                        if (result.gameObject.name.Equals("ActionJoyStickBG"))
                        {
                            ret.touch = Input.touches[obj.Index];
                            ret.pointer = pointer;
                            ret.boolean = true;
                            return ret;
                        }
                    }
                }
            }
        }
        return ret;
    }

    public void actActionJoyStick(Finger actionFinger)
    {
        if (actionFinger.touch.phase == TouchPhase.Began)
        {
            fingerOnActionJS = true;
            actionStart = GameManager.GM.getCurrent().transform.position;
        }
        else if (actionFinger.touch.phase == TouchPhase.Moved ||
          actionFinger.touch.phase == TouchPhase.Stationary)
        {
            Vector2 pos = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                actionBGImg.rectTransform,
                actionFinger.pointer.position,
                actionFinger.pointer.pressEventCamera,
                out pos))
            {
                pos.x = (pos.x / actionBGImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / actionBGImg.rectTransform.sizeDelta.y);

                float x = (actionBGImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (actionBGImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                InputDirectionA = new Vector3(x, 0, y);
                InputDirectionA = (InputDirectionA.magnitude > 1) ? InputDirectionA.normalized : InputDirectionA;

                actionJSImg.rectTransform.anchoredPosition = new Vector3(
                    InputDirectionA.x * (actionBGImg.rectTransform.sizeDelta.x / 3),
                    InputDirectionA.z * (actionBGImg.rectTransform.sizeDelta.y / 3));
            }
        }
        else
        {
            InputDirectionA = Vector3.zero;
            actionJSImg.rectTransform.anchoredPosition = Vector3.zero;
            fingerOnMoveJS = false;
        }
    }

    public void doMultiTouch()
    {
        InfoText.text += "pinch occured";

        //Vector3 camPos = GameManager.GM.camera.transform.position;
        //Vector3 camAxis = GameManager.GM.cameraAxis.transform.position;

        //float x = camPos.x - camAxis.x; 
        //float y = camPos.y - camAxis.y; 
        //float z = camPos.z - camAxis.z;

        //GameManager.GM.camera.transform.position =
        //        new Vector3(x * Lean.LeanTouch.PinchScale + camAxis.x,
        //        y * Lean.LeanTouch.PinchScale + camAxis.y,
        //        z * Lean.LeanTouch.PinchScale + camAxis.z);

        GameManager.GM.camera.orthographicSize *= Lean.LeanTouch.PinchScale;
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
            GameManager.GM.rotDir = -1;
        }
    }

    public void rotateRight()
    {
        if (!GameManager.GM.rotating)
        {
            GameManager.GM.rotating = true;
            GameManager.GM.rotDir = 1;
        }
    }

    public void whatGUI(Lean.LeanFinger finger)
    {
    }
}
