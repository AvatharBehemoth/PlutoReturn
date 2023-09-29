using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionInfo : MonoBehaviour
{

    public TextMeshProUGUI FactionName;
    public Image FactionFlag;
    public TextMeshProUGUI FactionText;
    
    public RectTransform FactionInfoScreen;

    public static Action<string, Sprite, string, Vector2> onMouseEnter;
    public static Action onMouseExit;

    void Start()
    {
        hideFactionInfoScreen();

    }
    private void OnEnable()
    {
        onMouseEnter += ShowFactionInfoScreen;
        onMouseExit += hideFactionInfoScreen;
    }

    private void OnDisable()
    {
        onMouseEnter -= ShowFactionInfoScreen;
        onMouseExit -= hideFactionInfoScreen;
    }

    private void ShowFactionInfoScreen(string factionname, Sprite factionflag, string factiontext, Vector2 mousePos)
    {
        FactionName.text = factionname;
        FactionFlag.sprite = factionflag;
        FactionText.text = factiontext;
        FactionInfoScreen.gameObject.SetActive(true);
        FactionInfoScreen.sizeDelta = new Vector2(FactionText.preferredWidth , FactionText.preferredHeight);
        FactionInfoScreen.transform.position = new Vector2(mousePos.x + FactionInfoScreen.sizeDelta.x * 2, mousePos.y);

    }

    private void hideFactionInfoScreen()
    {
        FactionName = default;
        FactionText = default;
        FactionFlag = default;
        gameObject.SetActive(false);
    }
}
