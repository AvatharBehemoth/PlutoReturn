using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MajorFactionInfoDisplay : MonoBehaviour
{
    public string Factionname;
    public Sprite Factionflag;
    public string FactionText;
    public Vector3 mousepo;
    public static string factionChoice;

    public void OnMouseOver()
    {
        //Factionname = gameObject.name;
        MajorFactionScreen.onMouseEnter(Factionname, Factionflag, FactionText, mousepo);
        //If mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject." + gameObject.name);
    }

    public void OnMouseExit()
    {
        MajorFactionScreen.onMouseExit();
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
    }

    public void Update()
    {
        mousepo = Input.mousePosition;

    }

    private void ShowFactionInfoScreen()
    {
        MajorFactionScreen.onMouseEnter(Factionname, Factionflag, FactionText, mousepo);
    }
}
