using TMPro;
using UnityEngine;

public class MessageBar : MonoBehaviour
{
    [Header("Messagebar Parameters")]
    [SerializeField] private TextMeshProUGUI txt;

    public static MessageBar instance;
    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(instance);
        instance = this;

        CloseMessageBar();
    }

    public void ShowMessage(string msg)
    {
        gameObject.SetActive(true);
        txt.text = msg;
    }

    public void CloseMessageBar() => gameObject.SetActive(false);
}
