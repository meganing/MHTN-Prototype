using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Base class for all user interface behaviours.
/// Use this instead of <see cref="UIBehaviour"/> and <see cref="MonoBehaviour"/> for UI components.
/// </summary>
public abstract class UserInterfaceBehaviour : UIBehaviour
{
    public RectTransform rectTransform => transform is RectTransform rectTransform ? rectTransform : null;
}
