using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultScreen : MonoBehaviour, IScreen
{
    public event Action OnMainMenu;
    public event Action OnRestart;

    [SerializeField]
    private Image resultsOverlay;

    [SerializeField]
    private Sprite winOverlay;

    [SerializeField]
    private Sprite loseOverlay;

    [SerializeField]
    private TextMeshProUGUI totalMovesText;

    [SerializeField]
    private float slideInYPosition = 250.0f;

    [SerializeField]
    private float slideInDuration = 0.3f;

    public void ShowWinResult(int totalMoves)
    {
        SlideIn();
        resultsOverlay.sprite = winOverlay;
        totalMovesText.text = $"Total Moves: {totalMoves}";
        totalMovesText.gameObject.SetActive(true);
        Show();
    }

    public void ShowLoseResult()
    {
        SlideIn();
        resultsOverlay.sprite = loseOverlay;
        totalMovesText.gameObject.SetActive(false);
        Show();
    }

    private void SlideIn()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(0.0f, 1300.0f);
        rectTransform.DOLocalMoveY(slideInYPosition, slideInDuration).SetEase(Ease.InSine).SetDelay(0.3f);
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Hide();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
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
