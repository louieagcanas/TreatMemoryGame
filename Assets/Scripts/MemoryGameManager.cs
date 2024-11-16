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

    private int totalMoves = 0;
    private int guessCounter = 0;
    private int pairCounter = 0;
    private int pairTotalNeeded;

    private string firstCardName;
    private string secondCardName;

    private void Awake()
    {
        gameHUD.OnGameRestart += StartGame;
        boardManager.OnCardSelected += CardSelected;
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

        pairTotalNeeded = GameSettings.GetDifficultyLevel() + 1;
        boardManager.Initialize(GameSettings.GetDifficultyLevel());
    }

    private void CardSelected(string cardName)
    {
        totalMoves++;
        gameHUD.UpdateMoveCounter(totalMoves);

        guessCounter++;
         
        if(guessCounter == 1)
        {
            firstCardName = cardName;

            //start timer
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

        yield return new WaitForSeconds(1.0f);

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
            OnLevelDone?.Invoke();
        }
    }
}
