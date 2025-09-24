using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.TurnLightSwitch();
        if (GameManager.Instance.lightRoom)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;

        }
    }
}
