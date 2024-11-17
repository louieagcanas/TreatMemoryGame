using System;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour, IScreen
{
    public event Action OnMainMenu;
    public event Action OnRestart;

    [SerializeField]
    private TextMeshProUGUI moveCounter;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private PopupWindow popupWindow;

    public void UpdateMoveCounter(int moves)
    {
        moveCounter.text = moves.ToString();
    }

    public void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString();
    }

    public void TryRestart()
    {
        popupWindow.OpenPopupWindow("You're trying to restart the level. Are you sure you want to proceed?", "Yes", "No", ConfirmRestart, null);
    }

    public void TryReturnToMainMenu()
    {
        popupWindow.OpenPopupWindow("You're trying to return to the Main Menu. Are you sure you want to proceed?", "Yes", "No", ConfirmReturnToMainMenu, null);
    }

    public void ConfirmReturnToMainMenu()
    {
        OnMainMenu?.Invoke();
    }

    private void ConfirmRestart()
    {
        OnRestart?.Invoke();
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
