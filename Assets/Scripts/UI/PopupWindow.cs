using System;
using TMPro;
using UnityEngine;

public class PopupWindow : MonoBehaviour, IScreen
{
    [SerializeField]
    private TextMeshProUGUI popupMessage;

    [SerializeField]
    private TextMeshProUGUI confirmButtonText;

    [SerializeField]
    private TextMeshProUGUI declineButtonText;

    private event Action ConfirmCallback;
    private event Action DeclineCallback;

    public void OpenPopupWindow(string message, Action confirmCallback = null, Action declineCallback = null )
    {
        popupMessage.text = message;

        ConfirmCallback = confirmCallback;
        DeclineCallback = declineCallback;

        Show();
    }

    public void Confirm()
    {
        ConfirmCallback?.Invoke();

        ConfirmCallback = null;
        DeclineCallback = null;

        Hide();
    }

    public void Decline()
    {
        DeclineCallback?.Invoke();

        ConfirmCallback = null;
        DeclineCallback = null;

        Hide();
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
