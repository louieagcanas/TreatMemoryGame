using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usernameText;

    [SerializeField]
    private TextMeshProUGUI difficultyText;

    [SerializeField]
    private TextMeshProUGUI totalMovesText;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color currentUserColor;

    public void Initialize(LeaderboardEntryData leaderboardEntryData, bool isCurrentUser = false)
    {
        usernameText.text = leaderboardEntryData.Username;
        difficultyText.text = leaderboardEntryData.Difficulty.ToString();
        totalMovesText.text = leaderboardEntryData.TotalMoves.ToString();

        if( isCurrentUser )
        {
            usernameText.color = currentUserColor;
            difficultyText.color = currentUserColor;
            totalMovesText.color = currentUserColor;
        }
        else
        {
            usernameText.color = defaultColor;
            difficultyText.color = defaultColor;
            totalMovesText.color = defaultColor;
        }
    }
}
