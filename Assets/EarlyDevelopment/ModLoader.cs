using UnityEngine;
using System.IO;
using System.Collections;

public class ModLoader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        string cd = Directory.GetCurrentDirectory();
        string modDirectory = cd + "\\Mods\\";
        string[] modSubDirectories;

        //Debug.Log("Current Directory:  " + cd);

        //Debug.Log("Contents:  " + System.String.Join(", ", Directory.GetFiles(cd)));
        //Debug.Log("*.png Contents:  " + System.String.Join(", ", Directory.GetFiles(cd, "*.png")));

        if (!Directory.Exists(modDirectory)) { Directory.CreateDirectory(modDirectory); }
        else
        {
            Debug.Log("Mod Directory:  " + modDirectory);
            Debug.Log("Mod Folders: " + System.String.Join(", ", Directory.GetDirectories(modDirectory)));

            modSubDirectories = Directory.GetDirectories(modDirectory);

            foreach (string currentModDir in modSubDirectories)
            {
                Debug.Log("Checking for assets in:  " + currentModDir);
                Debug.Log("Contents:  " + System.String.Join(", ", Directory.GetFiles(currentModDir)));
                Debug.Log("*.png Contents:  " + System.String.Join(", ", Directory.GetFiles(currentModDir, "*.png")));
                if (File.Exists(currentModDir + "\\thumb.png"))
                {
                    // Load the thumbnail and do stuff with it.
                    Debug.Log("Found a thumbnail!");
                }
                //if (File.Exists(currentModDir + "\\mod") && false)
                //{
                //    Debug.Log("Mod file appears to exist.");
                //    LoadAssetBundleFromPath(currentModDir + "\\mod");
                //}
                string[] assetBundleFileList = Directory.GetFiles(currentModDir, "*.unity3d");
                foreach (string currentAssetBundleFile in assetBundleFileList)
                {
                    Debug.Log("AssetBundles appear to exist.");
                    LoadAssetBundleFromPath(currentAssetBundleFile);
                }
            }
        }

        if (!Directory.Exists(modDirectory)) { Directory.CreateDirectory(modDirectory); }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadAssetBundleFromPath(string path)
    {
        Debug.Log("Loading mod at: " + path);
        AssetBundle bun = AssetBundle.LoadFromFile(path);
        //bun.laod

        // If we want to actually use these scripts in the other objects, we'll want to finish adding it to the assembly first.
        TextAsset txt = null;
        System.Type[] assemblyTypes = new System.Type[1];
        bool isSceneBundle = false;

        try
        {
            TextAsset[] tas = bun.LoadAllAssets<TextAsset>();
            System.Collections.Generic.List<string> tasList = new System.Collections.Generic.List<string>();
            foreach (TextAsset ta in tas) { tasList.Add(ta.name); }
            Debug.Log("TextAssets loaded: " + string.Join(", ", tasList.ToArray()));
            

            if (tasList.Count > 0)
            {
                // Load the TextAsset object
                foreach (TextAsset ta in tas)
                {
                    txt = ta;
                    Debug.Log("taName: " + txt.name);
                    if (txt.name.Contains(".dll"))
                    {
                        Debug.Log("txt.name: " + txt.name);
                        Debug.Log(txt ? "Found ModAssembly. **" + txt.name : "NOPE!");
                        //txt = bun.LoadAsset<TextAsset>("ModAssembly");
                        //Debug.Log(txt ? "Found ModAssembly. Applying to ModMain object." : "NOPE!");

                        // Load the assembly and get a type (class) from it
                        if (txt)
                        {
                            //Debug.Log("Found ModAssembly. Applying to ModMain object.");
                            Debug.Log("Found ModAssembly, Loading into Current Assembly.");
                            var asm = System.Reflection.Assembly.Load(txt.bytes);
                            assemblyTypes = asm.GetTypes();
                            foreach (System.Reflection.Module m in asm.GetLoadedModules()) { Debug.Log("Module: " + m.ToString()); }
                            foreach (System.Type t in asm.GetExportedTypes()) { Debug.Log("Exported Types: " + t.ToString()); }

                            //var type = assembly.GetType("ModMainBehavior");
                            //GameObject go = Instantiate(bun.LoadAsset<GameObject>("ModMain"));
                            //go.AddComponent(type);
                        }
                    }
                }
            }
        }
        catch (System.InvalidOperationException e) 
        {
            Debug.Log("Probably trying to call the operation on a bundle that's just a scene file: " + e);
            isSceneBundle = true;
        }


        if (!isSceneBundle)
        {
            UnityEngine.Object[] objs = bun.LoadAllAssets<UnityEngine.Object>();
            System.Collections.Generic.List<string> objsList = new System.Collections.Generic.List<string>();
            foreach (UnityEngine.Object obj in objs) { objsList.Add(obj.name); }
            Debug.Log("ALL Objects loaded: " + string.Join(", ", objsList.ToArray()));

            UnityEngine.GameObject[] gos = bun.LoadAllAssets<UnityEngine.GameObject>();
            System.Collections.Generic.List<string> gosList = new System.Collections.Generic.List<string>();
            foreach (UnityEngine.GameObject go in gos) { gosList.Add(go.name); }
            Debug.Log("GameObjects loaded: " + string.Join(", ", gosList.ToArray()));

            if (!txt)
            {
                Debug.Log("Did not find Assembly file.");
            }

            foreach (GameObject g in gos)
            {
                if (g.name.Contains("_autospawn")) { Object.Instantiate(g); }
            }

            if (assemblyTypes.Length > 0)
            {
                GameObject g = new GameObject();
                foreach (System.Type at in assemblyTypes)
                {
                    Debug.Log("Checking Component: " + at.Name);
                    if (at.Name.Contains("_autoscript"))
                    {
                        Debug.Log("Adding Component: " + at.Name);
                        g.AddComponent(at);
                    }
                }
            }
        }
        else 
        {
            Debug.Log("Scene paths are probably: " + string.Join(",\n", bun.GetAllScenePaths()));
        }
    }
}
