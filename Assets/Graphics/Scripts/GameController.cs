using stateman;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 
 Map asset is an amalgam of:
Map of emerging USMegaregions
Map by Free Vector Maps
http://freevectormaps.com
Since the map asset is a composite asset of multipe works including my own, credit is not needed, but still provided here.
 */
public class GameController : MonoBehaviour
{
    public static GameController i;
    public statemachine<GameController> statemachine { get; private set; }
    public RaycastHit2D hit;
    public GameObject[] obj;
    public TextMeshProUGUI fedgovpowint;
    public TextMeshProUGUI ownpowint;
    public TextMeshProUGUI currint;
    Color temp;
    public GameObject ChosenFaction { get; private set; }
    public bool sendmessage = false;
    public bool processparams = false;
    public bool unifiedmegaregion = false;
    public bool start = false;
    public float economicgrowthfactor = 0.02f;
    public float currencydevaluations = 0.02f;
    public float fedcurrencydevaluations = 0.02f;
    public float currencyvalue = 1;
    public float foreignfailure = 0;
    public float fedcorruption = 50;
    public float citycurrency = 0;
    public float relationwithstates = 0;
    public float relationswithmegaregions = 0;
    public float fedbasenumber = 1000;
    public float fedpower = 0;
    public float ownbasenumber = 0;
    public float ownpower = 0;
    public bool _citycurrency = false;
    public bool gameloss = false;
    public bool gamewin = false;
    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        temp = obj[1].GetComponentInChildren<Image>().color;
        Init();
    }

    public void Init()
    {
        statemachine = new statemachine<GameController>(i);
        statemachine.Push(BeginState.i);
    }

    public void Init2()
    {
        SelectFactionState.i.ChooseYourFaction.gameObject.SetActive(false);
        SelectFactionState.i.factionselected = false;
        SelectFactionState.i.ownfaction = false;
        ChosenFaction = null;
        if (SelectFactionState.i.ownFactionName != "") 
        {
            GameObject gm = GameObject.Find(SelectFactionState.i.ownFactionName);
            var factionobject = gm.GetComponent<FactionObject>();
            factionobject = null;
            //factionobject.ownfaction = false;
            SelectFactionState.i.ownFactionName = "";
            
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].GetComponentInChildren<Image>().color = temp;
            }
        }
        GameObject TempCanvas = GameObject.Find("Canvas");
        CalendarTime calendar = TempCanvas.GetComponent<CalendarTime>();
        calendar.yearIndex = 2019;
        calendar.monthIndex = 1;
        calendar.dayIndex = 1;
        statemachine = new statemachine<GameController>(i);
        statemachine.ChangeState(SelectFactionState.i);        
        SelectFactionState.i.Init();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!SelectFactionState.i.factionselected && statemachine.CurrentState != MenuState.i
            && statemachine.CurrentState != BeginState.i && statemachine.CurrentState != SelectFactionState.i)
        {
            statemachine.ChangeState(SelectFactionState.i);
        }
        if (SelectFactionState.i.factionselected && statemachine.CurrentState == SelectFactionState.i)
        {
            start = true;
            statemachine.ChangeState(MapState.i);
        }

        if (statemachine.CurrentState != BeginState.i)
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (statemachine.CurrentState != MenuState.i)
                {
                    statemachine.Push(MenuState.i);
                    MenuState.i.CheckActive();
                    MenuState.i.CheckButtonPressed();
                }
                else if (statemachine.CurrentState == MenuState.i)
                {
                 MenuState.i.CheckActive();
                 MenuState.i.CheckButtonPressed();
                 statemachine.Pop();
                }
            } 
        }
        if (GameController.i.statemachine.CurrentState ==MapState.i) 
        { 
            Gamecore();
            CheckAgenda();
        }
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();            
        }
        statemachine.Execute();
    }

    public void GetMessage(Option.option tempoption)
    {
        var tempother = GameObject.Find("OtherFactionInfo");
        GameObject tempcanvas = GameObject.Find("Canvas");
        var tempcam = GameObject.Find("Main Camera");
        Params _params = tempcam.GetComponent<Params>();
        GameController gamecontroller = GetComponent<GameController>();
        string tempothernametext = "";
        //search done per gamestate since the gameobject is from different menus
        if (gamecontroller.statemachine.CurrentState == DiplomacyStateMain.i)
        { 
            var tempothertext = tempother.GetComponent<TextMeshProUGUI>();
            tempothernametext = tempothertext.text;
        }
        if (gamecontroller.statemachine.CurrentState == DiplomacyStateMinor.i)
        {
            var tempothertext = tempother.GetComponent<TextMeshProUGUI>();
            tempothernametext = tempothertext.text;
        }
        if (gamecontroller.statemachine.CurrentState == DepartmentState.i)
        {
            tempother = GameObject.Find("mayorname");
            var tempothertext = tempother.GetComponent<TextMeshProUGUI>();
            tempothernametext = tempothertext.text;
        }

        if (tempoption.effectcat == "self")
        {
            MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
            if (tempoption.effectarea == "mayor")
            {
                _params.effectcat = tempoption.effectcat;
                _params.effectarea = tempoption.effectarea;
                _params.integergm = tempoption.integergm;
                _params.efficiency = tempoption.effectmod;
                _params.effectmod = tempoption.effectmod;
                _params.department = "mayorOffice";
            }
            if (tempoption.effectarea == "efficiency")
            {
                _params.effectcat = tempoption.effectcat;
                _params.effectarea = tempoption.effectarea;
                _params.integergm = tempoption.integergm;
                _params.efficiency = tempoption.effectmod;
                _params.effectmod = tempoption.effectmod;
                _params.department = "mayorOffice";

            }
            if (tempoption.effectcat == "corruption")
            {
                if (tempoption.integergm == 6)
                {
                    Revenue revenue = tempcanvas.GetComponent<Revenue>();
                    _params.effectcat = tempoption.effectcat;
                    _params.effectarea = tempoption.effectarea;
                    _params.integergm = tempoption.integergm;
                    _params.corruption = tempoption.effectmod;
                    _params.effectmod = tempoption.effectmod;
                    _params.department = "revenue";
                }
                else
                {
                    _params.effectcat = tempoption.effectcat;
                    _params.effectarea = tempoption.effectarea;
                    _params.integergm = tempoption.integergm;
                    _params.efficiency = tempoption.effectmod;
                    _params.effectmod = tempoption.effectmod;
                    _params.department = "mayorOffice";
                }
                if ((tempoption.effectmod < 0) || (tempoption.effectmod > 1))
                {
                    if (tempoption.integergm == 6)
                    {
                        Revenue revenue = tempcanvas.GetComponent<Revenue>();
                        _params.effectcat = tempoption.effectcat;
                        _params.effectarea = tempoption.effectarea;
                        _params.integergm = tempoption.integergm;
                        _params.efficiency = tempoption.effectmod;
                        _params.effectmod = tempoption.effectmod;
                        _params.department = "revenue";
                    }
                    else
                    {
                        _params.effectcat = tempoption.effectcat;
                        _params.effectarea = tempoption.effectarea;
                        _params.integergm = tempoption.integergm;
                        _params.efficiency = tempoption.effectmod;
                        _params.effectmod = tempoption.effectmod;
                        _params.department = "mayorOffice";
                    }
                }
            }
        }
        if (tempoption.effectcat == "crime")
        {
            Police police = tempcanvas.GetComponent<Police>();
            _params.effectcat = tempoption.effectcat;
            _params.effectarea = tempoption.effectarea;
            _params.integergm = tempoption.integergm;
            _params.efficiency = tempoption.effectmod;
            _params.effectmod = tempoption.effectmod;
            _params.department = "police";
        }
        if (tempoption.effectcat == "parties")
        {
            //kinda faking the effect for the other party, for brevity of development.
            MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
            
            _params.otherfactionnamne = tempothernametext;
            if (tempoption.effectarea == "revenue")
            {
                _params.effectcat = tempoption.effectcat;
                _params.effectarea = tempoption.effectarea;
                _params.integergm = tempoption.integergm;
                _params.efficiency = tempoption.effectmod;
                _params.effectmod = tempoption.effectmod;
                _params.department = "mayorOffice";
            }
            if (tempoption.effectarea == "efficiency")
            {
                _params.effectcat = tempoption.effectcat;
                _params.effectarea = tempoption.effectarea;
                _params.integergm = tempoption.integergm;
                _params.efficiency = tempoption.effectmod;
                _params.effectmod = tempoption.effectmod;
                _params.department = "mayorOffice";
            }
            if (tempoption.effectcat == "police")
            {
                Police police = tempcanvas.GetComponent<Police>();

                _params.effectcat = tempoption.effectcat;
                _params.effectarea = tempoption.effectarea;
                _params.integergm = tempoption.integergm;
                _params.efficiency = tempoption.effectmod;
                _params.effectmod = tempoption.effectmod;
            }
        }
        tempoption.measureactive = true;
        _params.measureactive = tempoption.measureactive;
        processparams = true;
        sendmessage = false;
    }

    public void OnGUI()
    {
        var style = new GUIStyle
        {
            fontSize = 24
        };

        GUILayout.Label("STATE STACK", style);
        foreach (var state in statemachine.StateStack)
        {
            GUILayout.Label(state.GetType().ToString(), style);
        }
    }

    public void CheckClick()
    {
        var ray = CamController.i.zoomCam.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        LayerMask layerDefault = LayerMask.NameToLayer("Default");
        Physics2D.queriesHitTriggers = true;
        GameObject TempCanvas = GameObject.Find("Canvas");
        MissionManager missionManager = TempCanvas.GetComponent<MissionManager>();
        GameObject Gm;
        if (hit.collider != null)
        {
            Gm = GameObject.Find(hit.collider.gameObject.name);
            if (statemachine.CurrentState == DiplomacyStateMain.i
                || statemachine.CurrentState == DiplomacyStateMinor.i
                || statemachine.CurrentState == DepartmentState.i
                )
            {
                Gm = null;
                Physics2D.queriesHitTriggers = false;
                StartCoroutine(Delayt());
            }

            if (statemachine.CurrentState == MenuState.i)
            {
                MenuState.i.CheckButton(Gm);
            }
            if (statemachine.CurrentState == SelectFactionState.i)
            {
                Physics2D.queriesHitTriggers = true;
                if (!SelectFactionState.i.buttonpressed)
                {
                    for (int i = 0; i < SelectFactionState.i.MainFactions.Length; i++)
                    {
                        if (Gm.name == SelectFactionState.i.MainFactions[i].name)
                        {
                            ChosenFaction = Gm;
                            SelectFactionState.i.buttonpressed = true;
                            SelectFactionState.i.CastRay(Gm);
                        }
                    }
                }
            }
            if (statemachine.CurrentState == MapState.i && missionManager.MissionManagerActive == true)
            {
                Gm = null;
                Physics2D.queriesHitTriggers = false;
            }
            else if (statemachine.CurrentState == MapState.i)
            {
                DiplomacyObject diplomacyObject = GetComponent<DiplomacyObject>();
                diplomacyObject.LoveYourself();
                if (Gm.CompareTag("MainFaction"))
                {
                    statemachine.Push(DiplomacyStateMain.i);
                    DiplomacyStateMain.i.CheckObject(Gm);
                    StartCoroutine(Delayt());
                }
                if ((statemachine.CurrentState != DiplomacyStateMain.i) && (Gm.CompareTag("MinorFaction")))
                {
                    statemachine.Push(DiplomacyStateMinor.i);
                    DiplomacyStateMinor.i.CheckObject(Gm);
                    StartCoroutine(Delayt());
                }
            }
        }
    }

    public void Gamecore()
    {
        GameObject TempCalendar = GameObject.Find("Canvas");
        CalendarTime calendarTime = TempCalendar.GetComponent<CalendarTime>();
        GameObject tempcanvas = GameObject.Find("Canvas");
        //process params
        if (processparams)
        {
            //calendarTime.Timeincreased = true;
            var tempcam = GameObject.Find("Main Camera");
            Params _params = tempcam.GetComponent<Params>();
            if (_params.effectcat == "self")
            {
                MayorOffice mayorOffice = TempCalendar.GetComponent<MayorOffice>();
                if (_params.effectarea == "mayor")
                {
                    if (_params.integergm == 1)
                    {
                        if (ownpower >= 500 && fedpower <= 800)
                        {
                            string wintext = new string("You have succesfully seceeded to form your own state.");
                            gamewin = true;
                            WinLose(wintext);
                        }
                        if (ownpower < 500 || fedpower > 800)
                        {
                            string losetext = new string("Secession is seen as illegal, Black SUV's park in front of the Mayor's office. And men in black suits take you away!");
                            gameloss = true;
                            WinLose(losetext);
                        }
                    }
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._efficiency = mayorOffice._efficiency + (mayorOffice._efficiency * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._efficiency += _params.effectmod;
                    }
                }
                if (_params.effectarea == "efficiency")
                {
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._efficiency = mayorOffice._efficiency + (mayorOffice._efficiency * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._efficiency += _params.effectmod;
                    }
                }
                if (_params.effectarea == "corruption")
                {
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._corruption = mayorOffice._corruption + (mayorOffice._corruption * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._corruption += _params.effectmod;
                    }
                }
            }
            if ((_params.effectarea == "corruption") && (_params.effectcat == "Revenue"))
            {
                Revenue revenue = tempcanvas.GetComponent<Revenue>();
                if ((_params.effectmod > 0) && (_params.effectmod < 1))
                {
                    revenue._corruption = revenue._corruption + (revenue._corruption * _params.effectmod);
                }
                if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                {
                    revenue._corruption += _params.effectmod;
                }
            }
            if (_params.effectcat == "crime")
            {
                Police police = tempcanvas.GetComponent<Police>();
                if ((_params.effectmod > 0) && (_params.effectmod < 1))
                {
                    police._efficiency = police._efficiency + (police._efficiency * _params.efficiency);
                }
                if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                {
                    police._efficiency += _params.efficiency;
                }
            }
            if (_params.effectcat == "parties")
            {
                MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
                Revenue revenue = tempcanvas.GetComponent<Revenue>();
                if (_params.effectarea == "mayor")
                {
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._efficiency = mayorOffice._efficiency + (mayorOffice._efficiency * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._efficiency += _params.effectmod;
                    }
                }

                if (_params.effectarea == "revenue")
                {
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._corruption = mayorOffice._corruption + (mayorOffice._corruption * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._corruption += _params.effectmod;
                    }
                }
                if (_params.effectarea == "efficiency")
                {
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        mayorOffice._efficiency = mayorOffice._efficiency + (mayorOffice._efficiency * _params.effectmod);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        mayorOffice._efficiency += _params.effectmod;
                    }
                }
                if (_params.effectcat == "police")
                {
                    Police police = tempcanvas.GetComponent<Police>();
                    if ((_params.effectmod > 0) && (_params.effectmod < 1))
                    {
                        police._efficiency += (police._efficiency * _params.efficiency);
                    }
                    if ((_params.effectmod <= 0) || (_params.effectmod >= 1))
                    {
                        police._efficiency += _params.efficiency;
                    }
                }
                FactionObject factionbject = ChosenFaction.GetComponent<FactionObject>();
                DiplomacyObject diplomacyObject = GameController.i.GetComponent<DiplomacyObject>();
                for (int a = 0; a < diplomacyObject.relationstates.Length; a++)
                {
                    if (diplomacyObject.relationstates[a].FactionName == _params.otherfactionnamne)
                    {
                        diplomacyObject.relationstates[a].RelationNumber += 10;
                    }
                }
            }
            _params = default;
            processparams = false;
        }
        
        if ((SelectFactionState.i.factionselected) && (start))
        {
            //set budget percentages from received taxes
            FactionObject factionbject = ChosenFaction.GetComponent<FactionObject>();
            double GDP = factionbject._GDP;
            double budget = factionbject._GDP * 0.2; //as general fund
            
            Economy economy = tempcanvas.GetComponent<Economy>();
            if ((calendarTime._yearIndex == 2019) && (calendarTime._monthIndex == 1)
                   && (calendarTime._dayIndex == 1))
            {
                //fill all department values for start
                Revenue revenue = tempcanvas.GetComponent<Revenue>();
                MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
                mayorOffice._budget = (float)(revenue._taxes_collected * 0.2);
                mayorOffice._personnel = (float)(mayorOffice._budget * 0.3);
                mayorOffice._efficiency = 50f;
                mayorOffice._tech_level = 1;
                mayorOffice._corruption = factionbject.corruption;
                mayorOffice._quality_department_heads = 25;
                Education education = tempcanvas.GetComponent<Education>();
                education._budget = (float)(revenue._taxes_collected * 0.05);
                education._personnel = (float)(education._budget * 0.3);
                education._efficiency = 50f;
                education._elementary_education = (float)(education._budget * 0.15);
                education._middle_education = (float)(education._budget * 0.15);
                education._higher_education = (float)(education._budget * 0.15);
                education._illiteracy_percent = 15f;
                Fire fire = tempcanvas.GetComponent<Fire>();
                fire._budget = (float)(revenue._taxes_collected * 0.05);
                fire._personnel = (float)(fire._budget * 0.3);
                fire._efficiency = 50f;
                fire._equipment = (float)(fire._budget * 0.7);
                Health health = tempcanvas.GetComponent<Health>();
                health._budget = (float)(revenue._taxes_collected * 0.1);
                health._personnel = (float)(health._budget * 0.3);
                health._efficiency = 50f;
                health._corruption = factionbject.corruption;
                health._hospitals = (float)(health._budget * 0.7);
                Housing housing = tempcanvas.GetComponent<Housing>();
                housing._budget = (float)(revenue._taxes_collected * 0.1);
                housing._personnel = (float)(housing._budget * 0.3);
                housing._efficiency = 50f;
                housing._corruption = factionbject.corruption;
                housing._total_housing = 100f;
                housing._new_housing = 90f;
                housing._housingneed = 10;
                Justice justice = tempcanvas.GetComponent<Justice>();
                justice._budget = (float)(revenue._taxes_collected * 0.15);
                justice._personnel = (float)(justice._budget * 0.3);
                justice._efficiency = 50;
                justice._corruption = factionbject.corruption;
                justice._municipalcourts = (float)(justice._budget * 0.2);
                justice._courts = (float)(justice._budget * 0.2);
                justice._prisons = (float)(justice._budget * 0.2);
                Parks parks = tempcanvas.GetComponent<Parks>();
                parks._budget = (float)(revenue._taxes_collected * 0.15);
                parks._personnel = (float)(parks._budget * 0.3);
                parks._efficiency = 50;
                parks._parks = (float)(parks._budget * 0.3);
                parks._equipment = (float)(parks._budget * 0.3);
                Police police = tempcanvas.GetComponent<Police>();
                police._budget = (float)(revenue._taxes_collected * 0.15);
                police._personnel = (float)(police._budget * 0.3);
                police._efficiency = 50;
                police._corruption = factionbject.corruption ;
                police._equipment = (float)(police._budget * 0.3);
                Publicworks publicworks = tempcanvas.GetComponent<Publicworks>();
                publicworks._budget = (float)(revenue._taxes_collected * 0.1);
                publicworks._personnel = (float)(publicworks._budget * 0.3);
                publicworks._efficiency = 50;
                publicworks._corruption = factionbject.corruption;
                publicworks._maintenance_sewers = (float)(publicworks._budget * 0.3);
                publicworks._maintenance_streets = (float)(publicworks._budget * 0.3);

                revenue._budget = (float)(revenue._taxes_collected * 0.05);
                revenue._personnel = (float)(publicworks._budget * 0.3);
                revenue._efficiency = 50;
                revenue._corruption = factionbject.corruption;

                Transportation transportation = tempcanvas.GetComponent<Transportation>();
                transportation._budget = (float)(revenue._taxes_collected * 0.05);
                transportation._personnel = (float)(transportation._budget * 0.3);
                transportation._efficiency = 50;
                transportation._corruption = factionbject.corruption;
                transportation._public_transport = (float)(transportation._budget * 0.3);
                transportation._road_maintenance = (float)(publicworks._budget * 0.3);
                
            }start = false;
        }
        if (ChosenFaction != null)
        {
            relationwithstates = 0;
            relationswithmegaregions = 0;
            FactionObject factionbject = ChosenFaction.GetComponent<FactionObject>();

            //calculate ownpower
            //democratic mandate is basenumber
            float wealthperperson = (float)factionbject._GDP / factionbject._population;
            ownbasenumber = (float)(factionbject._population / 1000000) + (wealthperperson / 1000);
            MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
            DiplomacyObject diplomacyObject = GameController.i.GetComponent<DiplomacyObject>();
            for (int a = 0; a < diplomacyObject.relationstates.Length; a++)
            {
                if (diplomacyObject.relationstates[a].RelationNumber > 25)
                {
                    for(int b = 0; b < diplomacyObject.MainFactions.Length; b++)
                    {
                        if (diplomacyObject.relationstates[a].FactionName == diplomacyObject.MainFactions[b].name)
                        {
                            relationswithmegaregions += 1;
                        }
                        else
                        { relationwithstates += 1; }
                    }               
                }
            }
            if (!_citycurrency)
            {
                ownpower = ownbasenumber + mayorOffice._efficiency - currencydevaluations
                + relationwithstates + relationswithmegaregions;
            }
            else
            {
                ownpower = ownbasenumber + mayorOffice._efficiency + citycurrency
                + relationwithstates + relationswithmegaregions;
            }
            //calculate fedpower
            fedpower = fedbasenumber - fedcurrencydevaluations - foreignfailure - fedcorruption;
            fedgovpowint.text = fedpower.ToString("F0");
            ownpowint.text = ownpower.ToString("F0");
            float currencyvalue = 1;
        }

        if (calendarTime.Timeincreased)
        {
            calendarTime.Timeincreased = false;

            Revenue revenue = tempcanvas.GetComponent<Revenue>();
            FactionObject factionbject = ChosenFaction.GetComponent<FactionObject>();
            //calculate new gdp / economy
            float monthlybudget = revenue._taxes_collected / 12;
            revenue._public_purse -= monthlybudget;
            float monthlyeconomicgrowth = economicgrowthfactor / 12;
            float economicgrowth = (float)(factionbject._GDP * monthlyeconomicgrowth);
            factionbject._GDP += economicgrowth;
            //calculate transport in relation to gdp
            float road_transport = (float)factionbject.road_transport;
            road_transport += monthlyeconomicgrowth;
            float rail_transport = (float)factionbject.rail_transport;
            rail_transport += monthlyeconomicgrowth;
            float air_transport = (float)factionbject.air_transport;
            air_transport += monthlyeconomicgrowth;
            float ship_transport = (float)factionbject.ship_transport;
            ship_transport += monthlyeconomicgrowth;
            //calculate population increase/decreas
            PopulationIndex populationIndex = tempcanvas.GetComponent<PopulationIndex>();
            populationIndex._population = (float)factionbject._population / 1000000;
            populationIndex._population += monthlyeconomicgrowth;
            //calculate wealth per person
            float wealthperperson = (float)factionbject._GDP / factionbject._population;
            //calculate crime
            Police police = tempcanvas.GetComponent<Police>();
            float fincancialconsequencecrime = ((police._efficiency - police._corruption) *100) - wealthperperson;
            float crimestat = (Mathf.Abs(police._efficiency - police._corruption) / 5000) * (factionbject._population / 100000);
            if (fincancialconsequencecrime > 5000)
            {
                crimestat += 1;
            }
            else
            {
                crimestat -= 1;
            }
            //calculate pressure health system / drug abuse
            //crimestat and health.efficiency 
            Health health = tempcanvas.GetComponent<Health>();
            float healthpressure = Mathf.Abs(health._efficiency / health._corruption);
            if (healthpressure >= 1)
            {
                fincancialconsequencecrime -= 3000;
            }
            if (healthpressure >= 0.5f)
            {
                fincancialconsequencecrime -= 1500;
            }
            else
            {
                fincancialconsequencecrime += 1000;
            }
            currencyvalue -= (currencydevaluations / 12);

            //calculate mission and measure effects
            //uses basenumber


            //base number + event effect - mayor office and mission effect
        }
        currint.text = currencyvalue.ToString("F5");
        processparams = false;
        GameObject[] minorfaction = GameObject.FindGameObjectsWithTag("MinorFaction");
    }
    
    public void CheckAgenda()
    {
        GameObject tempCanvas = GameObject.Find("Canvas");
        CalendarTime calendarTime = tempCanvas.GetComponent<CalendarTime>();
        MissionManager missionManager = tempCanvas.GetComponent<MissionManager>();        
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 1)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 2)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 3)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 10)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 18)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 25)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 1 && calendarTime._dayIndex == 28)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 2 && calendarTime._dayIndex == 1)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 2 && calendarTime._dayIndex == 15)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 2 && calendarTime._dayIndex == 21)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 2 && calendarTime._dayIndex == 22)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 2 && calendarTime._dayIndex == 27)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 3 && calendarTime._dayIndex == 12)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 3 && calendarTime._dayIndex == 16)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 3 && calendarTime._dayIndex == 30)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 4 && calendarTime._dayIndex == 1)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 4 && calendarTime._dayIndex == 2)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 4 && calendarTime._dayIndex == 4)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
        if (calendarTime._yearIndex == 2019 && calendarTime._monthIndex == 4 && calendarTime._dayIndex == 8)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }

        if (calendarTime._yearIndex == 2020 && calendarTime._monthIndex == 3 && calendarTime._dayIndex == 25)
        {
            if (calendarTime.Dayincreased)
            {
                missionManager.dooncemission = true;
            }
            missionManager.GetDate(calendarTime._yearIndex, calendarTime._monthIndex, calendarTime._dayIndex);
        }
    }

    public void WinLose(string text)
    {
        GameObject TempCanvas = GameObject.Find("Canvas");
        MissionManager missionManager = TempCanvas.GetComponent<MissionManager>();
        missionManager.MissionManagerActive = true;
        missionManager.dooncemission = false;
        GraphicCanvasRaycast graphraycast = TempCanvas.GetComponent<GraphicCanvasRaycast>();
        graphraycast.possibleresponschosen = false;
        GameObject panel = new GameObject("panel");
        panel.AddComponent<CanvasRenderer>();
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(1f, 1f, 1f, .65f);
        panel.transform.SetParent(TempCanvas.transform, false);

        var panelrect = panel.GetComponent<RectTransform>();
        panelrect.anchorMin = new Vector2(0, 1);
        panelrect.anchorMax = new Vector2(0, 1);
        panelrect.pivot = new Vector2(0, 1);
        panelrect.localPosition = new Vector2(-(Screen.width * 0.8f) / 2, (Screen.height * 0.725f) * 0.5f);
        panelrect.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);

        GameObject panelHeader = new GameObject();
        panelHeader.transform.SetParent(panel.transform);
        var panelHeaderrect = panelHeader.AddComponent<RectTransform>();
        panelHeaderrect.anchorMin = new Vector2(0, 1);
        panelHeaderrect.anchorMax = new Vector2(0, 1);
        panelHeaderrect.pivot = new Vector2(0, 1);
        panelHeaderrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, -panelrect.sizeDelta.y / 20);
        panelHeaderrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, panelrect.sizeDelta.y / 20);
        var panelHeadertexBox = panelHeader.AddComponent<TextMeshProUGUI>();
        if (gameloss)
        {
            panelHeadertexBox.text = "What a shame, You lost! Because: ";
            panelHeadertexBox.color = Color.black;
            panelHeadertexBox.alignment = TextAlignmentOptions.Center;

            GameObject panelEventtext = new GameObject();
            panelEventtext.transform.SetParent(panel.transform);
            var panelEventtextrect = panelEventtext.AddComponent<RectTransform>();
            panelEventtextrect.anchorMin = new Vector2(0, 1);
            panelEventtextrect.anchorMax = new Vector2(0, 1);
            panelEventtextrect.pivot = new Vector2(0, 1);
            var panelEventtextBox = panelEventtext.AddComponent<TextMeshProUGUI>();
            panelEventtextBox.text = text;
            panelEventtextBox.fontSize = 21;
            panelEventtextBox.color = Color.black;
            float paneleventtextheight = 120;
            panelEventtextrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, paneleventtextheight);
            panelEventtextrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, -(panelrect.sizeDelta.y / 20) * 5);
        }
        if (gamewin)
        {
            panelHeadertexBox.text = "Great Job! You won! This was your first step in restoring the republic" + text;
            panelHeadertexBox.color = Color.black;
        }
        //created custom button from script via
        //https://forum.unity.com/threads/ui-button-create-by-script-c.285829/#post-9149029
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        GameObject panelFooter = DefaultControls.CreateButton(uiResources);
        panelFooter.transform.SetParent(panel.transform);
        var panelFooterrect = panelFooter.GetComponent<RectTransform>();
        panelFooterrect.anchorMin = new Vector2(0, 1);
        panelFooterrect.anchorMax = new Vector2(0, 1);
        panelFooterrect.pivot = new Vector2(0, 1);
        panelFooterrect.localPosition = new Vector2(panelrect.sizeDelta.x / 4, -(panelrect.sizeDelta.y / 20) * 18);
        panelFooterrect.sizeDelta = new Vector2(panelrect.sizeDelta.x / 2, panelrect.sizeDelta.y / 20);


        Text panelFootertexBox = panelFooter.GetComponentInChildren<Text>();
        panelFootertexBox.text = "Ok";
        panelFootertexBox.color = Color.black;
        Button panelFooterClick = panelFooter.GetComponent<Button>();
        panelFooterClick.onClick.AddListener(() => Onclick());
        gameloss = false;
        gamewin = false;
    }
    public void Onclick()
    {
        GameObject TempCanvas = GameObject.Find("Canvas");
        GraphicCanvasRaycast graphicCanvas = TempCanvas.GetComponent<GraphicCanvasRaycast>();
        MissionManager missionManager = TempCanvas.GetComponent<MissionManager>();
        missionManager.TempdateEvent = default;
        missionManager.dateEvents = default;
        GameObject destroy = GameObject.Find("panel");
        Destroy(destroy);
        missionManager.MissionManagerActive = false;
        statemachine.ChangeState(BeginState.i);
        BeginState.i.Init();
    }

    public IEnumerator Delayt()
    {
        yield return new WaitForSeconds(0.2f);
    }
}