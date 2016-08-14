using UnityEngine;
using System.Collections;

public class GraphicsManager : MonoBehaviour {
    public static GraphicsManager GRAPHMINST;
	// Use this for initialization
	void Start () {

        if (GRAPHMINST = null)
        {
            GRAPHMINST = new GraphicsManager();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
