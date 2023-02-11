using System;
using System.Collections.Generic;

[Serializable]
public class InventoryO
{
    public int[] tilesNums;
    public InventoryO(int tilesNum)
    {
        tilesNums = new int[tilesNum];
    }
}