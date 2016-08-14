using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    public bool acting = false;         //this says that the current character is acting,
                                        // or in other words has a selected action
    public enum ACTION {
        ATK = 0,
        ITEM = 1,
        SKILL = 2,
        DEF = 3,
        NONE = -1
    }

    ACTION curAction = ACTION.NONE;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.touchCount > 0) {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                // focus on character
                // show circle
                // make ray not be able to hit anything that isnt terrain stuff
            }
            
        }

        // if draged
        // move circle

        // if released move character to that point along that route
	}

    public void setActionATK()
    {
        curAction = ACTION.ATK;
        acting = true;
    }

    public void setActionDEF()
    {
        curAction = ACTION.DEF;
        acting = true;
    }

    public void setActionSKILL()
    {
        curAction = ACTION.SKILL;
        acting = true;
    }

    public void setActionITEM()
    {
        curAction = ACTION.ITEM;
        acting = true;
    }

    public void setActionNONE()
    {
        curAction = ACTION.NONE;
        acting = false;
    }
}
