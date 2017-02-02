using UnityEngine;
using System.Collections;

public class Tooltipper : MonoBehaviour {

    string strOutput;

    void OnGUI()
    {
        /*if (Input.GetKey(KeyCode.T))
        {
            string text = "((You see nothing special...))";
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D raycastHit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (raycastHit.collider != null && raycastHit.collider.gameObject != null)
            {
                text = raycastHit.collider.gameObject.name + " " + raycastHit.collider.gameObject.transform.position.ToString();
            }
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y + 16f, 200f, 40f), text);
        }*/

        if (Input.GetKey(KeyCode.T))
        {
            strOutput = "";
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject g in gameObjects)
            {
                strOutput += g.name + ": " + g.transform.position.ToString() + "; \n";
                Renderer r = g.GetComponent<Renderer>();
                if (r != null && r.isVisible)
                {
                    Vector3 screenpos = Camera.main.WorldToScreenPoint(g.transform.position);
                    if (Vector2.Distance(Input.mousePosition, screenpos) < 50f)
                    {
                        string txt = g.name + " " + g.transform.position.ToString();
                        GUI.Label(new Rect(screenpos.x + 32f, Screen.height - (screenpos.y + 32f), 200f, 40f), txt);
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            strOutput = "";
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject g in gameObjects)
            {
                strOutput += g.name + ": " + g.transform.position.ToString() + "; \n";
                Renderer r = g.GetComponent<Renderer>();
                if (r != null && r.isVisible)
                {
                    Vector3 screenpos = Camera.main.WorldToScreenPoint(g.transform.position);
                    string txt = g.name + " " + g.transform.position.ToString();
                    GUI.Label(new Rect(screenpos.x + 32f, Screen.height - (screenpos.y + 32f), 200f, 40f), txt);
                }
            }
        }

        GUI.Label(new Rect(0 + 32f, 128, 200f, 40f), "@~Dazl Debug is Running~@");
        GUI.TextArea(new Rect(0 + 32f, Screen.height - 200f - 32f, 200f, 200f), strOutput);

    }
}
