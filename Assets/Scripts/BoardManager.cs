using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    //[SerializeField]
    //private float cellWidth;

    //[SerializeField]
    //private float cellHeight;

    [SerializeField]
    private float boardOffset;

    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private GameSave gameSave;

    private GridLayoutGroup gridLayoutGroup;

    private int totalCards = 0;

    private RectTransform board;

    private void Awake()
    {
        board = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        totalCards = (gameSave.GetDifficultyLevel() + 1) * 2;
        PopulateBoard();
        ResizeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateBoard()
    {
        for(int i = 0; i < totalCards; i++)
        {
            GameObject cardInstance = GameObject.Instantiate(cardPrefab, transform);
        }
    }

    private void ResizeBoard()
    {

        Debug.Log($"Total Cards: {totalCards}");

        int bestRows = 1;
        int bestColumns = totalCards;
        int minimumDifference = Mathf.Abs(bestRows - bestColumns);

        for (int rows = 1; rows <= totalCards; rows++)
        {
            if (totalCards % rows == 0)
            {
                int columns = totalCards / rows;
                int difference = Mathf.Abs(rows - columns);

                if (difference < minimumDifference)
                {
                    bestRows = rows;
                    bestColumns = columns;
                    minimumDifference = difference;
                }
            }
        }

        Debug.Log($"Best Rows: {bestRows}");
        Debug.Log($"Best Columns: {bestColumns}");

        float width = (bestColumns * gridLayoutGroup.cellSize.x) + (bestColumns * gridLayoutGroup.spacing.x) + boardOffset;
        float height = (bestRows * gridLayoutGroup.cellSize.y) + (bestRows * gridLayoutGroup.spacing.y) + boardOffset;
        board.sizeDelta = new Vector2(width, height);

        //int rows = 2; //minimum 2;
        //int columns = totalCards / 2;
        //if( columns > maximumColumns )
        //{
        //    int extraRow = columns % maximumColumns;
        //    rows += extraRow;
        //    columns = maximumColumns;
        //}

        //float width = (columns * cellWidth) + boardOffset;
        //float height = (rows * cellHeight) + boardOffset;
        //board.sizeDelta = new Vector2 (width, height);
    }
}
