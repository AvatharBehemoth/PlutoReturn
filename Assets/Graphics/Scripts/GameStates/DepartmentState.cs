using stateman;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepartmentState : state<GameController>
{
    public static DepartmentState i { get; private set; }

    public RectTransform DepartMenu;
    public RectTransform MayorsMenu;
    public static List<Option.option> mayOptions;
    public static List<string> liststring;
    float[] membervalues;
    float[] tempmembervalues;
    public bool mayormeasures = false;
    public bool mayorvariables = false;
    public bool departmentvariables = false;
    public float temptime;

    private void Awake()
    {
        i = this;
    }

    public override void Execute()
    {
        CamController.i.HandleUpdate();
    }

    public void FillMenumayor(GameObject Gm)
    {
        temptime = Time.time;
        mayormeasures = false;
        mayorvariables = true;
        DepartMenu.gameObject.SetActive(false);
        GameObject tempcanvas = GameObject.Find("Canvas");
        MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
        membervalues = new float[0];

        var i = 0;
        var j = 0;
        var k = 0;
        var l = 0;
        if ((Gm.name == "MayorsOffice") || (Gm.name == "mayvariables"))
        {
            GameObject[] departarray = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var department in departarray)
            {
                if (department.name == "MayorsMenu")
                {
                    GameObject MayorsMenu = department.gameObject;
                    MayorsMenu.gameObject.SetActive(true);
                    DepartMenu.gameObject.SetActive(false);
                }
            }

            GameObject varpanel = GameObject.Find("varpanel");
            GameObject OwnFactionText = GameObject.Find("mayorname");
            var OwnFactionTexttext = OwnFactionText.GetComponent<TextMeshProUGUI>();
            SelectFactionState selectFactionState = GetComponent<SelectFactionState>();
            OwnFactionTexttext.text = selectFactionState.ownFactionName;

            //show variables in menu
            liststring = new List<string>();
            liststring = mayorOffice.GetList();
            membervalues = new float[6];
            membervalues[0] = mayorOffice._budget;
            membervalues[1] = mayorOffice._personnel;
            membervalues[2] = mayorOffice._efficiency;
            membervalues[3] = mayorOffice._tech_level;
            membervalues[4] = mayorOffice._corruption;
            membervalues[5] = mayorOffice._quality_department_heads;
            foreach (string item in liststring)
            {
                //create departmentvariables and slider game objects
                GameObject depvar = new GameObject();
                GameObject depvarmemberval = new GameObject();
                GameObject depvarSliderHolder = new GameObject();
                GameObject depvarSlider = new GameObject();
                GameObject depvarSliderHolderText = new GameObject();
                GameObject Background = new GameObject();
                GameObject FillArea = new GameObject();
                GameObject Fill = new GameObject();
                GameObject HandleSlideArea = new GameObject();
                GameObject Handle = new GameObject();

                depvar.transform.SetParent(varpanel.transform);
                depvarmemberval.transform.SetParent(depvar.transform);
                depvarSliderHolder.transform.SetParent(varpanel.transform);
                depvarSlider.transform.SetParent(depvarSliderHolder.transform);
                Background.transform.SetParent(depvarSlider.transform);
                FillArea.transform.SetParent(depvarSlider.transform);
                Fill.transform.SetParent(FillArea.transform);
                HandleSlideArea.transform.SetParent(depvarSlider.transform);
                Handle.transform.SetParent(HandleSlideArea.transform);
                depvarSliderHolderText.transform.SetParent(depvarSliderHolder.transform);

                var depvartransform = depvar.AddComponent<RectTransform>();
                var depvarmembervaltransform = depvarmemberval.AddComponent<RectTransform>();
                var depvarsliderHoldertransform = depvarSliderHolder.AddComponent<RectTransform>();
                var depvarSlidertransform = depvarSlider.AddComponent<RectTransform>();
                var Backgroundtransform = Background.AddComponent<RectTransform>();
                var FillAreatransform = FillArea.AddComponent<RectTransform>();
                var Filltransform = Fill.AddComponent<RectTransform>();
                var HandleSlideAreatransform = HandleSlideArea.AddComponent<RectTransform>();
                var Handletransform = Handle.AddComponent<RectTransform>();
                var depvarSliderHolderTexttransform = depvarSliderHolderText.AddComponent<RectTransform>();

                depvartransform.anchorMin = new Vector2(0, 1);
                depvartransform.anchorMax = new Vector2(0, 1);
                depvartransform.pivot = new Vector2(0, 1);

                depvarmembervaltransform.anchorMin = new Vector2(0, 1);
                depvarmembervaltransform.anchorMax = new Vector2(0, 1);
                depvarmembervaltransform.pivot = new Vector2(0, 1);

                depvarsliderHoldertransform.anchorMin = new Vector2(0, 1);
                depvarsliderHoldertransform.anchorMax = new Vector2(0, 1);
                depvarsliderHoldertransform.pivot = new Vector2(0, 1);

                depvarSlidertransform.anchorMin = new Vector2(0, 1);
                depvarSlidertransform.anchorMax = new Vector2(0, 1);
                depvarSlidertransform.pivot = new Vector2(0, 1);

                Backgroundtransform.anchorMin = new Vector2(0, 0);
                Backgroundtransform.anchorMax = new Vector2(1, 1);
                Backgroundtransform.pivot = new Vector2(0.5f, 0.5f);

                FillAreatransform.anchorMin = new Vector2(0, 0);
                FillAreatransform.anchorMax = new Vector2(1, 1);
                FillAreatransform.pivot = new Vector2(0.5f, 0.5f);

                Filltransform.anchorMin = new Vector2(0, 0);
                Filltransform.anchorMax = new Vector2(1, 1);
                Filltransform.pivot = new Vector2(0.5f, 0.5f);

                HandleSlideAreatransform.anchorMin = new Vector2(0, 0);
                HandleSlideAreatransform.anchorMax = new Vector2(1, 1);
                HandleSlideAreatransform.pivot = new Vector2(0.5f, 0.5f);

                Handletransform.anchorMin = new Vector2(0, 1);
                Handletransform.anchorMax = new Vector2(0, 1);
                Handletransform.pivot = new Vector2(0, 1);

                depvarSliderHolderTexttransform.anchorMin = new Vector2(0, 1);
                depvarSliderHolderTexttransform.anchorMax = new Vector2(0, 1);
                depvarSliderHolderTexttransform.pivot = new Vector2(0, 1);

                depvar.name = item;
                depvarmemberval.name = item + "value";
                depvarSliderHolder.name = item + "sliderholder";
                depvarSlider.name = item + "slider";
                Background.name = "Background";
                FillArea.name = "FillArea";
                Fill.name = "Fill";
                HandleSlideArea.name = "HandleSlideArea";
                Handle.name = "Handle";
                depvarSliderHolderText.name = "sliderholdertext";

                depvartransform.localPosition = new Vector2(0, j);
                depvarmembervaltransform.localPosition = new Vector2(0, 0);
                depvarsliderHoldertransform.localPosition = new Vector2(320, j);
                depvarSlidertransform.localPosition = new Vector2(0, 0);
                depvarSliderHolderTexttransform.localPosition = new Vector2(0, -15);

                var depvarmembervaltext = depvarmemberval.AddComponent<TextMeshProUGUI>();
                if (item == "budget" || item == "personnel")
                {
                    depvarmembervaltext.text = (membervalues[l] / 1000000).ToString() + " M";
                }
                else
                {
                    depvarmembervaltext.text = membervalues[l].ToString();
                }
                depvarSlider.AddComponent<Slider>();
                Background.AddComponent<Image>();
                var Backgroundimage = Background.GetComponent<Image>();
                Backgroundimage.sprite = (Sprite)Resources.Load("Assets/background");
                Handle.AddComponent<Image>();
                var Handleimage = Handle.GetComponent<Image>();
                Handleimage.sprite = Backgroundimage.sprite;
                Handleimage.color = new Color(0x00, 0x00, 0x00);
                depvarSlider.GetComponent<Slider>().targetGraphic = Handle.GetComponent<Image>();
                depvarSlider.GetComponent<Slider>().fillRect = Fill.GetComponent<RectTransform>();
                depvarSlider.GetComponent<Slider>().handleRect = Handle.GetComponent<RectTransform>();
                depvarSlider.GetComponent<Slider>().minValue = 0;
                depvarSlider.GetComponent<Slider>().maxValue = 200;
                depvarSlider.GetComponent<Slider>().value = 100;
                var depText = depvarSliderHolderText.AddComponent<TextMeshProUGUI>();
                depText.text = "100 %";
                tempmembervalues = new float[2];
                depvarSlider.GetComponent<Slider>().onValueChanged.AddListener(
                    (v) => {
                    depText.text = v.ToString() + " %";
                        if (item == "budget")
                        {
                            depvarmembervaltext.text = (((v * membervalues[0] / 100) / 1000000).ToString("F0") + " M");
                            tempmembervalues[0] = (v * membervalues[0] / 100);
                        }
                        if (item == "personnel")
                        {
                            depvarmembervaltext.text = (((v * membervalues[1] / 100) / 1000000).ToString("F0") + " M");
                            tempmembervalues[1] = (v * membervalues[1] / 100);
                        }
                    }
                    );
                j -= 30;
                i = 30;
                k++;
                l++;
                TextMeshProUGUI depvartext = depvar.AddComponent<TextMeshProUGUI>();
                depvartext.text = depvar.name;

                var depvary = depvartransform.sizeDelta.y;
                var depvarx = depvartransform.sizeDelta.x;
                var depvarmembervaly = depvarmembervaltransform.sizeDelta.y;
                var depvarmembervalx = depvarmembervaltransform.sizeDelta.x;
                var depvarSliderHoldery = depvarsliderHoldertransform.sizeDelta.y;
                var depvarSliderHolderx = depvarsliderHoldertransform.sizeDelta.x;
                var depvarSlidery = depvarSlidertransform.sizeDelta.y;
                var depvarSliderx = depvarSlidertransform.sizeDelta.x;
                var Handley = Handletransform.sizeDelta.y;
                var Handlex = Handletransform.sizeDelta.x;
                var depvarSliderHolderTexty = depvarSliderHolderTexttransform.sizeDelta.y;
                var depvarSliderHolderTextx = depvarSliderHolderTexttransform.sizeDelta.x;

                depvary = i;
                depvarx = 240;
                depvarmembervaly = i;
                depvarmembervalx = 80;
                depvarSliderHoldery = i;
                depvarSliderHolderx = 160;
                depvarSlidery = i / 2;
                depvarSliderx = 160;

                depvarmembervaltransform.offsetMin = new Vector2(240, 0);
                depvarmembervaltransform.offsetMax = new Vector2(0, 0);
                Backgroundtransform.offsetMin = new Vector2(0, 0);
                Backgroundtransform.offsetMax = new Vector2(0, 0);
                FillAreatransform.offsetMin = new Vector2(0, 0);
                FillAreatransform.offsetMax = new Vector2(0, 0);
                Filltransform.offsetMin = new Vector2(0, 0);
                Filltransform.offsetMax = new Vector2(0, 0);
                HandleSlideAreatransform.offsetMin = new Vector2(0, 0);
                HandleSlideAreatransform.offsetMax = new Vector2(-5, 0);
                Handley = 5;
                Handlex = 5;
                depvarSliderHolderTexty = i / 2;
                depvarSliderHolderTextx = 160;

                depvartransform.sizeDelta = new Vector2(depvarx, depvary);
                depvarmembervaltransform.sizeDelta = new Vector2(depvarmembervalx, depvarmembervaly);
                depvarsliderHoldertransform.sizeDelta = new Vector2(depvarSliderHolderx, depvarSliderHoldery);
                depvarSlidertransform.sizeDelta = new Vector2(depvarSliderx, depvarSlidery);
                depvarSliderHolderTexttransform.sizeDelta = new Vector2(depvarSliderHolderTextx, depvarSliderHolderTexty);

                Handletransform.sizeDelta = new Vector2(Handlex, Handley);
                depvartext.fontSize = 21;
                depText.fontSize = 21;
                depText.color = Color.black;
                depText.alignment = TextAlignmentOptions.Center;
                depvarmembervaltext.fontSize = 15;
                depvarmembervaltext.color = Color.black;
                depvarmembervaltext.alignment = TextAlignmentOptions.Center;
                if (item == "budget" || item == "personnel")
                {
                    depvarSlider.GetComponent<Slider>().enabled = true;
                }
                else
                {
                    depvarSlider.GetComponent<Slider>().enabled = false;
                }
            }
        }
    }

    public List<Option.option> GetList()
    {
        mayOptions = new List<Option.option>();
        mayOptions.Add(new Option.option(
            "Secede from the state(s) to create your own state", 1, "self", "mayor", .35f, "treaty", false, 1));
        mayOptions.Add(new Option.option(
            "Make a single Charter to become one municipality", 2, "self", "mayor", 5, "treaty", false, 1));
        mayOptions.Add(new Option.option(
            "Train your department heads rigorously to improve their skill", 3, "self", "efficiency", 10, "measure", false, 1));
        mayOptions.Add(new Option.option(
            "organise a convention of departments of the megaregions. To increase synergy", 4, "self", "efficiency", 1, "measure", false, 1));
        return mayOptions;
    }

    public void FillMenumaymeasures()
    {
        // noticed a system level error in unity. i have fixed this by adding a bool condition.
        //when generating or derstroying game objects unity tends to randomly keep repeating the instruction to generate. 
        //leading to nullreferenceexceptionerrors.
        //the same happens when deleting game objects, without a bool condition.
        //intesting: when using breakpoint and going through each instruction
        //unity does not randomly repeat the generate instruction.
        mayormeasures = true;
        mayorvariables = false;
        mayOptions = GetList();
        //show variables in menu
        DepartMenu.gameObject.SetActive(false);
        GameObject tempcanvas = GameObject.Find("Canvas");
        MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();

        GameObject varpanel = GameObject.Find("varpanel");

        var i = 0;
        var j = 0;

        // generate game objects manually
        foreach (Option.option option in mayOptions)
        {
            GameObject prop = new GameObject();
            prop.transform.SetParent(varpanel.transform);
            prop.tag = "measure";
            var proptransform = prop.AddComponent<RectTransform>();
            proptransform.anchorMin = new Vector2(0, 1);
            proptransform.anchorMax = new Vector2(0, 1);
            proptransform.pivot = new Vector2(0, 1);

            prop.name = option.integergm.ToString();
            prop.transform.localPosition = new Vector2(0, j);

            TextMeshProUGUI proptext = prop.AddComponent<TextMeshProUGUI>();
            proptext.text = option.optiontext;
            Vector2 Size = proptext.GetPreferredValues(proptext.text);

            if (Size.x > 650)
            {
                i = 60;
            }
            else if (Size.x <= 650)
            {
                i = 30;
            }
            var propy = proptransform.sizeDelta.y;
            var propx = proptransform.sizeDelta.x;
            propy = i;
            propx = 480;
            proptransform.sizeDelta = new Vector2(propx, propy);
            proptext.fontSize = 25;
            GameObject backgroundimage = new GameObject();
            backgroundimage.name = "backgroundimage";
            backgroundimage.transform.SetParent(prop.transform);
            RectTransform bgi = backgroundimage.AddComponent<RectTransform>();
            Image bgimage = backgroundimage.AddComponent<Image>();
            bgi.anchorMin = new Vector2(0, 1);
            bgi.anchorMax = new Vector2(0, 1);
            bgi.pivot = new Vector2(0, 1);
            bgi.sizeDelta = new Vector2(propx, propy);
            bgi.transform.localPosition = new Vector2(0, 0);
            Color quarter = new Color(1f, 1f, 1f, .25f);
            bgimage.color = quarter;
            j -= i + 2;
            backgroundimage.SetActive(false);
        }
    }

    public void FillMenudep(GameObject Gm)
    { 
        //i have to manually create if statements to find the selected department(class) 
        //Because Unity3d reflection in unity gives back information from unity "under the hood" management,
        //so the anwer has to be filtered. it was less effort doing it manually.
        // hastag "why is Unity3d constantly fighting me? 
        MayorsMenu.gameObject.SetActive(false);
        departmentvariables = true;

        var i = 0;
        var j = 0;
        var k = 0;
        var l = 0;
        membervalues = new float[0];
        if ((gameObject.name != "MayorsOffice") && (gameObject.name != "maymeasures")
                    && (gameObject.name != "mayvariables"))
        {
            GameObject[] departarray = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var department in departarray)
            {
                if (department.name == "DepartMenu")
                {
                    GameObject DepartMenu = department.gameObject;
                    DepartMenu.gameObject.SetActive(true);
                }
            }
            liststring = new List<string>();
            if (Gm.name == "Education")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Education education = tempcanvas.GetComponent<Education>();
                liststring = education.GetList();
                membervalues = new float[7];
                membervalues[0] = education._budget;
                membervalues[1] = education._personnel;
                membervalues[2] = education._efficiency;
                membervalues[3] = education._elementary_education;
                membervalues[4] = education._middle_education;
                membervalues[5] = education._higher_education;
                membervalues[6] = education._illiteracy_percent;
            }
            else if (Gm.name == "Fire")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Fire fire = tempcanvas.GetComponent<Fire>();
                liststring = fire.GetList();
                membervalues = new float[4];
                membervalues[0] = fire._budget;
                membervalues[1] = fire._personnel;
                membervalues[2] = fire._efficiency;
                membervalues[3] = fire._equipment;
            }
            else if (Gm.name == "Health")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Health health = tempcanvas.GetComponent<Health>();
                liststring = health.GetList();
                membervalues = new float[5];
                membervalues[0] = health._budget;
                membervalues[1] = health._personnel;
                membervalues[2] = health._efficiency;
                membervalues[3] = health._corruption;
                membervalues[4] = health._hospitals;
            }
            else if (Gm.name == "Housing")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Housing housing = tempcanvas.GetComponent<Housing>();
                liststring = housing.GetList();
                membervalues = new float[7];
                membervalues[0] = housing._budget;
                membervalues[1] = housing._personnel;
                membervalues[2] = housing._efficiency;
                membervalues[3] = housing._corruption;
                membervalues[4] = housing._total_housing;
                membervalues[5] = housing._new_housing;
                membervalues[6] = housing._housingneed;
            }
            else if (Gm.name == "Justice")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Justice justice = tempcanvas.GetComponent<Justice>();
                liststring = justice.GetList();
                membervalues = new float[7];
                membervalues[0] = justice._budget;
                membervalues[1] = justice._personnel;
                membervalues[2] = justice._efficiency;
                membervalues[3] = justice._corruption;
                membervalues[4] = justice._municipalcourts;
                membervalues[5] = justice._courts;
                membervalues[6] = justice._prisons;
            }
            else if (Gm.name == "Parks")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Parks parks = tempcanvas.GetComponent<Parks>();
                liststring = parks.GetList();
                membervalues = new float[5];
                membervalues[0] = parks._budget;
                membervalues[1] = parks._personnel;
                membervalues[2] = parks._efficiency;
                membervalues[3] = parks._parks;
                membervalues[4] = parks._equipment;
            }
            else if (Gm.name == "Police")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Police police = tempcanvas.GetComponent<Police>();
                liststring = police.GetList();
                membervalues = new float[5];
                membervalues[0] = police._budget;
                membervalues[1] = police._personnel;
                membervalues[2] = police._efficiency;
                membervalues[3] = police._corruption;
                membervalues[4] = police._equipment;
            }
            else if (Gm.name == "Publicworks")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Publicworks publicworks = tempcanvas.GetComponent<Publicworks>();
                liststring = publicworks.GetList();
                membervalues = new float[6];
                membervalues[0] = publicworks._budget;
                membervalues[1] = publicworks._personnel;
                membervalues[2] = publicworks._efficiency;
                membervalues[3] = publicworks._corruption;
                membervalues[4] = publicworks._maintenance_streets;
                membervalues[5] = publicworks._maintenance_sewers;
            }
            else if (Gm.name == "Revenue")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Revenue revenue = tempcanvas.GetComponent<Revenue>();
                liststring = revenue.GetList();
                membervalues = new float[6];
                membervalues[0] = revenue._budget;
                membervalues[1] = revenue._personnel;
                membervalues[2] = revenue._efficiency;
                membervalues[3] = revenue._corruption;
                membervalues[4] = revenue._taxes_collected;
                membervalues[5] = revenue._tax_level;
            }
            else if (Gm.name == "Transportation")
            {
                GameObject tempcanvas = GameObject.Find("Canvas");
                Transportation transportation = tempcanvas.GetComponent<Transportation>();
                liststring = transportation.GetList();
                membervalues = new float[6];
                membervalues[0] = transportation._budget;
                membervalues[1] = transportation._personnel;
                membervalues[2] = transportation._efficiency;
                membervalues[3] = transportation._corruption;
                membervalues[4] = transportation._public_transport;
                membervalues[5] = transportation._road_maintenance;
            }

            GameObject departmentname =
            DepartMenu.transform.Find("departmentname").gameObject;
            TextMeshProUGUI departmenttext =
            departmentname.GetComponent<TextMeshProUGUI>();
            departmenttext.text = Gm.name + " Department";
            GameObject OwnFactionText = GameObject.Find("departmentname");
            GameController gamecontroller = GetComponent<GameController>();
            foreach (string item in liststring)
            {
                //create departmentvariables and slider game objects
                GameObject depvar = new GameObject();
                GameObject depvarmemberval = new GameObject();
                GameObject depvarSliderHolder = new GameObject();
                GameObject depvarSlider = new GameObject();
                GameObject depvarSliderHolderText = new GameObject();
                GameObject Background = new GameObject();
                GameObject FillArea = new GameObject();
                GameObject Fill = new GameObject();
                GameObject HandleSlideArea = new GameObject();
                GameObject Handle = new GameObject();

                GameObject departmentvariables = GameObject.Find("departmentvariables");
                depvar.transform.SetParent(departmentvariables.transform);
                depvarmemberval.transform.SetParent(depvar.transform);
                depvarSliderHolder.transform.SetParent(departmentvariables.transform);
                depvarSlider.transform.SetParent(depvarSliderHolder.transform);
                Background.transform.SetParent(depvarSlider.transform);
                FillArea.transform.SetParent(depvarSlider.transform);
                Fill.transform.SetParent(FillArea.transform);
                HandleSlideArea.transform.SetParent(depvarSlider.transform);
                Handle.transform.SetParent(HandleSlideArea.transform);
                depvarSliderHolderText.transform.SetParent(depvarSliderHolder.transform);


                var depvartransform = depvar.AddComponent<RectTransform>();
                var depvarmembervaltransform = depvarmemberval.AddComponent<RectTransform>();
                var depvarsliderHoldertransform = depvarSliderHolder.AddComponent<RectTransform>();
                var depvarSlidertransform = depvarSlider.AddComponent<RectTransform>();
                var Backgroundtransform = Background.AddComponent<RectTransform>();
                var FillAreatransform = FillArea.AddComponent<RectTransform>();
                var Filltransform = Fill.AddComponent<RectTransform>();
                var HandleSlideAreatransform = HandleSlideArea.AddComponent<RectTransform>();
                var Handletransform = Handle.AddComponent<RectTransform>();
                var depvarSliderHolderTexttransform = depvarSliderHolderText.AddComponent<RectTransform>();


                depvartransform.anchorMin = new Vector2(0, 1);
                depvartransform.anchorMax = new Vector2(0, 1);
                depvartransform.pivot = new Vector2(0, 1);

                depvarmembervaltransform.anchorMin = new Vector2(0, 1);
                depvarmembervaltransform.anchorMax = new Vector2(0, 1);
                depvarmembervaltransform.pivot = new Vector2(0, 1);

                depvarsliderHoldertransform.anchorMin = new Vector2(0, 1);
                depvarsliderHoldertransform.anchorMax = new Vector2(0, 1);
                depvarsliderHoldertransform.pivot = new Vector2(0, 1);

                depvarSlidertransform.anchorMin = new Vector2(0, 1);
                depvarSlidertransform.anchorMax = new Vector2(0, 1);
                depvarSlidertransform.pivot = new Vector2(0, 1);

                Backgroundtransform.anchorMin = new Vector2(0, 0);
                Backgroundtransform.anchorMax = new Vector2(1, 1);
                Backgroundtransform.pivot = new Vector2(0.5f, 0.5f);

                FillAreatransform.anchorMin = new Vector2(0, 0);
                FillAreatransform.anchorMax = new Vector2(1, 1);
                FillAreatransform.pivot = new Vector2(0.5f, 0.5f);

                Filltransform.anchorMin = new Vector2(0, 0);
                Filltransform.anchorMax = new Vector2(1, 1);
                Filltransform.pivot = new Vector2(0.5f, 0.5f);

                HandleSlideAreatransform.anchorMin = new Vector2(0, 0);
                HandleSlideAreatransform.anchorMax = new Vector2(1, 1);
                HandleSlideAreatransform.pivot = new Vector2(0.5f, 0.5f);

                Handletransform.anchorMin = new Vector2(0, 1);
                Handletransform.anchorMax = new Vector2(0, 1);
                Handletransform.pivot = new Vector2(0, 1);

                depvarSliderHolderTexttransform.anchorMin = new Vector2(0, 1);
                depvarSliderHolderTexttransform.anchorMax = new Vector2(0, 1);
                depvarSliderHolderTexttransform.pivot = new Vector2(0, 1);

                depvar.name = item;
                depvarmemberval.name = item + "value";
                depvarSliderHolder.name = item + "sliderholder";
                depvarSlider.name = item + "slider";
                Background.name = "Background";
                FillArea.name = "FillArea";
                Fill.name = "Fill";
                HandleSlideArea.name = "HandleSlideArea";
                Handle.name = "Handle";

                depvartransform.localPosition = new Vector2(0, j);
                depvarmembervaltransform.localPosition = new Vector2(0, 0);
                depvarsliderHoldertransform.localPosition = new Vector2(320, j);
                depvarSlidertransform.localPosition = new Vector2(0, 0);
                depvarSliderHolderTexttransform.localPosition = new Vector2(0, -15);

                var depvarmembervaltext = depvarmemberval.AddComponent<TextMeshProUGUI>();
                if (depvar.name == "budget" || depvar.name == "personnel" || depvar.name == "elementary education" 
                    || depvar.name == "middle education" || depvar.name == "higher education" 
                    || depvar.name == " hospitals" || depvar.name == "municipal courts" 
                    || depvar.name == "courts" 
                    || depvar.name == "prisons" || depvar.name == "parks" || depvar.name == "equipment" 
                    || depvar.name == "maintenance streets" || depvar.name == "maintenance sewers"
                    || depvar.name == "taxes collected" || depvar.name == "public transport" 
                    || depvar.name == "road maintenance")
                {
                    depvarmembervaltext.text = (membervalues[l] / 1000000).ToString("F0") + " M";
                }
                else
                {
                    depvarmembervaltext.text = (membervalues[l].ToString());
                }
                depvarSlider.AddComponent<Slider>();
                Background.AddComponent<Image>();
                var Backgroundimage = Background.GetComponent<Image>();
                Backgroundimage.sprite = (Sprite)Resources.Load("Assets/background");
                Handle.AddComponent<Image>();
                var Handleimage = Handle.GetComponent<Image>();
                Handleimage.sprite = Backgroundimage.sprite;
                Handleimage.color = new Color(0x00, 0x00, 0x00);
                depvarSlider.GetComponent<Slider>().targetGraphic = Handle.GetComponent<Image>();
                depvarSlider.GetComponent<Slider>().fillRect = Fill.GetComponent<RectTransform>();
                depvarSlider.GetComponent<Slider>().handleRect = Handle.GetComponent<RectTransform>();
                depvarSlider.GetComponent<Slider>().minValue = 0;
                depvarSlider.GetComponent<Slider>().maxValue = 200;
                depvarSlider.GetComponent<Slider>().value = 100;
                var depText = depvarSliderHolderText.AddComponent<TextMeshProUGUI>();
                depText.text = "100 %";
                tempmembervalues = new float[2];
                depvarSlider.GetComponent<Slider>().onValueChanged.AddListener((v) =>
                {
                    depText.text = v.ToString() + " %";
                    var depname = Gm.name;
                    
                    if (item == "budget")
                    {
                        depvarmembervaltext.text = (v / 100 * membervalues[0] / 1000000).ToString("F0") + " M";
                        tempmembervalues[0] = (v / 100 * membervalues[0]);
                    }
                    if (item == "personnel")
                    {
                        depvarmembervaltext.text = (v / 100 * membervalues[1] / 1000000).ToString("F0") + " M";
                        tempmembervalues[1] = (v / 100 * membervalues[1]);
                    }
                });

                j -= 30;
                i = 30;
                k++;
                l++;
                TextMeshProUGUI depvartext = depvar.AddComponent<TextMeshProUGUI>();
                depvartext.text = depvar.name;

                var depvary = depvartransform.sizeDelta.y;
                var depvarx = depvartransform.sizeDelta.x;
                var depvarmembervaly = depvarmembervaltransform.sizeDelta.y;
                var depvarmembervalx = depvarmembervaltransform.sizeDelta.x;
                var depvarSliderHoldery = depvarsliderHoldertransform.sizeDelta.y;
                var depvarSliderHolderx = depvarsliderHoldertransform.sizeDelta.x;
                var depvarSlidery = depvarSlidertransform.sizeDelta.y;
                var depvarSliderx = depvarSlidertransform.sizeDelta.x;
                var Handley = Handletransform.sizeDelta.y;
                var Handlex = Handletransform.sizeDelta.x;
                var depvarSliderHolderTexty = depvarSliderHolderTexttransform.sizeDelta.y;
                var depvarSliderHolderTextx = depvarSliderHolderTexttransform.sizeDelta.x;

                depvary = i;
                depvarx = 240;
                depvarmembervaly = i;
                depvarmembervalx = 80;
                depvarSliderHoldery = i;
                depvarSliderHolderx = 160;
                depvarSlidery = i / 2;
                depvarSliderx = 160;
                depvarmembervaltransform.offsetMin = new Vector2(240, 0);
                depvarmembervaltransform.offsetMax = new Vector2(0, 0);
                Backgroundtransform.offsetMin = new Vector2(0, 0);
                Backgroundtransform.offsetMax = new Vector2(0, 0);
                FillAreatransform.offsetMin = new Vector2(0, 0);
                FillAreatransform.offsetMax = new Vector2(0, 0);
                Filltransform.offsetMin = new Vector2(0, 0);
                Filltransform.offsetMax = new Vector2(0, 0);
                HandleSlideAreatransform.offsetMin = new Vector2(0, 0);
                HandleSlideAreatransform.offsetMax = new Vector2(-5, 0);
                Handley = 5;
                Handlex = 5;
                depvarSliderHolderTexty = i / 2;
                depvarSliderHolderTextx = 160;

                depvartransform.sizeDelta = new Vector2(depvarx, depvary);
                depvarmembervaltransform.sizeDelta = new Vector2(depvarmembervalx, depvarmembervaly);
                depvarsliderHoldertransform.sizeDelta = new Vector2(depvarSliderHolderx, depvarSliderHoldery);
                depvarSlidertransform.sizeDelta = new Vector2(depvarSliderx, depvarSlidery);
                depvarSliderHolderTexttransform.sizeDelta = new Vector2(depvarSliderHolderTextx, depvarSliderHolderTexty);

                Handletransform.sizeDelta = new Vector2(Handlex, Handley);
                depvartext.fontSize = 21;
                depText.fontSize = 12;
                depText.color = Color.black;
                depText.alignment = TextAlignmentOptions.Center;
                depvarmembervaltext.fontSize = 12;
                depvarmembervaltext.color = Color.black;
                depvarmembervaltext.alignment = TextAlignmentOptions.Center;
                if (item == "budget" || item == "personnel")
                {
                    depvarSlider.GetComponent<Slider>().enabled = true;
                }
                else
                {
                    depvarSlider.GetComponent<Slider>().enabled = false;
                }
            }
        }
    }
    
    public void destroyvariables()
    {
        foreach (string item in liststring)
        {
            GameObject destroy = GameObject.Find(item);
            string itemvalue = item.ToString() + "value";
            GameObject destroyvalue = GameObject.Find(itemvalue);
            string itemslider = item.ToString() + "sliderholder";
            GameObject destroyslider = GameObject.Find(itemslider);
            DestroyImmediate(destroyslider.gameObject);
            DestroyImmediate(destroyvalue.gameObject);
            DestroyImmediate(destroy.gameObject);
        }
        liststring = null;
        departmentvariables = false;
    }

    public void destroymayvariables()
    {
        foreach (string item in liststring)
        {
            GameObject destroy = GameObject.Find(item);
            string itemvalue = item.ToString() + "value";
            GameObject destroyvalue = GameObject.Find(itemvalue);
            string itemslider = item.ToString() + "sliderholder";
            GameObject destroyslider = GameObject.Find(itemslider);
            DestroyImmediate(destroyslider.gameObject);
            DestroyImmediate(destroyvalue.gameObject);
            DestroyImmediate(destroy.gameObject);
        }
        liststring = null;
        mayorvariables = false;
    }

    public void destroymaymeasures()
    {
        foreach (Option.option option in mayOptions)
        {
            GameObject destroy = GameObject.Find(option.integergm.ToString());
            Destroy(destroy.gameObject);
        }
        mayOptions = null;
        mayormeasures = false;
    }

    public void MayorSetValues()
    {
        GameObject DepartmentName = GameObject.Find("mayorname");
        GameObject tempcanvas = GameObject.Find("Canvas");
        MayorOffice mayorOffice = tempcanvas.GetComponent<MayorOffice>();
        if (tempmembervalues[0] != 0)
        {mayorOffice._budget = tempmembervalues[0];}
        if (tempmembervalues[1] != 0)
        { mayorOffice._personnel = tempmembervalues[1];}
        
        mayorOffice._efficiency = membervalues[2];
        mayorOffice._tech_level = membervalues[3];
        mayorOffice._quality_department_heads = membervalues[4];
    }

    public void Setvalues()
    {

        GameObject DepartmentName = GameObject.Find("departmentname");
        TextMeshProUGUI DepartmentText = DepartmentName.GetComponent<TextMeshProUGUI>();
        if (DepartmentText.text == "Education Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Education education = tempcanvas.GetComponent<Education>();
            if (tempmembervalues[0] != 0)
            { education._budget = tempmembervalues[0];}
            if (tempmembervalues[1] != 0)
            { education._personnel = tempmembervalues[1];}
            education._efficiency = membervalues[2];
            education._elementary_education = membervalues[3];
            education._middle_education = membervalues[4];
            education._higher_education = membervalues[5];
            education._illiteracy_percent = membervalues[6];
        }
        else if (DepartmentText.text == "Fire Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Fire fire = tempcanvas.GetComponent<Fire>();
            if (tempmembervalues[0] != 0)
            {fire._budget = tempmembervalues[0];}
            if (tempmembervalues[1] != 0)
            {fire._personnel = tempmembervalues[1]; }
            fire._efficiency = membervalues[2];
            fire._equipment = membervalues[3];
        }
        else if (DepartmentText.text == "Health Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Health health = tempcanvas.GetComponent<Health>();
            if (tempmembervalues[0] != 0) 
            {health._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { health._personnel = tempmembervalues[1]; }
            health._efficiency = membervalues[2];
            health._corruption = membervalues[3];
            health._hospitals = membervalues[4];
        }
        else if (DepartmentText.text == "Housing Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Housing housing = tempcanvas.GetComponent<Housing>();
            if (tempmembervalues[0] != 0)
            { housing._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { housing._personnel = tempmembervalues[1]; }
            housing._efficiency = membervalues[2];
            housing._corruption = membervalues[3];
            housing._total_housing = membervalues[4];
            housing._new_housing = membervalues[5];
            housing._housingneed = membervalues[6];
        }
        else if (DepartmentText.text == "Justice Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Justice justice = tempcanvas.GetComponent<Justice>();
            if (tempmembervalues[0] != 0)
            { justice._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { justice._personnel = tempmembervalues[1]; }
            justice._efficiency = membervalues[2];
            justice._corruption = membervalues[3];
            justice._municipalcourts = membervalues[4];
            justice._courts = membervalues[5];
            justice._prisons = membervalues[6];
        }
        else if (DepartmentText.text == "Parks Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Parks parks = tempcanvas.GetComponent<Parks>();
            if (tempmembervalues[0] != 0)
            { parks._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { parks._personnel = tempmembervalues[1]; }
            parks._efficiency = membervalues[2];
            parks._parks = (int)membervalues[3];
            parks._equipment = membervalues[4];
        }
        else if (DepartmentText.text == "Police Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Police police = tempcanvas.GetComponent<Police>();
            if (tempmembervalues[0] != 0)
            { police._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { police._personnel = tempmembervalues[1]; }
            police._efficiency = membervalues[2]; 
            police._corruption = membervalues[3];
            police._equipment = membervalues[4];
        }
        else if (DepartmentText.text == "Publicworks Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Publicworks publicworks = tempcanvas.GetComponent<Publicworks>();
            if (tempmembervalues[0] != 0)
            { publicworks._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { publicworks._personnel = tempmembervalues[1]; }
            publicworks._efficiency = membervalues[2];
            publicworks._corruption = membervalues[3];
            publicworks._maintenance_streets = membervalues[4];
            publicworks._maintenance_sewers = membervalues[5];
        }
        else if (DepartmentText.text == "Revenue Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Revenue revenue = tempcanvas.GetComponent<Revenue>();
            if (tempmembervalues[0] != 0)
            { revenue._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { revenue._personnel = tempmembervalues[1]; }
            revenue._efficiency = membervalues[2];
            revenue._corruption = membervalues[3];
            revenue._taxes_collected = membervalues[4];
            revenue._tax_level = membervalues[5];
        }
        else if (DepartmentText.text == "Transportation Department")
        {
            GameObject tempcanvas = GameObject.Find("Canvas");
            Transportation transportation = tempcanvas.GetComponent<Transportation>();
            if (tempmembervalues[0] != 0)
            { transportation._budget = tempmembervalues[0]; }
            if (tempmembervalues[1] != 0)
            { transportation._personnel = tempmembervalues[1]; }
            transportation._efficiency = membervalues[2];
            transportation._corruption = membervalues[3];
            transportation._public_transport = membervalues[4];
            transportation._road_maintenance = membervalues[5];
        }
    }

    public Option.option GetmayOption(int number)
    {
        GameController gameController = gameObject.GetComponent<GameController>();
        Option.option tempoption = mayOptions[number - 1];
        gameController.sendmessage = true;
        return tempoption;
    }
}