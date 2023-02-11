using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayController : GameSubController<UIGamePlayRoot>
{

    [SerializeField]
    private UnityEvent<int> itemDraged;
    private InventoryManager inventoryManager;

    public override void EngageController()
    {
        ui.GamePlayView.OnMenuItemClicked += MenuItemClicked;
        base.EngageController();
        onEngage();
    }

    public override void DisengageController()
    {
        base.DisengageController();
        ui.GamePlayView.OnMenuItemClicked -= MenuItemClicked;
        onDisEngage();
    }

    private void onEngage()
    {
        SetRefs();
        LoadTilesNums();
    }

    private void LoadTilesNums()
    {
        for (int i = 0; i < ui.GamePlayView.tilesNumsTxts.Length; i++)
        {
            int cTileNum = inventoryManager.GetTileNum(i);
            ui.GamePlayView.tilesNumsTxts[i].text = cTileNum.ToString("00");
            SetTileAvailability(i, cTileNum);
        }
    }

    private void SetTileAvailability(int tileIndx, int tilesNums)
    {
        ui.GamePlayView.tilesEventTriggers[tileIndx].enabled = tilesNums > 0;
    }

    private void SetRefs()
    {
        if (inventoryManager == null)
            inventoryManager = InventoryManager.Instance;
    }

    public void UpdateInventoryNum(int tileNum)
    {
        ui.GamePlayView.tilesNumsTxts[tileNum].text = inventoryManager.GetTileNum(tileNum).ToString("00");
        SetTileAvailability(tileNum, inventoryManager.GetTileNum(tileNum));
    }

    private void MenuItemClicked(int itemNu)
    {
        itemDraged.Invoke(itemNu);
    }

    private void onDisEngage()
    {

    }

    public override void Close()
    {
        base.Close();
    }
}