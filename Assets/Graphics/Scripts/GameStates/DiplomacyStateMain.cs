using UnityEngine;
using TMPro;
using stateman;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DiplomacyStateMain : state<GameController>
{

    public static DiplomacyStateMain i { get; private set; }
    public RectTransform dipmenu;
    public List<Option.option> DiplomatiOptions;
    public bool dipmeasures = false;
    public float temptime;

    private void Awake()
    {
        i = this;
    }

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }

    public void CheckObject(GameObject Gm)
    {
        temptime = Time.time;

        dipmenu.gameObject.SetActive(true);
        GameObject OwnFactionText = GameObject.Find("OwnFactionInfo");
        GameObject OwnPlaceholder = GameObject.Find("OwnPlaceholder");
        GameController gamecontroller = GetComponent<GameController>();
        FactionObject ownfobj = gamecontroller.ChosenFaction.GetComponent<FactionObject>();
        OwnPlaceholder.GetComponent<Image>().color = ownfobj._factionColor;
        OwnFactionText.GetComponent<TextMeshProUGUI>().text = gamecontroller.ChosenFaction.name;

        GameObject OtherPlaceholder = GameObject.Find("OtherPlaceholder");
        GameObject OtherFactionText = GameObject.Find("OtherFactionInfo");
        FactionObject otherfobj = Gm.GetComponent<FactionObject>();
        OtherPlaceholder.GetComponent<Image>().color = otherfobj._factionColor;
        OtherFactionText.GetComponent<TextMeshProUGUI>().text = Gm.name;
        GameObject FactionInt = GameObject.Find("inttext");
        var DiplomacyObject = GetComponent<DiplomacyObject>();

        for (int i = 0; i < DiplomacyObject.relationstates.Length; i++)
        {
            if (DiplomacyObject.relationstates[i].FactionName == Gm.name)
            {
                FactionInt.GetComponent<TextMeshProUGUI>().text = DiplomacyObject.relationstates[i].RelationNumber.ToString();
                List<Option.option> TempList = SetDiploOptions(Gm);
                GenerateListgameobjects(TempList, Gm);
                break;
            }
        }
    }

    public List<Option.option> SetDiploOptions(GameObject Gm)
    {
        GameController gamecontroller = GetComponent<GameController>();
        DiplomatiOptions = new List<Option.option>();
        if (Gm.name == gamecontroller.ChosenFaction.name)
        {
            DiplomatiOptions.Add(new Option.option("Reorganise departments for optimal procedures and efficiency.",
                1, "self", "efficiency", 1, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "organise a convention of departments of all the cities. To increase synergy",
                2, "self", "efficiency", 1, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Form a federation of municipalities.", 3, "self", "efficiency", 1, "treaty", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Battle drug abuse, by creating treatment centers.", 4, "crime", "police", 4, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Battle drug abuse, by more repression.", 5, "crime", "police", -4, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Create an anti corruption taskforce reporting to Revenue.", 6, "Revenue", "corruption", 10, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Create an anti corruption taskforce reporting to the Mayor.", 7, "self", "corruption", -10, "measure", false, 1));
        }
        
        else if (Gm.name != gamecontroller.ChosenFaction.name)
        {
            //max 8 options to be generated with current dipmenu panel size.
            DiplomatiOptions.Add(new Option.option(
                "create trade regulation minimizing sales tax", 1, "parties", "revenue", .35f, "treaty", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Organise a convention for civil servants to improve relations", 2, "parties", "efficiency", 5, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "Battle drug abuse, by creating interregional task forces", 3, "parties", "police", 10, "measure", false, 1));
            DiplomatiOptions.Add(new Option.option(
                "organise a convention of departments of the megaregions. To increase synergy", 4,"parties", "efficiency", 1, "measure", false, 1));
        }
        return DiplomatiOptions;
    }

    public void GenerateListgameobjects(List<Option.option> DiplomatOptions, GameObject Gm)
    {
        GameObject panel = GameObject.Find("panel");

        //max 8 options to be generated with current dipmenu panel size.
        var i = 0;
        var j = 0;
        foreach (Option.option option in DiplomatiOptions)
        {
            GameObject prop = new GameObject();
            prop.transform.SetParent(panel.transform);
            var proptransform = prop.AddComponent<RectTransform>();
            proptransform.anchorMin = new Vector2(0,1);
            proptransform.anchorMax = new Vector2(0,1);
            proptransform.pivot = new Vector2(0,1);
                       
            prop.name = option.integergm.ToString();
            prop.tag = "measure";
            prop.transform.localPosition = new Vector2(0, j);
            
            TextMeshProUGUI proptext = prop.AddComponent<TextMeshProUGUI>();
            proptext.text = option.optiontext;
            Vector2 Size = proptext.GetPreferredValues(proptext.text);
            
            if (Size.x > 650)
            {
                i = 60;
            }
            else if (Size.x <= 650)
            {
                i = 30;
            }
            var propy = proptransform.sizeDelta.y;
            var propx = proptransform.sizeDelta.x;
            propy = i;
            propx = 480;
            proptransform.sizeDelta = new Vector2(propx, propy);
            proptext.fontSize = 25;
            GameObject backgroundimage = new GameObject();
            backgroundimage.name = "backgroundimage";
            backgroundimage.transform.SetParent(prop.transform);
            RectTransform bgi = backgroundimage.AddComponent<RectTransform>();
            Image bgimage = backgroundimage.AddComponent<Image>();
            bgi.anchorMin = new Vector2(0, 1);
            bgi.anchorMax = new Vector2(0, 1);
            bgi.pivot = new Vector2(0, 1);
            bgi.sizeDelta = new Vector2(propx, propy);
            bgi.transform.localPosition = new Vector2(0, 0);
            Color quarter = new Color(1f, 1f, 1f, .25f);
            bgimage.color = quarter;
            j -= i +2;
            backgroundimage.SetActive(false);
        }
        dipmeasures = true;
    }

    public Option.option GetDipOption(int number)
    {
        GameController gameController = GetComponent<GameController>();
        Option.option tempoption = DiplomatiOptions[number - 1];
        gameController.sendmessage = true;
        return tempoption;
    }

    public void destroymaymeasures()
    {
        foreach (Option.option option in DiplomatiOptions)
        {
            GameObject destroy = GameObject.Find(option.integergm.ToString());
            Destroy(destroy.gameObject);
        }
        DiplomatiOptions = null;
        dipmeasures = false;
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
    }
}