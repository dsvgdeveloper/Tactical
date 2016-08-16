using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public GameObject uiManager;
    public GameObject battleManager;

    // connect soundManager
    // connect graphicsManager
    // connect settingsManager
    // vector container for characters
    public List<CharacterStats> party;
    public List<GameObject> CharList;
    public int current;
    public CharacterStats target;
    // vector container for item storage
    public List<Item> storage;
    public bool gameOver = false;
    public bool movingChar = false;

    // camera controls
    // these need to be moved over to game manager
    public Camera camera;                   // main camera
    public Gesture gesture;

    public bool rotating = false;          // flag for making the script run
    public int rotDir = 1;                 // roatation direction 1=   , -1=

    public bool zooming = false;
    public float zoomFactor = 1f;

    public bool panning = false;
    public Vector2 panVec;                  // x gets added to x, y gets added to z
    public Vector3 deltaCamPos;             // this is a vector that always always the camera 
                                            // to be a set distance from the camera axis
    public GameObject cameraAxis;
    public GameObject HUD;
    public GameObject targetHUD;
    public GameObject joysticks;

    public float pinchThreshold = 0.1f;
    public float rotateThreshold = 10f;
    public float dragThreshold = .5f;
    public float swipeTime = .5f;

    public int targetNum = 0;
    public int numTargets = 0;

    public GameObject moveToTarget;
    public bool moving = false;

    public float moveSpeed = 15f;

    void Awake ()
    {
    }

    // Use this for initialization
    void Start () {
        cameraAxis = GameObject.Find("GameManager");

        foreach (CharacterStats obj in FindObjectsOfType<CharacterStats>()) {
            if (obj.tag == "Player")
            {
                obj.Index = numTargets;
                numTargets++;
                party.Add(obj);
            }
            else if (obj.tag == "Enemy")
            {
                target = obj;
            }
        }

        if (GM == null)
        {
            GM = this;
        }

        // log in player
        // load from server
        GM.gesture = GameObject.Find("Gesture").GetComponent<Gesture>();

        current = 0;

        for (int i = 1; i < party.Count; i++)
        {
            if (party[current].MAXCT > party[i].MAXCT)
            {
                current = i;
            }
        }

        cameraAxis.transform.position = party[current].transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            party[current].health.Value -= 10f;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            party[current].health.Value += 10f;
        }

        if (gameOver)
        {
            // set game over
        }
    }

    public int mostReady()
    {
        Debug.Log("mostReady ready");
        int index = 0;

        for (int i = 0; i < party.Count; i++)
        {
            // get characters current cp
            if (party[i].ct/party[i].MAXCT >= party[index].ct/party[index].MAXCT)
            {
                // save the most ready as index
                index = i;
            }

        }

        return index;
    }
    
    public void switchTouchingScreen()
    {
        movingChar = !movingChar;
        uiManager.GetComponent<UIManager>().enabled = !uiManager.GetComponent<UIManager>().enabled;
        battleManager.GetComponent<CharacterController>().enabled =
            !battleManager.GetComponent<CharacterController>().enabled;
    }

    public bool isCurrentActing()
    {
        return (battleManager.GetComponent<CharacterController>() as CharacterController).acting;
    }

    public void focusOn(CharacterStats target)
    {
        // focus on tapped object
        moving = true;
        moveToTarget = target.gameObject;

        if (target.tag.Equals("Player")) {
            focusOnPlayer(target);
        } else if (target.tag.Equals("Enemy")) {
            focusOnEnemy(target);
        }
    }

    void focusOnEnemy(CharacterStats target)
    {
        if (!targetHUD.activeInHierarchy)
        {
            targetHUD.SetActive(true);
        }
        target.moveCircle.enabled = true;
    }

    void focusOnPlayer(CharacterStats tapped)
    {
        // check if its the current focus and a player character
        // if so go to character controlling

        // else switch focus to it

        // if it is a player character set curretn position in roster to that character
        // so that a swipe will move to next character in roster
        for (int i = 0; i < party.Count; i++)
        {
            if ((tapped.name.ToLower()).Equals(party[i].name.ToLower()))
            {
                if (tapped.ct / tapped.MAXCT >= 1)
                {
                    current = i;
                }
                targetNum = i;
                break;
            }
        }

        if ((tapped.name.ToLower()).Equals(party[current].name.ToLower()))
        {
            movingChar = true;
            HUD.SetActive(false);
            joysticks.SetActive(true);
            party[current].moveCircle.enabled = true;
            party[current].actionCircle.enabled = true;
        }
    }

    public void cycle()
    {
        Debug.Log("cycle");
        //Vector3 camPos = camera.transform.position;
        //Vector3 camAxis = cameraAxis.transform.position;

        targetNum++;

        if (targetNum >= numTargets || targetNum < 0)
        {
            targetNum = 0;
        }

        moving = true;
        moveToTarget = party[targetNum].gameObject;
        //cameraAxis.transform.position = party[targetNum].transform.position;
        //camera.transform.position = cameraAxis.transform.position + (camPos - camAxis);

        if (party[targetNum].ct / party[targetNum].MAXCT >= 1f)
        {
            current = targetNum;
        }
    }

    public void goToTopOfList()
    {
        int top;

        top = mostReady();
        Debug.Log("top of list");
        //Vector3 camPos = camera.transform.position;
        //Vector3 camAxis = cameraAxis.transform.position;

        cameraAxis.transform.position = party[top].transform.position;
        //camera.transform.position = cameraAxis.transform.position + (camPos - camAxis);

        targetNum = top;
    }

    public float getCT()
    {
        return party[current].ct;
    }

    public CharacterStats getCurrent()
    {
        return party[current];
    }

    public CharacterStats getTarget()
    {
        return target;
    }
}
