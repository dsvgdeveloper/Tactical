using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public static SoundManager SMINST;

	// Use this for initialization
	void Start () {

        if (SMINST = null)
        {
            SMINST = new SoundManager();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
