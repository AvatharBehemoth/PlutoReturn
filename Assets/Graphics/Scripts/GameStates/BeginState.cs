using stateman;
using UnityEngine;



public class BeginState : state<GameController>
{
    public static BeginState i { get; private set; }

    public RectTransform Menu;
    GameController gamecontroller;

    private void Awake()
    {
        i = this;
        gamecontroller = GetComponent<GameController>();
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Menu.gameObject.SetActive(true);
        foreach (var _1state in gamecontroller.statemachine.StateStack)
        {
            if (gamecontroller.statemachine.CurrentState != BeginState.i)
            { gamecontroller.statemachine.Pop(); }
        }
    }

    public void Newgame()
    {
        gamecontroller.Init2();
        Menu.gameObject.SetActive(false);
        SelectFactionState.i.buttonpressed = false;
    }

    public void Setmenuactive()
    {
        Menu.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Menu.gameObject.SetActive(false);
        Application.Quit();
        Debug.Log("quit");
    }

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }
}
