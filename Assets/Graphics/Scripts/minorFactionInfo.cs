using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class minorFactionInfo : MonoBehaviour
{
    public TextMeshProUGUI factionName;
    public RectTransform minorfactionscreen;
    public RectTransform canvas;

    Vector2 mousePos;

    void Start()
    {
        HideMinorFactionInfoScreen();
    }

    void OnMouseOver()
    {
        //give minorfactioninfoscreen the name of the gameobject
        
        factionName.text = gameObject.name;
        ShowMinorFactionInfoScreen(factionName.text, mousePos);

    }

    void OnMouseExit()
    {
        HideMinorFactionInfoScreen();
    }

    private void ShowMinorFactionInfoScreen(string Factionname, Vector2 mousePos)
    {
        minorfactionscreen.sizeDelta = new Vector2(factionName.preferredWidth, factionName.preferredHeight);
        factionName.text = Factionname;

        minorfactionscreen.gameObject.SetActive(true);
    }

    private void HideMinorFactionInfoScreen()
    {
        minorfactionscreen.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (minorfactionscreen.gameObject.activeSelf)
        {
            mousePos = Input.mousePosition;
            
            if (mousePos.y - minorfactionscreen.rect.height < 0)
            {
                minorfactionscreen.transform.position = new Vector2(mousePos.x - mousePos.x, mousePos.y - mousePos.y);
            }
            else
            { minorfactionscreen.transform.position = new Vector2(mousePos.x - (minorfactionscreen.rect.width / 2), mousePos.y - (minorfactionscreen.rect.height / 2)); }
        }
    }
}