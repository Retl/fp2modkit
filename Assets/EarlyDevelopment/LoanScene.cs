using UnityEngine;
using System.Collections.Generic;

public class LoanScene : MonoBehaviour {

    public List<GameObject> carryOvers = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            UnityEngine.SceneManagement.Scene newScene = UnityEngine.SceneManagement.SceneManager.CreateScene("Custom Level Scene");
            UnityEngine.SceneManagement.Scene oldScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            foreach (GameObject g in carryOvers)
            {
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(g, newScene);
            }
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Custom Level Scene");
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
            UnityEngine.SceneManagement.SceneManager.UnloadScene(oldScene);

        }
	}
}
