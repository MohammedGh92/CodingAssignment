using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : Tile
{

    public void Activate(TileO tileObj, Material mat)
    {
        base.Activate(mat);
        this.tileObj = tileObj;
    }

    public TileType GetTile()
    {
        return tileObj.GetTileType();
    }

}