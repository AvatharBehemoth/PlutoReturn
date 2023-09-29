using UnityEngine;

public class MainfactionDynamics : MonoBehaviour
{
    private GameObject[] MainFaction;
    private GameObject[] MinorFaction;

    // Start is called before the first frame update
    void Start()
    {        
    }

    public void DiplomacyMenu(GameObject Gm)
    {
        GameObject otherfaction = gameObject.GetComponentInChildren<GameObject>();
        otherfaction.name = Gm.name;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
