using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CdSingleUI : MonoBehaviour
{
    [SerializeField] private Button thumbnailBtn;
    [SerializeField] private TextMeshProUGUI songNameTxt;
    [SerializeField] private Song song;

    private void Start()
    {
        thumbnailBtn.onClick.AddListener(() =>
        {
            PlayerInteract.Instance.SetCd(song);
        });

        CdPickerUI.Instance.OnCancelClick += CdPickUI_OnCancelClick;
    }

    private void CdPickUI_OnCancelClick(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void UpdateCd(Song song)
    {
        if (song == null) return;
        this.song = song;
        songNameTxt.text = song.songName;
        Show();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
