using UnityEngine;

/// <summary>
/// Root controller responsible for changing game phases with SubControllers.
/// </summary>
public abstract class RootController<T> : MonoBehaviour
{
    /// <summary>
    /// Method used by subcontrollers to change game phase.
    /// </summary>
    /// <param name="controller">Controller type.</param>
    public abstract void ChangeController(T controller);

    /// <summary>
    /// Method used to disable all attached subcontrollers.
    /// </summary>
    public abstract void DisengageControllers();

    /// <summary>
    /// Method used when click on device back button clicked
    /// </summary>
    public virtual void BackBTNClicked()
    {
        foreach (Appwindow appwindow in windows)
        {
            if (appwindow.IsActive())
            {
                appwindow.Close();
                return;
            }
        }
    }

    [SerializeField]
    protected Appwindow[] windows;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackBTNClicked();
    }
}