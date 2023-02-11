using UnityEngine;

public abstract class Appwindow : MonoBehaviour
{
    protected GameObject ContainerGO;

    protected virtual void Awake()
    {
        ContainerGO = transform.GetChild(0).gameObject;
    }

    public virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public virtual bool IsContActive()
    {
        return ContainerGO.activeSelf;
    }

    public virtual void Close()
    {

    }
}