using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public float swipeTime;
    public float maxSwipeTime = 0.2f;
    public float minSwipeDist = 10f;

    float startTimeT1;
    float startTimeT2;
    float endTimeT1;
    float endTimeT2;
    Vector2 startPosT1;
    Vector2 startPosT2;
    Vector2 endPosT1;
    Vector2 endPosT2;

    float oldAngle = 0;
    public GameObject cameraObj;
    public GameObject itemPanel;
    public GameObject skillPanel;
    Camera cam;

    // Use this for initialization
    void Start () {
        cam = cameraObj.GetComponent<Camera>() as Camera;
	}
	
	// Update is called once per frame
	void Update () {
 //       if (Input.touchCount > 0)
 //       {
 //           // use this space to check for gestures
 //           switch(Gesture.gesture.getSwipeType())
 //           {
 //               // swipe up on HUD
 //               case Gesture.SWIPEUP:
 //                   // set current character to attack state
 //                   swipeAttack();
 //                   break;
 //               // swipe left on hud
 //               case Gesture.SWIPELEFT:
 //                   // set current character to defend state
 //                   swipeDefense();
 //                   break;
 //               // swipe down on hud
 //               case Gesture.SWIPEDOWN:
 //                   // open and turn on skills panel
 //                   swipeSkill();
 //                   break;
 //               // swipe right on hud
 //               case Gesture.SWIPERIGHT:
 //                   // open and turn on item panel
 //                   swipeItem();
 //                   break;
 //               // swipe left on upper screen
 //               case Gesture.SWIPENEXTLEFT:
 //                   // set current to next most ready character
 //                   swipeLeft();
 //                   break;
 //               // swipe right on upper screen
 //               case Gesture.SWIPENEXTRIGHT:
 //                   // set current to next character in rotation
 //                   swipeRight();
 //                   break;
 //               // rotate - multiple touch - ????
 //               case Gesture.ROTATE:
 //                   // rotate camera by fixed ammount
 //                   rotateCamera();
 //                   break;
 //               // drag - single touch
 //               // do like in swipe but has much longer time or distance
 //               case Gesture.PAN:
 //                   panCamera();
 //                   // move camera
 //                   break;
 //               // pinch - get start and end for both touches and that they moved, like in swipe detection
 //               // if the magnitude between the 2 increased then zoom in else of magnitude decreases then zoom out
 //               case Gesture.PINCH:
 //                   // zoom camera
 //                   zoomCamera();
 //                   break;
 //               case Gesture.TAP:
 //                   // focus camera on selected character or if target was current character begin move
 //                   // or if action was selected and target is within range
 //                   if (Gesture.gesture.getTappedCharacter() != GameManager.GM.current) {
 //                       targetAnotherCharacter();
 //                   } else if (Gesture.gesture.getTappedCharacter() == GameManager.GM.current) {
 //                       startMovement();
 //                   } else if (GameManager.GM.isCurrentActing() &&
 //                               Gesture.gesture.getTappedCharacter() != GameManager.GM.current &&
 //                               ActionManager.ACTIONINST.isValidTarget(/*selected skill*/)) {
 //                       performAction();
 //                   }
 //                   break;
 //           }
 //       }
    }
    

    private void rotateCamera() {
        // rotate camera a fixed ammount based on rotate ammount
        // maintain isometric style
        GameManager.GM.rotating = true;
    }

    private void panCamera()
    {
        // pan camera
        GameManager.GM.panning = true;
    }

    private void zoomCamera()
    {
    }

    private void swipeRight()
    {
        // make camera focus on next character in rotation
    }

    private void swipeLeft()
    {
        // make camera focus on most ready party character
    }

    private void swipeAttack()
    {
        // for current character turn on attack circle
        (GameManager.GM.battleManager.GetComponent<CharacterController>() as CharacterController).setActionATK();
    }

    private void swipeSkill()
    {
        //open and activate skill panel
    }

    private void swipeItem()
    {
        //open and activate item panel
    }
    
    private void swipeDefense()
    {
        // for current character set action as defend
        // move to next most ready character
        (GameManager.GM.battleManager.GetComponent<CharacterController>() as CharacterController).setActionDEF();
    }

    private void targetAnotherCharacter()
    {
        //GameManager.GM.current = Gesture.gesture.getTappedCharacter()
        //    .GetComponent<CharacterController>() as CharacterController;
    }

    private void startMovement()
    {
    }

    private void performAction()
    {
    }
}
