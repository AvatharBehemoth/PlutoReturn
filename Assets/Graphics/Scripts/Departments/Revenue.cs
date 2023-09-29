using System.Collections.Generic;
using UnityEngine;

public class Revenue : MonoBehaviour
{
    public float _budget { get; set; }
    public float _personnel { get; set; }
    public float _efficiency { get; set; }
    public float _corruption { get; set; }
    public float _taxes_collected { get; set; }
    public float _general_fund { get; set; }
    public float _public_purse { get; set; }
    public float _tax_level { get; set; }

    private void Awake()
    {
        _tax_level = 0.005f;
        _taxes_collected = 0;
        _public_purse = 0;
    }

    public List<string> list;
    public List<string> GetList()
    {
        list = new List<string>();
        list.Add("budget");
        list.Add("personnel");
        list.Add("efficiency");
        list.Add("corruption");
        list.Add("taxes collected");
        list.Add("tax level");
        return list;
    }
}
