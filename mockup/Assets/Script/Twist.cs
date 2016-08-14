using UnityEngine;
using System.Collections;

public class Twist : MonoBehaviour {

    public void OnEnable()
    {
        Lean.LeanTouch.OnTwistDegrees += OnTwistDegrees;
    }

    public void OnDisable()
    {
        Lean.LeanTouch.OnTwistDegrees -= OnTwistDegrees;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTwistDegrees(float degrees)
    {
        if (Input.touchCount != 2) return;

        if (degrees >= GameManager.GM.rotateThreshold) {
            Debug.Log("twist occured");

            // begin rotation
            
            // take degrees and find the number of 45 degree increments

            // adjust for non exact increments

            // perform the correct number of rotations
        }
    }
}
