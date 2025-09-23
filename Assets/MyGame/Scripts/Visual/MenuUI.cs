using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance { get; private set; }

    [SerializeField] private Button menuBtn;

    private void Awake()
    {
        Instance = this;

        menuBtn.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        PlayerInteract.Instance.SetInteractable(false);
        PlayerLocomotion.Instance.SetCanMove(false);
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        PlayerInteract.Instance.SetInteractable(true);
        PlayerLocomotion.Instance.SetCanMove(true);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
