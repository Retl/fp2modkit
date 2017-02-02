using UnityEngine;
using System.Collections;

public class InitModLoader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        string modLoaderName = "Mod Loader";

        if (GameObject.Find(modLoaderName) == null) {
            GameObject go = new GameObject();
            go.name = modLoaderName;
            go.AddComponent<ModLoader>();
            DontDestroyOnLoad(go);
        }
	}
	
}
