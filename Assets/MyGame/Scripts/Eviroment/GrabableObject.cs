using UnityEngine;

public class GrabableObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerInteract.Instance.GrabObject(this.gameObject);
    }
}