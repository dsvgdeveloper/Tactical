using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager SETMINST;

	// Use this for initialization
	void Start () {

        if (SETMINST = null)
        {
            SETMINST = new SettingsManager();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
