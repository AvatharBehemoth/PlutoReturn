using System.Linq;
using UnityEngine;

public class DiplomacyObject : MonoBehaviour
{
    public struct Relationstate
    {
        public Relationstate(string factionName, int relationnumber)
        {
            FactionName = factionName;
            RelationNumber = relationnumber;
        }
        public string FactionName { get; set; }
        public int RelationNumber { get; set; }
    }

    public GameObject[] MainFactions;
    public GameObject[] MinorFactions;
    
    public void Awake()
    {
        MainFactions = GameObject.FindGameObjectsWithTag("MainFaction");
        MinorFactions = GameObject.FindGameObjectsWithTag("MinorFaction");
    }

    public Relationstate[] relationstates;
    public GameObject[] Faction;
    // Start is called before the first frame update
    void Start()
    {
        Faction = MainFactions.Concat(MinorFactions).ToArray();

        relationstates = new Relationstate[Faction.Length];
        for (int i = 0; i < relationstates.Length; i++)
        {
            relationstates[i] = new Relationstate(Faction[i].name, 25);
        }
    }
    public void LoveYourself() 
    {
        for (int i = 0; i < relationstates.Length; i++)
        {

            if (relationstates[i].FactionName == SelectFactionState.i.ownFactionName)
            {
                relationstates[i].RelationNumber = 100;
            }
        }
    }
}
