using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public event Action<string> OnCardSelected;

    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Sprite[] availableSprites;

    [SerializeField]
    private float boardOffset;

    private GridLayoutGroup gridLayoutGroup;
    private RectTransform board;

    private List<Card> selectedCards = new List<Card>();
    private List<Card> availableCards = new List<Card>();

    private void Awake()
    {
        board = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    public void Initialize(int difficultyLevel)
    {
        int totalCards = (difficultyLevel + 1) * 2; //2 as cards will be in pairs

        PopulateBoard(totalCards, difficultyLevel);
        ResizeBoard(totalCards);
    }

    private void PopulateBoard(int totalCards, int difficultyLevel)
    {
        // Get memory images
        List<Sprite> cardSprites = new List<Sprite>();
        int spriteIndex = 0;
        for(int i = 0; i < totalCards; i++ )
        {
            cardSprites.Add(availableSprites[spriteIndex]);

            if(i != 0 && i%2 != 0)
            {
                spriteIndex++;
            }
        }

        // Shuffle images
        for( int i = 0; i < cardSprites.Count; i++ )
        {
            Sprite temp = cardSprites[i];
            int randomIndex = UnityEngine.Random.Range(0, cardSprites.Count);
            cardSprites[i] = cardSprites[randomIndex];
            cardSprites[randomIndex] = temp;
        }

        // Initialize Cards
        for (int i = 0; i < totalCards; i++)
        {
            if( i < availableCards.Count )
            {
                //Use existing
                Card card = availableCards[i];
                card.Initialize(i, cardSprites[i]);
            }
            else
            {
                //Create new - can be removed since main menu is now on a separate scene
                GameObject cardInstance = GameObject.Instantiate(cardPrefab, transform);
                Card card = cardInstance.GetComponent<Card>();
                card.Initialize(i, cardSprites[i]);
                card.OnCardSelected += CardSelected;

                availableCards.Add(card);
            }
        }
    }

    private void CardSelected(int cardIndex)
    {
        Card card = availableCards[cardIndex];
        selectedCards.Add(card);
        OnCardSelected?.Invoke(card.CardName);
    }

    //private void ResizeBoard(int totalCards)
    //{
    //    Debug.Log($"Total Cards: {totalCards}");

    //    int bestRows = 1;
    //    int bestColumns = totalCards;
    //    int minimumDifference = Mathf.Abs(bestRows - bestColumns);

    //    for (int rows = 1; rows <= totalCards; rows++)
    //    {
    //        if (totalCards % rows == 0)
    //        {
    //            int columns = totalCards / rows;
    //            int difference = Mathf.Abs(rows - columns);

    //            if (difference < minimumDifference)
    //            {
    //                bestRows = rows;
    //                bestColumns = columns;
    //                minimumDifference = difference;
    //            }
    //        }
    //    }

    //    Debug.Log($"Best Rows: {bestRows}");
    //    Debug.Log($"Best Columns: {bestColumns}");

    //    float width = (bestColumns * gridLayoutGroup.cellSize.x) + (bestColumns * gridLayoutGroup.spacing.x) + boardOffset;
    //    float height = (bestRows * gridLayoutGroup.cellSize.y) + (bestRows * gridLayoutGroup.spacing.y) + boardOffset;
    //    board.sizeDelta = new Vector2(width, height);
    //}

    private void ResizeBoard(int totalCards)
    {
        Debug.Log($"Total Cards: {totalCards}");

        int rows = (int)Math.Ceiling((double)totalCards / 4);
        Debug.Log($"Rows: {rows}");

        int baseRowSize = totalCards / rows;
        int remainder = totalCards % rows;

        int columns = baseRowSize + (remainder > 0 ? 1 : 0);

        Debug.Log($"Column: {columns}");

        float width = (columns * gridLayoutGroup.cellSize.x) + (columns * gridLayoutGroup.spacing.x) + boardOffset;
        float height = (rows * gridLayoutGroup.cellSize.y) + (rows * gridLayoutGroup.spacing.y) + boardOffset;
        board.sizeDelta = new Vector2(width, height);
    }

    public void CheckCardPairResult(bool didMatched)
    {
        if(didMatched)
        {
            foreach (Card card in selectedCards)
            {
                card.MarkAsDone();
            }
        }
        else
        {
            foreach( Card card in selectedCards )
            {
                card.FlipShowCardBack();
            }
        }

        selectedCards.Clear();
    }

    public void Reset()
    {
        selectedCards.Clear();
        foreach(Card card in availableCards)
        {
            card.OnCardSelected -= CardSelected;
        }
    }

    public void EnableCardInput()
    {
        foreach(Card card in availableCards)
        {
            card.EnableInput();
        }
    }
    public void DisableCardInput()
    {
        foreach (Card card in availableCards)
        {
            card.DisableInput();
        }
    }
}
