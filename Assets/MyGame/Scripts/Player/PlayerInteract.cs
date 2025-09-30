using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    [SerializeField] private float maxDistance = 6f;
    [SerializeField] private bool canInteractObject = false;

    [SerializeField] public bool canInteract = true;
    [SerializeField] private Renderer cd;
    [SerializeField] private Transform cdPref;
    [SerializeField] private GameObject LeftHand;
    private GameObject smt;

    public bool isCarryCd = false;
    public bool isCarrySmt = false;

    [SerializeField] private Song song;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideCd();
        GameInput.Instance.OnInteractPress += GameInput_OnInteractPress;
        GameInput.Instance.OnThrowPress += GameInput_OnThrowPress;
    }

    private void GameInput_OnThrowPress(object sender, EventArgs e)
    {
        ThrowObject();
        if (!isCarryCd) return;
        if (song.thumbnail == null) return;
        Transform cdTransform = Instantiate(cdPref, transform);
        cdTransform.GetComponent<Renderer>().material.mainTexture = song.thumbnail;
        cdTransform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000);
        HideCd();
    }

    private void GameInput_OnInteractPress(object sender, EventArgs e)
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) && hit.transform.GetComponentInParent<IInteractable>() != null)
        {
            hit.transform.GetComponentInParent<IInteractable>().Interact();
        }
    }

    private void Update()
    {
        if(canInteract)
        {
            HandleInteractVisual();
        }
    }

    private void HandleInteractVisual()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) && hit.transform.GetComponentInParent<IInteractable>() != null)
        {
            canInteractObject = true;
        } else
        {
            canInteractObject = false;
        }
    }

    public void GrabObject(GameObject smt)
    {
        if (isCarrySmt) return;
        // Object được grab phải là non-static nha
        if(smt.TryGetComponent<Rigidbody>(out Rigidbody component))
        {
            Destroy(component);
        }
        smt.transform.SetParent(LeftHand.transform);
        smt.transform.localPosition = Vector3.zero;
        smt.transform.localRotation = Quaternion.identity;
        this.smt = smt;
        isCarrySmt = true;
    }
    public void GrabPrefab(GameObject pref)
    {
        GameObject newPref = Instantiate(pref);
        GrabObject(newPref);
    }

    public void ThrowObject()
    {
        foreach (Transform child in LeftHand.transform)
        {
            child.SetParent(null);
            isCarrySmt = false;
            Rigidbody rb = child.AddComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * 1000);
        }
    }

    public void SetCd(Song song)
    {
        this.song = song;
        if (song.thumbnail != null)
        {
            ShowCd();
            cd.material.mainTexture = song.thumbnail;
            CdPickerUI.Instance.Hide();
        }
        else
        {
            Debug.LogError("Không tìm thấy ảnh cover");
        }
    }
    public Song GetPlayerCd()
    {
        return song;
    }

    public void SetInteractable(bool canInter)
    {
        canInteract = canInter; 
    }

    public void HideCd()
    {
        cd.gameObject.SetActive(false);
        isCarryCd = false;
    }
    public void ShowCd()
    {
        cd.gameObject.SetActive(true);
        isCarryCd = true;
    }

    public bool GetCanInteract()
    {
        return canInteractObject;
    }
    public bool GetIsCarryCd()
    {
        return isCarryCd;
    }

}
