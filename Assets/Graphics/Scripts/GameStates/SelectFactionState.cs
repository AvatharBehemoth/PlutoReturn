using System.Collections;
using UnityEngine;
using TMPro;
using stateman;
using UnityEngine.UI;


public class SelectFactionState : state<GameController>
{
    public TextMeshProUGUI ChooseYourFaction;
    public TextMeshProUGUI selected;

    public bool buttonpressed = false;

    public static SelectFactionState i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    public bool factionselected;
    public bool ownfaction;
    public string ownFactionName;
    public GameObject[] MainFactions;
    public GameObject[] departments;
    public Transform MainFaction;
    public Transform MinorFaction;

    public void Start()
    {
        Init();
    }


    public void Init()
    {
        ChooseYourFaction.gameObject.SetActive(true);
        selected.gameObject.SetActive(false);
    }

    public void CastRay(GameObject Gm)
    {
            factionselected = true;
            ownfaction = true;
            ownFactionName = Gm.name;
            FactionObject fobj = Gm.GetComponent<FactionObject>();
            fobj.ownfaction = true;
            ChooseYourFaction.gameObject.SetActive(false);
            for(int i = 0; i < departments.Length; i++)
            {
                departments[i].GetComponentInChildren<Image>().color = fobj._factionColor ;
            }
            StartCoroutine(Delaytime());
    }

    public IEnumerator Delaytime()
    {
        selected.gameObject.SetActive(true);
        selected.text = "You have chosen: " + ownFactionName;
        yield return new WaitForSeconds(5);
        selected.gameObject.SetActive(false);
    }    

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }
}
