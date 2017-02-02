//// C# Example
//// Builds an asset bundle from the selected objects in the project view.
//// Once compiled go to "Menu" -> "Assets" and select one of the choices
//// to build the Asset Bundle

//using UnityEngine;
//using UnityEditor;

//public class ExportAssetBundles
//{
//    [MenuItem("Assets/Build AssetBundle From Selection - Track dependencies")]
//    static void ExportResource()
//    {
//        // Bring up save panel
//        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
//        if (path.Length != 0)
//        {
//            // Build the resource file from the active selection.
//            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
//            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, 0);
//            Selection.objects = selection;
//        }
//    }
//    [MenuItem("Assets/Build AssetBundle From Selection - No dependency tracking")]
//    static void ExportResourceNoTrack()
//    {
//        // Bring up save panel
//        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
//        if (path.Length != 0)
//        {
//            // Build the resource file from the active selection.
//            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path);
//        }
//    }
//}

using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Copy Assembly Bytes to Assets")]
    static void CopyAssemblyBytesToAssets()
    {
        try
        {
            System.IO.File.Copy("Library\\ScriptAssemblies\\Assembly-CSharp.dll", "Assets\\Assembly-CSharp.dll.bytes", true);
        }
        catch
        {
            UnityEngine.Debug.Log("Could not copy the Assembly to a .bytes file.");
        }
    }

    [MenuItem("Assets/Build AssetBundles (Win32)")]
    static void BuildAllAssetBundlesWin32()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Assets/Build AssetBundles (OSX)")]
    static void BuildAllAssetBundlesOSX()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
    }

    [MenuItem("Assets/Build AssetBundles (Linux)")]
    static void BuildAllAssetBundlesLinux()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneLinuxUniversal);
    }
}