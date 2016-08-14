using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {
    public static ActionManager ACTIONINST;

	// Use this for initialization
	void Start () {
	    if (ACTIONINST == null)
        {
            ACTIONINST = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isValidTarget()
    {
        return false;
    }
}
