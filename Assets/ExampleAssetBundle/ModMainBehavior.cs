using UnityEngine;
using System.Collections;

public class ModMainBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

    }

    // Update is called once per frame
    void OnGUI() {
        if (Camera.main)
        {
            GUI.Label(new Rect(0, 0, 400, 400), "Main Camera Position: " + Camera.main.transform.position.ToString());
        }
        else
        {
            GUI.Label(new Rect(0, 0, 400, 400), "Could not find Main Camera.");
        }
    }
}
