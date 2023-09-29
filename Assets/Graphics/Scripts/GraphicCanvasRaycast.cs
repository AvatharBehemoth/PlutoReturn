using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;

public class GraphicCanvasRaycast : MonoBehaviour
{
    //from https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html
    //Attach this script to your Canvas GameObject.
    //Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
    //Also make sure you have an EventSystem in your hierarchy.

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public List<RaycastResult> results;
    public RaycastResult result { get; set; }
    public bool messagesent = false;
    public bool measureselected = false;
    public bool possibleresponschosen = false;
    Option.option tempoption;
    public float time;
    int number = 0;

    void Start()
    {
        //Fetch the Raycaster
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System
        m_EventSystem = GetComponent<EventSystem>();
        results = new List<RaycastResult>();
    }

    void Update()
    {
        time = Time.time;
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                //unity has a "problem" system level error. Menu's which arent active still are regarded as 
                //active. The easy way to deal with many simultanious result.gameobjects
                //is to change the location of the menus

                if (result.gameObject.name == "Yes")
                {
                    GameObject gm = result.gameObject;
                    MenuState.i.CheckActive();
                    MenuState.i.CheckButtonPressed();
                    MenuState.i.CheckButton(gm);
                }
                if (result.gameObject.name == "No")
                {
                    GameObject gm = result.gameObject;
                    MenuState.i.CheckActive();
                    MenuState.i.CheckButtonPressed();
                    MenuState.i.CheckButton(gm);
                }
                if (result.gameObject.name == "NewGame")
                {
                    GameObject gm = result.gameObject;
                    BeginState.i.Setmenuactive();
                    StartCoroutine(delay());
                    BeginState.i.Newgame();
                }
                if (result.gameObject.name == "Quit")
                {
                    GameObject gm = result.gameObject;
                    BeginState.i.Quit();
                }
                if ((result.gameObject.tag == "department") && 
                    (GameController.i.statemachine.CurrentState == MapState.i))
                {
                    GameObject gm = result.gameObject;
                    if (result.gameObject.name == "MayorsOffice")
                    {
                        DepartmentState.i.FillMenumayor(gm);
                        GameController.i.statemachine.Push(DepartmentState.i);
                    }
                    else
                    {
                        if (!DepartmentState.i.departmentvariables)
                        DepartmentState.i.FillMenudep(gm);
                        GameController.i.statemachine.Push(DepartmentState.i);
                    }
                }
                if (GameController.i.statemachine.CurrentState == DepartmentState.i)
                {
                    GameObject gm = result.gameObject;
                    
                    if ((result.gameObject.name == "mayvariables") && (DepartmentState.i.mayormeasures))
                    {
                        GameObject varpanel = GameObject.Find("varpanel");
                        if (varpanel.transform.childCount != 0)
                        {
                            DepartmentState.i.destroymaymeasures();
                        }
                        DepartmentState.i.FillMenumayor(gm);
                    }
                    if ((result.gameObject.name == "maymeasuresbutton") && (DepartmentState.i.mayorvariables))
                    {
                        GameObject varpanel = GameObject.Find("varpanel");
                        if (varpanel.transform.childCount != 0)
                        {
                            DepartmentState.i.destroymayvariables();
                        }
                        DepartmentState.i.FillMenumaymeasures();
                    }
                    if (time > DepartmentState.i.temptime + 0.5)
                    {
                        GameObject Tempobject;
                        if ((measureselected) && (result.gameObject.tag == "measure"))
                        {
                            measureselected = false;
                            GameObject tempTempObject = GameObject.Find(number.ToString());
                            tempTempObject.transform.GetChild(0).gameObject.SetActive(false);
                            Tempobject = default;
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                        if (result.gameObject.tag == "measure")
                        {
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                    }
                    if (result.gameObject.name == "mayaccept" || result.gameObject.name == "mayacceptbutton") 
                        if ((measureselected) || (DepartmentState.i.mayorvariables))
                    {
                        if(DepartmentState.i.mayormeasures)
                        {
                            tempoption = SendMessage(number);
                            GameController.i.GetMessage(tempoption);
                            DepartmentState.i.destroymaymeasures();
                            tempoption = default;
                            number = default;
                            measureselected = false;
                        }
                        if(DepartmentState.i.mayorvariables)
                        {
                            DepartmentState.i.MayorSetValues();
                            DepartmentState.i.destroymayvariables();
                        }
                        DepartmentState.i.MayorsMenu.gameObject.SetActive(false);
                        GameController.i.statemachine.Pop();
                    }
                    if (result.gameObject.name == "depacceptbutton")
                    {
                        DepartmentState.i.Setvalues();
                        DepartmentState.i.destroyvariables();
                        DepartmentState.i.DepartMenu.gameObject.SetActive(false);
                        GameController.i.statemachine.Pop();
                    }
                    if (result.gameObject.name == "depcancelbutton")
                    {
                        DepartmentState.i.destroyvariables();
                        DepartmentState.i.DepartMenu.gameObject.SetActive(false);
                        GameController.i.statemachine.Pop();
                    }
                    if (result.gameObject.name == "maycancelbutton")
                    {
                        GameObject varpanel = GameObject.Find("varpanel");
                        if ((varpanel.transform.childCount != 0) && (DepartmentState.i.mayormeasures))
                        {
                            DepartmentState.i.destroymaymeasures();
                            tempoption = default;
                            measureselected = false;
                        }
                        if ((varpanel.transform.childCount != 0) && (DepartmentState.i.mayorvariables))
                        {                            
                            DepartmentState.i.destroymayvariables();
                        }
                        DepartmentState.i.MayorsMenu.gameObject.SetActive(false);
                        GameController.i.statemachine.Pop();
                    }
                }
                if ((GameController.i.statemachine.CurrentState == DiplomacyStateMain.i) && 
                    (DiplomacyStateMain.i.dipmenu.gameObject.activeSelf == true))
                {
                    GameObject Tempobject;
                    if (time > DiplomacyStateMain.i.temptime + 0.5)
                    {
                        if ((measureselected) && (result.gameObject.tag == "measure"))
                        {
                            measureselected = false;
                            GameObject temTempObject = GameObject.Find(number.ToString());
                            temTempObject.transform.GetChild(0).gameObject.SetActive(false);
                            Tempobject = default;
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                        if (result.gameObject.tag == "measure")
                        {
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                    }
                    if ((result.gameObject.name == "prop") && (measureselected) && (DiplomacyStateMain.i.dipmeasures == true))
                    {
                        tempoption = SendMessage(number);
                        tempoption.measureactive = true;
                        GameController.i.GetMessage(tempoption);
                        DiplomacyStateMain.i.destroymaymeasures();
                        DiplomacyStateMain.i.dipmenu.gameObject.SetActive(false);
                        tempoption = default;
                        number = default;
                        measureselected = false;
                        GameController.i.statemachine.Pop();
                    }
                    if (result.gameObject.name == "cancel")
                    {
                        GameObject gm = result.gameObject;
                        DiplomacyStateMain.i.destroymaymeasures();
                        DiplomacyStateMain.i.dipmenu.gameObject.SetActive(false);
                        tempoption = default;
                        measureselected = false;
                        GameController.i.statemachine.Pop();
                    }
                }
                if ((GameController.i.statemachine.CurrentState == DiplomacyStateMinor.i) &&
                    (DiplomacyStateMinor.i.dipmenu.gameObject.activeSelf == true))
                {
                    if (time > DiplomacyStateMinor.i.temptime + 0.5)
                    {
                        GameObject Tempobject;
                        if ((measureselected) && (result.gameObject.tag == "measure"))
                        {
                            measureselected = false;
                            GameObject tempTempObject = GameObject.Find(number.ToString());
                            tempTempObject.transform.GetChild(0).gameObject.SetActive(false);
                            Tempobject = default;
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                        if (result.gameObject.tag == "measure")
                        {
                            char[] temp = result.gameObject.name.ToCharArray();
                            number = (int)Char.GetNumericValue(temp[0]);
                            messagesent = true;
                            Tempobject = result.gameObject;
                            Tempobject.transform.GetChild(0).gameObject.SetActive(true);
                            measureselected = true;
                        }
                    }
                    if ((result.gameObject.name == "prop") && (measureselected) && (DiplomacyStateMinor.i.dipmeasures == true))
                    {
                        tempoption = SendMessage(number);
                        tempoption.measureactive = true;
                        GameController.i.GetMessage(tempoption);
                        DiplomacyStateMinor.i.destroymaymeasures();
                        DiplomacyStateMinor.i.dipmenu.gameObject.SetActive(false);
                        tempoption = default;
                        number = default;
                        measureselected = false;
                       GameController.i.statemachine.Pop();
                    }
                    if (result.gameObject.name == "cancel")
                    {
                        GameObject gm = result.gameObject;
                        DiplomacyStateMinor.i.CheckButton(gm);
                        DiplomacyStateMinor.i.destroymaymeasures();
                        DiplomacyStateMinor.i.dipmenu.gameObject.SetActive(false);
                        tempoption = default;
                        number = default;
                        measureselected = false;
                       GameController.i.statemachine.Pop();
                    }

                    
                }                    
                GameObject TempCanvas = GameObject.Find("Canvas");
                MissionManager missionManager = TempCanvas.GetComponent<MissionManager>();
                if (missionManager.MissionManagerActive == true)
                {
                    if (result.gameObject.tag == "PossibleResponse")
                    {
                        for (int i = 0; i < missionManager.TempdateEvent.PossibleResponse.Length; i++)
                        {
                            GameObject[] posresp = GameObject.FindGameObjectsWithTag("PossibleResponse");
                            foreach (GameObject posresp2 in posresp)
                            {
                                var tempobj = posresp2.transform.Find("backgroundimage");
                                tempobj.gameObject.SetActive(false);
                            }
                        }
                        var tempgameobject = result.gameObject.transform.GetChild(0);
                        var tempgameobjectimage = tempgameobject.gameObject.GetComponent<Image>();
                        tempgameobjectimage.gameObject.SetActive(true);
                        possibleresponschosen = true;
                    }
                }
            }
        }
    }

    public Option.option SendMessage(int number)
    {
        if (GameController.i.statemachine.CurrentState == DepartmentState.i)
        {
            tempoption = DepartmentState.i.GetmayOption(number);
        }
        if (GameController.i.statemachine.CurrentState == DiplomacyStateMain.i)
        {
            tempoption = DiplomacyStateMain.i.GetDipOption(number);
        }
        if (GameController.i.statemachine.CurrentState == DiplomacyStateMinor.i)
        {
            tempoption = DiplomacyStateMinor.i.GetDipOption(number);
        }
        return tempoption;
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
    }
}

