using System;
using System.Collections;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField]
    private CanvasManager canvasManager;

    [SerializeField]
    private GameHUD gameHUD;

    [SerializeField]
    private ResultScreen resultScreen;

    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private LevelTimers levelTimers;
    private float timer;

    private int totalMoves = 0;
    private int guessCounter = 0;
    private int pairCounter = 0;
    private int pairTotalNeeded;

    private string firstCardName;
    private string secondCardName;

    private Coroutine timerCoroutine;

    private void Awake()
    {
        gameHUD.OnMainMenu += ReturnToMainMenu;
        gameHUD.OnRestart += RestartGame;

        boardManager.OnCardSelected += CardSelected;

        resultScreen.OnMainMenu += ReturnToMainMenu;
        resultScreen.OnRestart += RestartGame;
    }

    public void StartGame()
    {
        Reset();

        timer = levelTimers.Timers[GameSettings.GetDifficultyLevel() - 1];
        pairTotalNeeded = GameSettings.GetDifficultyLevel() + 1;
        boardManager.Initialize(GameSettings.GetDifficultyLevel());

        StartTimer();
    }

    public void RestartGame()
    {
        gameHUD.Show();
        Reset();
        StartGame();
    }

    private void Reset()
    {
        totalMoves = 0;
        gameHUD.UpdateMoveCounter(totalMoves);

        guessCounter = 0;
        pairCounter = 0;

        firstCardName = string.Empty;
        secondCardName = string.Empty;

        StopTimer();
    }

    private void CardSelected(string cardName)
    {
        totalMoves++;
        gameHUD.UpdateMoveCounter(totalMoves);

        guessCounter++;
         
        if(guessCounter == 1)
        {
            firstCardName = cardName;
        }
        else if (guessCounter == 2)
        {
            secondCardName = cardName;
            guessCounter = 0;

            Debug.Log($"Check if cards are matched.");
            StartCoroutine(CheckIfCardsMatched());
        }
    }

    private IEnumerator CheckIfCardsMatched()
    {
        boardManager.DisableCardInput();

        yield return new WaitForSeconds(0.5f);

        if (firstCardName == secondCardName)
        {
            Debug.Log("Cards are matched!");
            pairCounter++;

            boardManager.CheckCardPairResult(true);
            CheckIfLevelIsDone();
        }
        else
        {
            Debug.Log("Cards doesn't matched!");
            boardManager.CheckCardPairResult(false);
        }

        boardManager.EnableCardInput();
    }

    private void CheckIfLevelIsDone()
    {
        if (pairCounter == pairTotalNeeded)
        {
            Debug.Log($"Level Done!");
            GameWon();
        }
    }

    private void GameWon()
    {
        gameHUD.Hide();
        resultScreen.ShowWinResult("You Win!", totalMoves);
        StopTimer();
    }

    private void GameLose()
    {
        gameHUD.Hide();
        resultScreen.ShowWinResult("You Lose!", totalMoves);
        StopTimer();
    }

    private void ReturnToMainMenu()
    {
        canvasManager.SwitchToMainMenu();
    }

    private void StartTimer()
    {
        timerCoroutine = StartCoroutine(CountdownTimer());
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }

    private IEnumerator CountdownTimer()
    {
        gameHUD.UpdateTimer(timer);

        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            gameHUD.UpdateTimer(timer);
        }

        GameLose();
    }
}
