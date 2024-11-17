using System;
using System.Collections;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    public event Action OnLevelDone;

    [SerializeField]
    private BoardManager boardManager;

    [SerializeField]
    private GameHUD gameHUD;

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
        gameHUD.OnGameRestart += StartGame;
        boardManager.OnCardSelected += CardSelected;
    }

    private void Update()
    {
        
    }

    private void Reset()
    {
        totalMoves = 0;
        gameHUD.UpdateMoveCounter(totalMoves);

        guessCounter = 0;
        pairCounter = 0;

        firstCardName = string.Empty;
        secondCardName = string.Empty;
    }

    public void StartGame()
    {
        Reset();

        timer = levelTimers.Timers[GameSettings.GetDifficultyLevel() - 1];
        pairTotalNeeded = GameSettings.GetDifficultyLevel() + 1;
        boardManager.Initialize(GameSettings.GetDifficultyLevel());

        timerCoroutine = StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        gameHUD.UpdateTimer(timer);

        while (timer > 0 )
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            gameHUD.UpdateTimer(timer);
        }
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

            Debug.Log($"Check if matched!");
            StartCoroutine( CheckIfCardsMatched() );
        }
    }

    private IEnumerator CheckIfCardsMatched()
    {
        boardManager.DisableCardInput();

        yield return new WaitForSeconds(0.5f);

        if (firstCardName == secondCardName)
        {
            Debug.Log("Cards matched!");
            pairCounter++;

            boardManager.CheckCardPairResult(true);
            StartCoroutine(CheckIfLevelIsDone());
        }
        else
        {
            Debug.Log("Cards doesn't matched!");
            boardManager.CheckCardPairResult(false);
        }

        boardManager.EnableCardInput();
    }

    private IEnumerator CheckIfLevelIsDone()
    {
        yield return new WaitForSeconds(0.2f);
        if (pairCounter == pairTotalNeeded)
        {
            Debug.Log($"Level Done!");
            StopCoroutine(timerCoroutine);
            OnLevelDone?.Invoke();
        }
    }
}
