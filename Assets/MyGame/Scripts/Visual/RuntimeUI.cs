using System.Collections;
using TMPro;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    public static RuntimeUI Instance { get; private set; }

    [SerializeField] private GameObject messageContainer;
    [SerializeField] private MessageSingleUI messageSingleUI;

    private void Awake()
    {
        Instance = this;
    }




    //private void Start()
    //{
    //    messageUI.SetActive(false);
    //}

    public void PushMessage(string message, bool isError)
    {
        MessageSingleUI messageSingleUITmp = Instantiate(messageSingleUI, messageContainer.transform);
        messageSingleUITmp.SetMessage(message, isError);
    }

    //private IEnumerator ShowMessage(string message)
    //{
    //    messageText.text = message;
    //    messageUI.SetActive(true);
    //    yield return new WaitForSeconds(1);
    //    messageUI.SetActive(false);
    //}
}
