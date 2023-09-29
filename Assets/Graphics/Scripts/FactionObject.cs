using UnityEngine;

public class FactionObject : MonoBehaviour
{
    //definition of what variables are in a the main faction object.
    public string _faction;
    public Color _factionColor;
    public string _largest_city;
    public int _population;
    public double _GDP;
    public string _ship_transport;
    public int ship_transport;
    public string _road_transport;
    public int road_transport;
    public string _rail_transport;
    public int rail_transport;
    public string _air_transport;
    public int air_transport;
    public string _crime;
    public int crime;
    public string _corruption;
    public int corruption;

    public bool Chapter_9 = false;
    public bool Fed_takeover = false;
    public bool Econ_vic = false;
    public bool Alliance_everyone = false;
    public bool Mil_victory = false;
    public bool Mil_defeat = false;
    public bool ownfaction = false;

    public void Start()
    {
        _factionColor = GetComponent<SpriteRenderer>().color;
        _faction = GetComponent<MajorFactionInfoDisplay>().Factionname;
        if (_faction == "Cascadia")
        { _largest_city = "Seatle"; _population = 8367519; _GDP = 600000000000;
            ship_transport = 70; road_transport = 50; rail_transport = 25; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Southern California")
        { _largest_city = "Los Angeles"; _population = 23762904; _GDP = 900000000000;
            ship_transport = 80; road_transport = 70; rail_transport = 70; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Northern California")
        { _largest_city = "San Francisco"; _population = 12621707; _GDP = 1212000000000;
            ship_transport = 70; road_transport = 50; rail_transport = 50; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Arizona Sun Corridor")
        { _largest_city = "Phoenix"; _population = 6200000; _GDP = 210000000000;
            ship_transport = 25; road_transport = 50; rail_transport = 25; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Front Range")
        { _largest_city = "Denver"; _population = 5467633; _GDP = 292202000000;
            ship_transport = 25; road_transport = 50; rail_transport = 50; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Texas Triangle")
        { _largest_city = "Dallas"; _population = 20852272; _GDP = 1300000000000;
            ship_transport = 25; road_transport = 50; rail_transport = 50; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Gulf Coast")
        { _largest_city = "Houston"; _population = 64008345; _GDP = 495000000000;
            ship_transport = 80; road_transport = 50; rail_transport = 25; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Florida")
        { _largest_city = "Miami"; _population = 21538187; _GDP = 448000000000;
            ship_transport = 80; road_transport = 50; rail_transport = 50; air_transport = 70;
            crime = 99; corruption = 85;
        }
        if (_faction == "Piedmont Atlantic")
        { _largest_city = "Atlanta"; _population = 19000000; _GDP = 1100000000000;
            ship_transport = 25; road_transport = 50; rail_transport = 50; air_transport = 80;
            crime = 99; corruption = 85;
        }
        if (_faction == "Northeast")
        { _largest_city = "New York"; _population = 52332123; _GDP = 4540000000000;
            ship_transport = 90; road_transport = 50; rail_transport = 50; air_transport = 80;
            crime = 99; corruption = 85;
        }
        if (_faction == "Great Lakes")
        { _largest_city = "Chicago"; _population = 85011531; _GDP = 6000000000000;
            ship_transport = 80; road_transport = 50; rail_transport = 50; air_transport = 80;
            crime = 99; corruption = 85;
        }
        // murder rate chicago 15.65 per 100,000 / us 7.8 per 100.000 / netherlands 0.7 per 100.000
    }
}


