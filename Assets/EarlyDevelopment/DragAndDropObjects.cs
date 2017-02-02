using UnityEngine;
using System.Collections;

public class DragAndDropObjects : MonoBehaviour {

    public GameObject selectedObject = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            this.WriteToDazzleFile();
        }*/

        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject g in gameObjects)
            {
                Renderer r = g.GetComponent<Renderer>();
                if (r != null && r.isVisible)
                {
                    Vector3 screenpos = Camera.main.WorldToScreenPoint(g.transform.position);
                    float closest = 200f;
                    float dist = Vector2.Distance(Input.mousePosition, screenpos);
                    if (dist < closest)
                    {
                        closest = dist;
                        selectedObject = g;
                        //string txt = g.name + " " + g.transform.position.ToString();
                        //GUI.Label(new Rect(screenpos.x + 32f, Screen.height - (screenpos.y + 32f), 200f, 40f), txt);
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            selectedObject = null;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject g in gameObjects)
            {
                Renderer r = g.GetComponent<Renderer>();
                if (r != null && r.isVisible)
                {
                    Vector3 screenpos = Camera.main.WorldToScreenPoint(g.transform.position);
                    float closest = 999999f;
                    float dist = Vector2.Distance(Input.mousePosition, screenpos);
                    if (dist < closest)
                    {
                        closest = dist;
                        selectedObject = g;
                        //string txt = g.name + " " + g.transform.position.ToString();
                        //GUI.Label(new Rect(screenpos.x + 32f, Screen.height - (screenpos.y + 32f), 200f, 40f), txt);
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (selectedObject != null) { UnityEngine.Object.Destroy(selectedObject); }
            selectedObject = null;
        }

        if (selectedObject != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedObject.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, selectedObject.transform.position.z);
            Vector3 screenpos = Camera.main.WorldToScreenPoint(selectedObject.transform.position);

            string txt = selectedObject.name;
            GUI.Label(new Rect(screenpos.x + 32f, (screenpos.y + 32f), 200f, 40f), txt);
            GUI.Label(new Rect(0 + 32f, (0 + 32f), 200f, 40f), txt);
        }
    }

    //private void OnGUI() {
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
    //        foreach (GameObject g in gameObjects)
    //        {
    //            Renderer r = g.GetComponent<Renderer>();
    //            if (r != null && r.isVisible)
    //            {
    //                Vector3 screenpos = Camera.main.WorldToScreenPoint(g.transform.position);
    //                float closest = 200f;
    //                float dist = Vector2.Distance(Input.mousePosition, screenpos);
    //                if (dist < closest)
    //                {
    //                    closest = dist;
    //                    selectedObject = g;
    //                    //string txt = g.name + " " + g.transform.position.ToString();
    //                    //GUI.Label(new Rect(screenpos.x + 32f, Screen.height - (screenpos.y + 32f), 200f, 40f), txt);
    //                }
    //            }
    //        }
    //    }
    //    if (Input.GetKeyUp(KeyCode.Y))
    //    {
    //        selectedObject = null;
    //    }
    //    if (selectedObject != null)
    //    {
    //        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        selectedObject.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, selectedObject.transform.position.z);
    //    }
    //} 
}
