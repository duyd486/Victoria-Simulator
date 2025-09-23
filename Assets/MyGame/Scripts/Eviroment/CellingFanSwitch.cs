using UnityEngine;

public class CellingFanSwitch : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.TurnCellingFan();
    }
}
