using stateman;
using UnityEngine;


public class MenuState : state<GameController>
{
    public static MenuState i { get; private set; }
    public RectTransform MenuQuit;
    public GameObject[] menuquit;
    public bool active;
    private void Awake()
    {
        i = this;
    }

    public void Start()
    {
        active = false;
    }

    public void CheckButtonPressed()
    {   
        if (active)
        {
            MenuQuit.gameObject.SetActive(active);
        }
        else
        {
            MenuQuit.gameObject.SetActive(active);
        }
    }

    public bool CheckActive()
    {
        if (active)
        {
            active = false;
            return active;
        }
        else
        {
            active = true;
            return active;
        }
    }    
    
    public void CheckButton(GameObject Gm)
    {
        GameController gamecontroller = GetComponent<GameController>();
        if (Gm.name == "Yes")
        {
            gamecontroller.statemachine.ChangeState(BeginState.i);
            BeginState.i.Init();
        }
        if (Gm.name == "No")
        {
            gamecontroller.statemachine.Pop();
        }
    }

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }
}
