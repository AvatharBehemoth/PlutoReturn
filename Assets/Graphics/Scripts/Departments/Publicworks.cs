using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publicworks : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _corruption { get; set; }
    public float _maintenance_streets { get; set; }
    public float _maintenance_sewers { get; set; }

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("corruption");
        list.Add("maintenance streets");
        list.Add("maintenance sewers");
        return list;
    }
}
