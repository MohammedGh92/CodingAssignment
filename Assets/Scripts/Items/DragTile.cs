using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTile : Tile
{
    protected override void SetMaterial(Material mat)
    {
        base.SetMaterial(mat);
        meshRenderer.material.color = new Color(1, 1, 1, 0.25f);

    }
}