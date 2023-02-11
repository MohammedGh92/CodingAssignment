using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridO
{
    public Dictionary<Tuple<int, int>, GroundTileO> groundTiles;
    public GridO()
    {
        groundTiles = new Dictionary<Tuple<int, int>, GroundTileO>();
    }

    public GridO(Dictionary<Tuple<int, int>, GroundTileO> groundTiles)
    {
        this.groundTiles = groundTiles;
    }

}