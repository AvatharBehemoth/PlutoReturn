using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopulationIndex : MonoBehaviour
{
    public Rect Population;
    public TextMeshProUGUI pop;
    public Sprite updown;

    public float _population { get; set; }
    public void Update()
    {

        if (GameController.i.ChosenFaction != null)
        {        
            FactionObject factionObject = GameController.i.ChosenFaction.GetComponent<FactionObject>();
            _population = factionObject._population / 1000000;
            var populationnumber = pop.GetComponent<TextMeshProUGUI>();
            populationnumber.text = _population.ToString() + " M";
        }
    }

}
