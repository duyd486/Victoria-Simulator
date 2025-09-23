using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.TurnLightSwitch();
    }
}
