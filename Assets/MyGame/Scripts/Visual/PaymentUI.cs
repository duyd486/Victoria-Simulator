using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PaymentUI : MonoBehaviour
{
    [SerializeField] private Button cancelBtn;
    [SerializeField] private Image QrImg;
    [SerializeField] private GameObject successUI;
    [SerializeField] private Sprite loadingSprite;

    private RootPayment rootPayment;
    private bool isWaittingPayment = false;
    private float paymentTimer = 5f;



    private void Awake()
    {
        successUI.SetActive(false);
        cancelBtn.onClick.AddListener(() =>
        {
            StopPayment();
        });
    }

    private void Start()
    {
        Hide();
        CdPickerUI.Instance.OnBuyClick += CdPickerUI_OnBuyClick;
    }

    private void Update()
    {
        if (isWaittingPayment)
        {
            paymentTimer -= Time.deltaTime;
            if(paymentTimer < 0)
            {
                PaymentHeartBeat();
                paymentTimer = 4f;
            }
        }
    }


    private void CdPickerUI_OnBuyClick(object sender, CdPickerUI.OnBuyClickEventArgs e)
    {
        successUI.SetActive(false);
        Show();
        QrImg.sprite = loadingSprite;
        StartCoroutine(ApiManager.Instance.PostPayment(e.song, SetPaymentQr));
    }

    public void SetPaymentQr(RootPayment root)
    {
        rootPayment = root;
        StartCoroutine(ApiManager.Instance.GetTextureFromURL(root.data.code_url, (Texture2D texture) =>
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            QrImg.sprite = sprite;
            isWaittingPayment = true;
        }));
    }

    public IEnumerator PaymentSuccess()
    {
        isWaittingPayment = false;
        successUI.SetActive(true);
        yield return new WaitForSeconds(2);
        Hide();
    }

    public void StopPayment()
    {
        isWaittingPayment = false;
        Hide();
    }

    public void PaymentHeartBeat()
    {
        Debug.Log("Payment Beating");
        StartCoroutine(ApiManager.Instance.HandlePaymentHeartBeat(rootPayment.data.order_code.ToString(), () => {
            StartCoroutine(PaymentSuccess());
        }));
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
