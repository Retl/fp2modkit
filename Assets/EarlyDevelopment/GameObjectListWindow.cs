using UnityEngine;
using System.Collections;

public class GameObjectListWindow : MonoBehaviour {

    public Rect windowRect0 = new Rect(20, 20, 120, 50);
    public Rect windowRect1 = new Rect(20, 100, 120, 50);
    void OnGUI()
    {
        GUI.color = Color.red;
        windowRect0 = GUI.Window(0, windowRect0, DoMyWindow, "Red Window");
        GUI.color = Color.green;
        windowRect1 = GUI.Window(1, windowRect1, DoMyWindow, "Green Window");
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
            print("Got a click in window with color " + GUI.color);

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}
