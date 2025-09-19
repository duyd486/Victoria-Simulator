using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CdPickerUI : MonoBehaviour
{
    public static CdPickerUI Instance {  get; private set; }

    [SerializeField] private Button cancelBtn;
    [SerializeField] private TextMeshProUGUI artistNameTxt;
    [SerializeField] private GameObject cdUIContainer;
    [SerializeField] private CdSingleUI cdUI;


    public event EventHandler OnCancelClick;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hide();
        cancelBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        cdUI.Hide();
    }

    void Update()
    {
        
    }

    public void Show(Artist artist)
    {
        if (artist == null) return;
        PlayerInteract.Instance.SetInteractable(false);
        PlayerLocomotion.Instance.SetCanMove(false);
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
        artistNameTxt.text = artist.artistName;
        foreach(Song song in artist.songs)
        {
            CdSingleUI cdSingleUI = Instantiate(cdUI, cdUIContainer.transform);
            cdSingleUI.UpdateCd(song);
        }
    }
    public void Hide()
    {
        PlayerInteract.Instance.SetInteractable(true);
        PlayerLocomotion.Instance.SetCanMove(true);
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        OnCancelClick?.Invoke(this, EventArgs.Empty);
    }
}
