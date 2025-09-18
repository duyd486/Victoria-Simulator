using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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

        Sprite thumnailSprite = Sprite.Create( song.thumbnail, new Rect(0, 0, song.thumbnail.width, song.thumbnail.height), new Vector2(0.5f, 0.5f));
        thumbnailBtn.image.sprite = thumnailSprite;

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
