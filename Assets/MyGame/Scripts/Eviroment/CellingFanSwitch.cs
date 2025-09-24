using UnityEngine;

public class CellingFanSwitch : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.TurnCellingFan();
        if (GameManager.Instance.isCellingFanRotate)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;

        }
    }
}
