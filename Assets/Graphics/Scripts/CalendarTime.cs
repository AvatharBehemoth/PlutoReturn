using TMPro;
using UnityEngine;

public class CalendarTime : MonoBehaviour
{
    public Rect calendar;
    public TextMeshProUGUI day;
    public TextMeshProUGUI month;
    public TextMeshProUGUI year;
    public bool Dayincreased = false;
    public bool Timeincreased = false;
    
    public int _yearIndex { get { return yearIndex;  }  set { yearIndex = _yearIndex ; } }
    public int _monthIndex { get { return monthIndex; } set { monthIndex = _monthIndex; } }
    public int _dayIndex { get { return dayIndex; } set { dayIndex = _dayIndex; } }
    public float gameTime { get; set; }
    public float gt { get; set; }
    public int yearIndex = 2019;
    public int monthIndex = 1;
    public int dayIndex = 1;
    public int tempday = 0;
    // Update is called once per frame
    void Update()
    {
        if (GameController.i.statemachine.CurrentState == MapState.i)
        {
            GameObject TempCanvas = GameObject.Find("Canvas");
            MissionManager missionManager = TempCanvas.GetComponent<MissionManager>();
            tempday = dayIndex;
            Dayincreased = false;
            if (!missionManager.MissionManagerActive)
            {
                gt += Time.deltaTime;
                if (gt > 1)
                {
                    dayIndex += 1;
                    gt = 0;
                    
                }
                if (dayIndex > 28)
                {
                    if (monthIndex == 2) { monthIndex++; dayIndex = 1; Timeincreased = true; }
                }
                if (dayIndex > 30)
                {
                    if (monthIndex == 4 || monthIndex == 6 || monthIndex == 9 || monthIndex == 11) { monthIndex++; dayIndex = 1; Timeincreased = true; }
                }
                if (dayIndex > 31)
                {
                    if (monthIndex == 1 || monthIndex == 3 || monthIndex == 5 || monthIndex == 7 || monthIndex == 8 || monthIndex == 10 || monthIndex == 12)
                    { monthIndex++; dayIndex = 1; Timeincreased = true; }
                }
                if (monthIndex > 12)
                { yearIndex += 1; monthIndex = 1; }
            }
        }
        if (tempday != dayIndex)
        {
            Dayincreased = true;
        }
        string padding = " ";
        day.text = dayIndex.ToString() + padding;
        month.text = monthIndex.ToString() + padding;
        year.text = yearIndex.ToString() + padding;
    }
}
