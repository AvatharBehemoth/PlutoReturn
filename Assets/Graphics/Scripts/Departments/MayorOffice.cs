using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorOffice : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _tech_level { get; set; }
    public float _corruption { get; set; }
    public float _quality_department_heads { get; set; }

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("tech level");
        list.Add("corruption");
        list.Add("quality department heads");
        return list;
    }
}
