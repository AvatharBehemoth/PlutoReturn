using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Justice : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _corruption { get; set; }
    public float _municipalcourts { get; set; }
    public float _courts { get; set; }
    public float _prisons { get; set; }

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("corruption");
        list.Add("municipal courts");
        list.Add("courts");
        list.Add("prisons");
        return list;
    }
}
