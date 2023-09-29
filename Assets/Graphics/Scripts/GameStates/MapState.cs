using stateman;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : state<GameController>
{
    public static MapState i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }
}