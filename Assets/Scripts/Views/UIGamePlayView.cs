using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGamePlayView : UIViews
{

    public UnityAction<int> OnMenuItemClicked;
    public Text[] tilesNumsTxts;
    public EventTrigger[] tilesEventTriggers;

    public void MenuItemClicked(int itemNu)
    {
        OnMenuItemClicked?.Invoke(itemNu);
    }

}