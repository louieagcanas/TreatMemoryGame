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
    private Color color;

    public void Initialize(LeaderboardEntryData leaderboardEntryData, bool isCurrentUser = false)
    {
        usernameText.text = leaderboardEntryData.Username;
        difficultyText.text = leaderboardEntryData.Difficulty.ToString();
        totalMovesText.text = leaderboardEntryData.TotalMoves.ToString();

        if( isCurrentUser )
        {
            backgroundImage.color = color;
        }
    }
}
