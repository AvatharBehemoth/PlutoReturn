using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _elementary_education { get; set; }
    public float _middle_education { get; set; }
    public float _higher_education { get; set; }
    public float _illiteracy_percent { get; set; }

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("elementary education");
        list.Add("middle education");
        list.Add("higher education");
        list.Add("illiteracy percent");
        return list;
    }
}
