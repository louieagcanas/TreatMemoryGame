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

    private event Action ConfirmCallback;
    private event Action DeclineCallback;

    public void OpenPopupWindow(string message, string confirmText, string declineText, 
                                   Action confirmCallback = null, Action declineCallback = null )
    {
        gameObject.SetActive(true);

        popupMessage.text = message;
        confirmButtonText.text = confirmText;
        declineButtonText.text = declineText;

        ConfirmCallback += confirmCallback;
        DeclineCallback += declineCallback;
    }

    public void Confirm()
    {
        Debug.Log($"Confirm!");
        ConfirmCallback?.Invoke();
        gameObject.SetActive(false);
    }

    public void Decline()
    {
        DeclineCallback?.Invoke();
        gameObject.SetActive(false);
    }
}
