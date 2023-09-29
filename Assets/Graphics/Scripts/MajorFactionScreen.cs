using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MajorFactionScreen : MonoBehaviour
{

    public TextMeshProUGUI factionName;
    public Image factionflag;
    public TextMeshProUGUI factiontext;

    public RectTransform InfoFactionText;

    public static Action<string, Sprite, string, Vector2> onMouseEnter;
    public static Action onMouseExit;
    public RectTransform SelectionUIrect;

    void Start()
    {
        HideFactionInfoScreen();

    }
    private void OnEnable()
    {
        onMouseEnter += ShowFactionInfoScreen;
        onMouseExit += HideFactionInfoScreen;
    }

    private void OnDisable()
    {
        onMouseEnter -= ShowFactionInfoScreen;
        onMouseExit -= HideFactionInfoScreen;
    }

    private void ShowFactionInfoScreen(string Factionname, Sprite Factionflag, string FactionText, Vector2 mousePos)
    {
        factionName.text = Factionname;
        factionflag.sprite = Factionflag;
        factiontext.text = FactionText;

        InfoFactionText.gameObject.SetActive(true);

    }

    private void HideFactionInfoScreen()
    {
        InfoFactionText.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (InfoFactionText.gameObject.activeSelf)
        {
            Vector2 mousePos = Input.mousePosition;
            if ((mousePos.y - InfoFactionText.rect.height) < 0)
            {
                InfoFactionText.transform.position = new Vector2(mousePos.x - InfoFactionText.rect.width / 2, mousePos.y + InfoFactionText.rect.height / 2);
            }
            if (mousePos.y + InfoFactionText.rect.height > Screen.height)
            {
                InfoFactionText.transform.position = new Vector2(mousePos.x - InfoFactionText.rect.width / 2, mousePos.y - InfoFactionText.rect.height / 2);
            }
            else
            {
                InfoFactionText.transform.position = new Vector2(mousePos.x, mousePos.y); 
            }
        }
    }
}
