using System;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public event Action OnGameRestart;

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

    private void ConfirmRestart()
    {
        OnGameRestart?.Invoke();
    }

    public void ReturnToMainMenu()
    {

    }
}
