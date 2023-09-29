using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _corruption { get; set; }
    public float _equipment{ get; set; }    

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("corruption");
        list.Add("equipment");
        return list;
    }
}