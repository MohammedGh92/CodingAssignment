using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : Singleton<InventoryManager>
{
    public int[] defaultTilesNum;
    private InventoryO inventory;
    [SerializeField]
    private UnityEvent<int> tileNumIncreased;
    [SerializeField]
    private UnityEvent<int> tileNumDecreased;

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void LoadData()
    {
        LoadDefaultData();
    }

    private void LoadDefaultData()
    {
        inventory = new InventoryO(3);
        for (int i = 0; i < defaultTilesNum.Length; i++)
            inventory.tilesNums[i] = defaultTilesNum[i];
    }

    public int GetTileNum(int tileNu)
    {
        return inventory.tilesNums[tileNu];
    }

    public void IncreaseTileNum(int tileNu)
    {
        inventory.tilesNums[tileNu] += 1;
        tileNumIncreased.Invoke(tileNu);
    }

    public void DecreaseTileNum(int tileNu)
    {
        inventory.tilesNums[tileNu] -= 1;
        tileNumDecreased.Invoke(tileNu);
    }
}