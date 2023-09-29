
public class DateEvents
{
    public struct DateEvent
    {
        public DateEvent
            (int nr, float year, float month, float day, string typeevent, string eventtext,
            string[] possibleresponse, bool win, bool lose)
        {
            Nr = nr;
            Year = year;
            Month = month;
            Day = day;
            TypeEvent = typeevent;
            EventText = eventtext;
            PossibleResponse = possibleresponse;
            Win = win;
            Lose = lose;
        }
        public int Nr;
        public float Year;
        public float Month;
        public float Day;
        public string TypeEvent;
        public string EventText;
        public string[] PossibleResponse;
        public bool Win;
        public bool Lose;
    }
}
