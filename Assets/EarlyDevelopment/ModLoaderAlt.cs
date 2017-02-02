using UnityEngine;
using System.IO;
using System.Collections;

public class ModLoaderAlt : MonoBehaviour {

    // Use this for initialization
    IEnumerator Start () {
        string cd = Directory.GetCurrentDirectory();
        string modDirectory = cd + "\\Mods\\";
        string[] modSubDirectories;

        //Debug.Log("Current Directory:  " + cd);

        //Debug.Log("Contents:  " + System.String.Join(", ", Directory.GetFiles(cd)));
        //Debug.Log("*.png Contents:  " + System.String.Join(", ", Directory.GetFiles(cd, "*.png")));

        if (!Directory.Exists(modDirectory)) { Directory.CreateDirectory(modDirectory); }
        else {
            Debug.Log("Mod Directory:  " + modDirectory);
            Debug.Log("Mod Folders: " + System.String.Join(", ", Directory.GetDirectories(modDirectory)));

            modSubDirectories = Directory.GetDirectories(modDirectory);

            foreach (string currentModDir in modSubDirectories) {
                Debug.Log("Checking for assets in:  " + currentModDir);
                Debug.Log("Contents:  " + System.String.Join(", ", Directory.GetFiles(currentModDir)));
                Debug.Log("*.png Contents:  " + System.String.Join(", ", Directory.GetFiles(currentModDir, "*.png")));
                if (File.Exists(currentModDir + "\\thumb.png")) {
                    // Load the thumbnail and do stuff with it.
                    Debug.Log("Found a thumbnail!");
                }
                if (File.Exists(currentModDir + "\\mod")) {
                    Debug.Log("Mod file appears to exist.");
                    string path = currentModDir + "\\mod";

                    {
                        Debug.Log("Loading mod at: " + path);
                        WWW www = new WWW("file://" + path);
                        //www.LoadFromCacheOrDownload(path, "");
                        yield return www;
                        // while (!www.isDone) { }

                        TextAsset[] tas = www.assetBundle.LoadAllAssets<TextAsset>();
                        System.Collections.Generic.List<string> tasList = new System.Collections.Generic.List<string>();
                        foreach (TextAsset ta in tas) { tasList.Add(ta.name); }

                        Debug.Log("TextAssets loaded: " + string.Join(", ", tasList.ToArray()));

                        // Load the TextAsset object
                        TextAsset txt = www.assetBundle.LoadAsset<TextAsset>("ModAssembly.dll.bytes");
                        Debug.Log("txt.name: " + txt.name);
                        Debug.Log(txt ? "Found ModAssembly. **" + txt.name : "NOPE!");
                        txt = www.assetBundle.LoadAsset<TextAsset>("ModAssembly");
                        Debug.Log(txt ? "Found ModAssembly. Applying to ModMain object." : "NOPE!");

                        // Load the assembly and get a type (class) from it
                        if (txt)
                        {
                            Debug.Log("Found ModAssembly. Applying to ModMain object.");
                            var assembly = System.Reflection.Assembly.Load(txt.bytes);
                            var type = assembly.GetType("ModMainBehavior");
                            GameObject go = Instantiate(www.assetBundle.LoadAsset<GameObject>("ModMain"));
                            go.AddComponent(type);
                        }
                        else
                        {
                            Debug.Log("Did not find Assembly file.");
                            GameObject go = Instantiate(www.assetBundle.LoadAsset<GameObject>("ModMain"));
                        }




                        GameObject[] gos = www.assetBundle.LoadAllAssets<GameObject>();
                        System.Collections.Generic.List<string> gosList = new System.Collections.Generic.List<string>();
                        foreach (GameObject g in gos) { gosList.Add(g.name); }

                        Debug.Log("Assets loaded: " + string.Join(", ", gosList.ToArray()));
                    }

                }
            }
        }

        if (!Directory.Exists(modDirectory)) { Directory.CreateDirectory(modDirectory); }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
