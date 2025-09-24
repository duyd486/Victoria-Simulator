using UnityEngine;

public class HopGiayAn : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (!PlayerInteract.Instance.isCarryHopGiayAn)
        {
            transform.SetParent(Camera.main.transform);
            transform.localPosition = new Vector3(-0.83f, -0.33f, 1.5f);
        }
        PlayerInteract.Instance.isCarryHopGiayAn = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
