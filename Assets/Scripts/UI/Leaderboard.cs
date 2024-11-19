using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour, IScreen
{
    [SerializeField]
    private GameObject leaderboardEntryPrefab;

    //[SerializeField]
    //private MemoryGameDatabaseManager databaseManager;

    [SerializeField]
    private Transform leaderboardEntriesParent;

    private List<LeaderboardEntry> leaderboardCards = new List<LeaderboardEntry>();

    public void ShowLeaderboard()
    {
        //databaseManager.OnLeaderboardLoaded += PopulateBoard;
        //databaseManager.LoadLeaderboardData();
        MemoryGameDatabaseManager.Instance.LoadLeaderboardData(PopulateBoard);
    }

    private void PopulateBoard()
    {
        List<LeaderboardEntryData> leaderboardData = MemoryGameDatabaseManager.Instance.LeaderboardEntries;
        string currentUserName = GameSettings.GetPlayerName();

        Debug.Log($"Leaderboard Cards Count: {leaderboardCards.Count}");

        for(int i = 0; i < leaderboardData.Count; i++ )
        {
            if( i < leaderboardCards.Count )
            {
                Debug.Log("Meron na!");
                LeaderboardEntry leaderboardEntry = leaderboardCards[i];
                leaderboardEntry.Initialize(leaderboardData[i], leaderboardData[i].Username == currentUserName);
            }
            else
            {
                Debug.Log("Wala pa!");
                GameObject leaderboardEntryObject = Instantiate(leaderboardEntryPrefab, leaderboardEntriesParent);
                LeaderboardEntry leaderboardEntry = leaderboardEntryObject.GetComponent<LeaderboardEntry>();
                leaderboardEntry.Initialize(leaderboardData[i], leaderboardData[i].Username == currentUserName);
                leaderboardCards.Add(leaderboardEntry);
            }
        }

        Show();
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
