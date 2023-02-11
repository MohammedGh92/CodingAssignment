using UnityEngine;
/// <summary>
/// Extending SubController class with generic reference UI Root.
/// </summary>
public abstract class GameSubController<T> : SubController<T, GameControllerType> where T : UIRoot
{

}