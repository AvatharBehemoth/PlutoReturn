using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public struct option
    {
        public option(string OptionText, int Integergm, string Effectcat,string Effectarea, float Effectmod, string TypeMeasure,
            bool Measureactive, int Tier)
        {
            optiontext = OptionText;
            integergm = Integergm;
            effectcat = Effectcat;
            effectarea = Effectarea;
            effectmod = Effectmod;
            typemeasure = TypeMeasure;
            measureactive = Measureactive;
            tier = Tier;
        }
        public string optiontext { get; set; }
        public int integergm { get; set; }
        public string effectcat { get; set; }
        public string effectarea { get; set; }
        public float effectmod { get; set; }
        public string typemeasure { get; set; }
        public bool measureactive { get; set; }
        public int tier { get; set; }
    }
}
