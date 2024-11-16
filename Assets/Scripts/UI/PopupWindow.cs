using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI popupMessage;

    [SerializeField]
    private TextMeshProUGUI confirmButtonText;

    [SerializeField]
    private TextMeshProUGUI declineButtonText;

    private Action ConfirmCallback;
    private Action DeclineCallback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopupWindow(string message, string confirmText, string declineText, 
                                   Action confirmCallback = null, Action declineCallback = null )
    {
        popupMessage.text = message;
        confirmButtonText.text = confirmText;
        declineButtonText.text = declineText;

        ConfirmCallback = confirmCallback;
        DeclineCallback = declineCallback;
    }

    public void Confirm()
    {

    }

    public void Decline()
    {

    }
}
