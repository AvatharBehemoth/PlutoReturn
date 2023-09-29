using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour
{

    public bool dooncemission = false;
    public List<DateEvents.DateEvent> dateEvents;
    public DateEvents.DateEvent TempdateEvent;
    public bool MissionManagerActive = false;
    private void Start()
    {
        dooncemission = true;
    }
    public void GetDate(float Year, float Month, float Day)
    {
        dateEvents = new List<DateEvents.DateEvent>();
        dateEvents = GetList();
        //several trigger dates
        {
            foreach (DateEvents.DateEvent dateEvent in dateEvents)
            {
                if (dateEvent.Year == Year && dateEvent.Month == Month && dateEvent.Day == Day)
                {
                    TempdateEvent = dateEvent;
                    OpenPanel(dateEvent);
                    break;
                }
            }
        }
    }
    public void OpenPanel(DateEvents.DateEvent dateEvent)
    {
        if (dooncemission)
        {
            float yposition = 0;
            MissionManagerActive = true;
            dooncemission = false;
            GameObject tempCanvas = GameObject.Find("Canvas");
            GraphicCanvasRaycast graphraycast = tempCanvas.GetComponent<GraphicCanvasRaycast>();
            graphraycast.possibleresponschosen = false;
            GameObject panel = new GameObject("panel");
            panel.AddComponent<CanvasRenderer>();
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(1f, 1f, 1f, .65f);
            panel.transform.SetParent(tempCanvas.transform, false);

            var panelrect = panel.GetComponent<RectTransform>();
            panelrect.anchorMin = new Vector2(0, 1);
            panelrect.anchorMax = new Vector2(0, 1);
            panelrect.pivot = new Vector2(0, 1);
            panelrect.localPosition = new Vector2(-(Screen.width * 0.8f) / 2, (Screen.height * 0.725f) * 0.5f);
            panelrect.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);

            GameObject panelHeader = new GameObject();
            panelHeader.transform.SetParent(panel.transform);
            var panelHeaderrect = panelHeader.AddComponent<RectTransform>();
            panelHeaderrect.anchorMin = new Vector2(0, 1);
            panelHeaderrect.anchorMax = new Vector2(0, 1);
            panelHeaderrect.pivot = new Vector2(0, 1);
            panelHeaderrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, -panelrect.sizeDelta.y / 20);
            panelHeaderrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, panelrect.sizeDelta.y / 20);
            var panelHeadertexBox = panelHeader.AddComponent<TextMeshProUGUI>();
            panelHeadertexBox.text = dateEvent.TypeEvent;
            panelHeadertexBox.color = Color.black;
            panelHeadertexBox.alignment = TextAlignmentOptions.Center;

            if (dateEvent.TypeEvent == "event")
            {
                GameObject panelEventtext = new GameObject();
                panelEventtext.transform.SetParent(panel.transform);
                var panelEventtextrect = panelEventtext.AddComponent<RectTransform>();
                panelEventtextrect.anchorMin = new Vector2(0, 1);
                panelEventtextrect.anchorMax = new Vector2(0, 1);
                panelEventtextrect.pivot = new Vector2(0, 1);
                var panelEventtextBox = panelEventtext.AddComponent<TextMeshProUGUI>();
                panelEventtextBox.text = dateEvent.EventText;
                panelEventtextBox.fontSize = 21;
                panelEventtextBox.color = Color.black;

                float paneleventtextheight = 60;
                if (panelEventtextBox.preferredHeight <= 150)
                {
                    paneleventtextheight = 60;
                }
                else
                {
                    paneleventtextheight = 120;
                }
                yposition = panelHeaderrect.localPosition.y - panelHeaderrect.sizeDelta.y - 60;
                panelEventtextrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, paneleventtextheight);
                panelEventtextrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, yposition);
                yposition -= paneleventtextheight;
            }
            if (dateEvent.TypeEvent == "decision")
            {
                GameObject panelEventtext = new GameObject();
                panelEventtext.transform.SetParent(panel.transform);
                var panelEventtextrect = panelEventtext.AddComponent<RectTransform>();
                panelEventtextrect.anchorMin = new Vector2(0, 1);
                panelEventtextrect.anchorMax = new Vector2(0, 1);
                panelEventtextrect.pivot = new Vector2(0, 1);
                var panelEventtextBox = panelEventtext.AddComponent<TextMeshProUGUI>();
                panelEventtextBox.text = dateEvent.EventText;
                panelEventtextBox.fontSize = 21;
                panelEventtextBox.color = Color.black;
                float paneleventtextheight = 60;
                if (panelEventtextBox.preferredHeight <= 150)
                {
                    paneleventtextheight = 60;
                }
                else
                {
                    paneleventtextheight = 120;
                }
                yposition = panelHeaderrect.localPosition.y - panelHeaderrect.sizeDelta.y - 60;
                panelEventtextrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, paneleventtextheight);
                panelEventtextrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, yposition);
                yposition -= paneleventtextheight + 60;

                for (int i = 0; i < dateEvent.PossibleResponse.Length; i++)
                {
                    GameObject panelSelecttext = new GameObject();
                    panelSelecttext.tag = "PossibleResponse";
                    panelSelecttext.name = i.ToString();
                    panelSelecttext.transform.SetParent(panel.transform);
                    var panelSelecttextrect = panelSelecttext.AddComponent<RectTransform>();
                    panelSelecttextrect.anchorMin = new Vector2(0, 1);
                    panelSelecttextrect.anchorMax = new Vector2(0, 1);
                    panelSelecttextrect.pivot = new Vector2(0, 1);

                    var panelSelecttextBox = panelSelecttext.AddComponent<TextMeshProUGUI>();
                    panelSelecttextBox.text = dateEvent.PossibleResponse[i];

                    panelSelecttextBox.fontSize = 21;
                    panelSelecttextBox.color = Color.black;

                    paneleventtextheight = 60;
                    if (panelSelecttextBox.preferredHeight <= 150)
                    {
                        paneleventtextheight = 60;
                    }
                    else
                    {
                        paneleventtextheight = 120;
                    }
                    panelSelecttextrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, paneleventtextheight);
                    panelSelecttextrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, yposition);
                    GameObject backgroundimage = new GameObject();
                    backgroundimage.name = "backgroundimage";
                    backgroundimage.transform.SetParent(panelSelecttext.transform);
                    RectTransform bgi = backgroundimage.AddComponent<RectTransform>();
                    Image bgimage = backgroundimage.AddComponent<Image>();
                    bgi.anchorMin = new Vector2(0, 1);
                    bgi.anchorMax = new Vector2(0, 1);
                    bgi.pivot = new Vector2(0, 1);
                    bgi.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, paneleventtextheight);
                    bgi.transform.localPosition = new Vector2(0, 0);
                    Color quarter = new Color(1f, 1f, 1f, .5f);
                    bgimage.color = quarter;
                    backgroundimage.SetActive(false);
                    yposition -= paneleventtextheight;
                }
            }
            //created custom button from script via
            //https://forum.unity.com/threads/ui-button-create-by-script-c.285829/#post-9149029
            DefaultControls.Resources uiResources = new DefaultControls.Resources();
            GameObject panelFooter = DefaultControls.CreateButton(uiResources);
            panelFooter.transform.SetParent(panel.transform);
            var panelFooterrect = panelFooter.GetComponent<RectTransform>();
            panelFooterrect.anchorMin = new Vector2(0, 1);
            panelFooterrect.anchorMax = new Vector2(0, 1);
            panelFooterrect.pivot = new Vector2(0, 1);
            panelFooterrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, -(panelrect.sizeDelta.y / 20) * 18);
            panelFooterrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, panelrect.sizeDelta.y / 20);


            Text panelFootertexBox = panelFooter.GetComponentInChildren<Text>();
            panelFootertexBox.text = "Ok";
            panelFootertexBox.color = Color.black;
            Button panelFooterClick = panelFooter.GetComponent<Button>();
            panelFooterClick.onClick.AddListener(() => Onclick());
        }
    }

    public void Onclick()
    {
        Canvas TempCanvas = GetComponent<Canvas>();
        GraphicCanvasRaycast graphicCanvas = TempCanvas.GetComponent<GraphicCanvasRaycast>();
        if (graphicCanvas.possibleresponschosen && TempdateEvent.TypeEvent == "decision")
        {
            int eventnumber = TempdateEvent.Nr;
            int choicenumber = default;

                GameObject[] posresp = GameObject.FindGameObjectsWithTag("PossibleResponse");
                foreach (GameObject posresp2 in posresp)
                {
                    var tempobj = posresp2.transform.Find("backgroundimage");
                    if (tempobj.gameObject.activeSelf)
                    {
                        int.TryParse(posresp2.name, out choicenumber);
                    }
                }
            CheckEffect(eventnumber, choicenumber);
            GameObject destroy = GameObject.Find("panel");
            Destroy(destroy);
            MissionManagerActive = false;
        }
        if (TempdateEvent.TypeEvent == "event")
        {
            int eventnumber = TempdateEvent.Nr;
            int choicenumber = 0;
            CheckEffect(eventnumber, choicenumber);
            GameObject destroy = GameObject.Find("panel");
            Destroy(destroy);
            MissionManagerActive = false;
        }
    }

    public List<DateEvents.DateEvent> GetList()
    {
        dateEvents = new List<DateEvents.DateEvent>();
        string[] arr = new string[] { };
        dateEvents.Add(new DateEvents.DateEvent(0, 2019, 1, 1, "event",
            "Congratulations on your election, or whatever you pulled. Good job!" +
            "Are you ready for the challenges ahead?", arr, false, false));
        string[] arr2 = new string[] {"We do nothing, we trust the science",
        "Yes potential problem, we should rely on the federal government, for the time being",
        "We can take direct action: creating a city currency based on the price of 1 gram of gold, " +
        "lets pretend its for tourists"};
        dateEvents.Add(new DateEvents.DateEvent(1, 2019, 1, 2, "decision", "Sir, were confronted with a possible problem." +
            "Sonce 2008 the fed has been printing unlimited money, and lending it at 0% interest to cronies." +
            "The repercussions for the dollar is that it will reduce its value to near 0 or 0." +
            "How do you want to confront this potential problem?", arr2, false, false));
        dateEvents.Add(new DateEvents.DateEvent(2, 2019, 1, 4, "event", "The democrats take control of the house. " +
            "With a promise to end the government shutdown, but without funding for the border wall.This will " +
            "become the longest government shutdown, ending january 25th.", arr, false, false));
        arr2 = new string[] { "The federal government and WHO know best",
            "Right now, we wait. We will react if and when a crisis happens!",
            "We pass laws protecting the people against medical and economic tyranny"};
        dateEvents.Add(new DateEvents.DateEvent(3, 2019, 1, 10, "decision", "Sir, we have news regarding an exercise called:" +
            "Crimson Contagion. Including lockdowns, forcing masks, and media theatrics. How should we respond in such a crisis?",
            arr2, false, false));
        dateEvents.Add(new DateEvents.DateEvent(4, 2019, 1, 25, "event", "Flights are halted in New York La Guardia due to a shortage" +
            "of airport staff as a result of the ongoing government shutdown.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(5, 2019, 1, 28, "event", "Justice Department charges Chinese tech firm" +
            "Huawei with multiple counts of fraud, raising U.S. China tensions.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(6, 2019, 2, 1, "event", "The president confirms that the U.S. will leave the intermediate-range Nuclear Forces treaty", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(7, 2019, 2, 15, "event", "The president declares a national emergency to free up funds for his proposed border wall.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(8, 2019, 2, 21, "event", "Jussie Smolet hate crime hoax, exposing the terrible mental health state of the country.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(9, 2019, 2, 23, "event", "Singer R. Kelly charged with 10 counts of aggravated crimnal sexual abuse against minors", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(10, 2019, 2, 27, "event", "Second North Korea U.S. summit was cut short.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(11, 2019, 3, 12, "event", "The College Admission bribery scandal broke. Showing bribery and fraud to secure admission to elite colleges." +
            "Exposing the moral compass of the country.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(12, 2019, 3, 16, "event", "Boeing grounds the entire fleet of 737 max airplanes because they keep crashing nose first.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(13, 2019, 3, 30, "event", "The president issues a permit for the construction of the Keystone Pipeline.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(14, 2019, 4, 1, "event", "The U.S. halts the delivery of F-35 fighter jet-related equipment to Turkey " +
            "to protest the country's planned purchasing of Russia's S-400 missile defense system. The Turks had alreadu payed.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(15, 2019, 4, 3, "event", "An explosion at a chemical plant in Crosby, Texas leaves one dead and two injured. " +
            "This comes just weeks after a similar Houston-area explosion in Deer Creek, Texas on March 17th.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(16, 2019, 4, 5, "event", "The 1973 War Powers Resolution is invoked for the first time " +
            "when the House votes 247–175 to end U.S. military assistance to Saudi Arabia in its intervention in the Yemeni Civil War; " +
            "the Senate voted 54–46 on the bill in March 2019. President Trump vetoes the bill on April 16, the second veto of his presidency.", arr, false, false));
        dateEvents.Add(new DateEvents.DateEvent(17, 2019, 4, 8, "event", "Joining Saudi Arabia and Bahrain, " +
            "the Trump administration announces its intentions to designate Iran's Islamic Revolutionary Guard Corps as a terrorist group. " +
            "The official designation takes place on April 15.", arr, false, false));
        arr2 = new string[] { "Well, we'll comply with the federal government", "We know their battle plan, we will counteract it" };
        dateEvents.Add(new DateEvents.DateEvent(18, 2019, 10, 18, "decision", "Sir, we have news about a second pandemic exercise called" +
            "Event201. This was a second simulation of an upper respiratory disease. It looks like they want to hit us this winter!", arr2, false, false));
        arr2 = new string[] { "Mobilise police forces and have the national guard stand by, we will not tolerate looting",
            "As soon as riots start, declare Martial Law, have looters shot on sight", 
            "If riots start, have conferences with community leaders, local business owners and citizens. Only use force if strictly necessary. " +
            "Dont tolerate rioting or looting, but do assist demonstrations.",
            "Let everything run it's course, We'll have some developers clear the minority shop owners out, and some megacorporations move in."};
        dateEvents.Add(new DateEvents.DateEvent(19, 2020, 3, 25, "decision",  "Some guy died. There is video of a policeman sitting on his neck/shoulders." +
            "This is against Standard Operating Procedure (SOP). Turns out the guy had a pootentially lethal dose of both Fentanyl and Methamphetamine in his system." +
            "Medical attention was not given. Most likely not following procedure AND withholding medical attention is what killed him!" +
            "Already media is going on a feeding frenzy. We are afraid of mass rioting", arr2, false, false));
        return dateEvents;
    }

    public void CheckEffect(int eventnumber, int choicenumber)
    {
        GameObject TempCanvas = GameObject.Find("Canvas");
        FactionObject factionbject = GameController.i.ChosenFaction.GetComponent<FactionObject>();
        Police police = TempCanvas.GetComponent<Police>();
        if (eventnumber == 1)
        {
            MissionManagerActive = true;
            if (choicenumber == 0)
            {
                GameController.i.gameloss = true;
                string losstext = "The Dollar will lose value, in an inflation cascade. " +
                "That will destroy its status as the reserve currency, making it even less valuable. " +
                "This leads to a collapse of the state and its institutions worse than what happened to the Soviet Union.";
                GameController.i.WinLose(losstext);
            }
            if (choicenumber == 1)
            { GameController.i.currencydevaluations += 0.01f; }
            if (choicenumber == 2)
            {
                GameController.i._citycurrency = true;
                GameController.i.citycurrency = 55;
                GameController.i.currencydevaluations = 0;
            }
        }

        if (eventnumber == 2)
        {
            MissionManagerActive = true;
            GameController.i.fedbasenumber -= 50;
        }

        if (eventnumber == 3)
        {
            MissionManagerActive = true;
            if (choicenumber == 0)
            {                
                GameController.i.gameloss = true;
                string losstext = "You have potentially helped overthrow the democratic order by suspending the rule of law" +
                "regarding fundamental rights of the people.";
                GameController.i.WinLose(losstext);
            }
            if (choicenumber == 1)
            {
                GameController.i.fedbasenumber += 100;
            }
            if (choicenumber == 2)
            {
                factionbject._GDP *= 1.2f;
            }
        }

        if (eventnumber == 4)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 5)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 50;
            }
        }

        if (eventnumber == 6)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 50;
            }
        }

        if (eventnumber == 7)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 8)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 9)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 10)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 50;
            }
        }

        if (eventnumber == 11)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }
        if (eventnumber == 12)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 50;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 13)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 50;
            }
        }

        if (eventnumber == 14)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 100;
            }
        }

        if (eventnumber == 15)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.fedbasenumber -= 75;
            }
        }

        if (eventnumber == 16)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
                GameController.i.foreignfailure += 50;
            }
        }

        if (eventnumber == 17)
        {
            if (choicenumber == 0)
            {
                MissionManagerActive = true;
            GameController.i.foreignfailure += 25;}
        }

        if (eventnumber == 18)
        {
            MissionManagerActive = true;
            if (choicenumber == 0)
            {                
                GameController.i.gameloss = true;
                string losstext = "You have potentially helped overthrow the democratic order by suspending the rule of law" +
                    "regarding fundamental rights of the people.";
                GameController.i.WinLose(losstext);
            }
            if (choicenumber == 1)
            {
                GameController.i.ownpower += 50;
            }
        }

        if (eventnumber == 19)
        {
            MissionManagerActive = true;
            if (choicenumber == 0)
            {
                GameController.i.ownpower += 25;
            }
            if (choicenumber == 1)
            {
                GameController.i.ownpower += 10;
                GameController.i.fedbasenumber -= 50;
            }
            if (choicenumber == 2) 
            {
                GameController.i.ownpower += 50;
                GameController.i.fedbasenumber -= 50;
            }
            if (choicenumber == 3)
            {
                GameController.i.ownpower -= 200; 
                GameController.i.fedbasenumber += 200;
            }
        }
    }   
}
