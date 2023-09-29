using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public Rect econ;
    public TextMeshProUGUI econtext;
    public TextMeshProUGUI budgettext;
    public static Image econarrow;
    public double gdp {get ; set; }
    public double budget { get; set; }
    public float budgetperday;
    public bool doonce = false;
    private void Awake()
    {
        gdp = 0;
        budget = 0;
    }

    private void Update()
    {
        CalendarTime calendarTime = GetComponent<CalendarTime>();
        Revenue revenue = GetComponent<Revenue>();
        if (GameController.i.ChosenFaction != null)
        {
            FactionObject chosenfactionobject = GameController.i.ChosenFaction.GetComponent<FactionObject>();

            if ((calendarTime._yearIndex == 2019) && (calendarTime._monthIndex == 1) && (calendarTime._dayIndex == 1))
            {
                //get income for the remaining year
                revenue._general_fund = (float)(chosenfactionobject._GDP * 0.01);
                revenue._taxes_collected = (float)(chosenfactionobject._GDP * revenue._tax_level);
                revenue._public_purse = (float)(revenue._taxes_collected * 0.25) + revenue._general_fund;
                string formattaxescollected = (revenue._public_purse / 1000000).ToString("F0") + " M";
                budgettext.text = formattaxescollected;
                econtext.text = (chosenfactionobject._GDP / 1000000).ToString("F0") + " M";
            }
            if ((calendarTime._monthIndex == 4) && (calendarTime._dayIndex == 1))
            {
                if (doonce)
                {
                    doonce = false;
                    //get new income for the year
                    revenue._taxes_collected = (float)(chosenfactionobject._GDP * revenue._tax_level);
                    revenue._public_purse += revenue._taxes_collected;
                    string formattaxescollected = (revenue._public_purse / 1000000).ToString("F0");
                    budgettext.text = formattaxescollected;
                    econtext.text = (chosenfactionobject._GDP / 1000000).ToString("F0") + " M";
                }
            }
            else
            { 
                if ((calendarTime._monthIndex == 3) &&(calendarTime._dayIndex == 31)) { doonce = true; }
                string formattaxescollected = (revenue._public_purse / 1000000).ToString("F0") + " M";
                econtext.text = (chosenfactionobject._GDP / 1000000).ToString("F0") + " M";
                budgettext.text = formattaxescollected;
            }
        }
    }
}
