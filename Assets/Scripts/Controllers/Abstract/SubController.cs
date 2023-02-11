using UnityEngine;

/// <summary>
/// Base class for SubControllers with reference to Root Controller.
/// </summary>
public abstract class SubController<U> : Appwindow
{
    [HideInInspector]
    public RootController<U> root;

    /// <summary>
    /// Method used to engage controller.
    /// </summary>
    public virtual void EngageController()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Method used to disengage controller.
    /// </summary>
    public virtual void DisengageController()
    {
        gameObject.SetActive(false);
    }
}

/// <summary>
/// Extending SubController class with generic reference UI Root.
/// </summary>
public abstract class SubController<T, U> : SubController<U> where T : UIRoot
{
    [SerializeField]
    protected T ui;
    public T UI => ui;
    protected bool onEngageOnce;

    public override void EngageController()
    {
        base.EngageController();

        ui.ShowRoot();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        ui.HideRoot();
    }
}