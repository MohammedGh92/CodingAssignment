using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    protected TileO tileObj;

    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual void Activate(Material mat)
    {
        meshRenderer.enabled = true;
        SetMaterial(mat);
    }

    public virtual void DisActivate()
    {
        meshRenderer.enabled = false;
    }

    protected virtual void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }

    public virtual void SetPos(Vector3 vector3)
    {
        transform.position = vector3;
    }
}
