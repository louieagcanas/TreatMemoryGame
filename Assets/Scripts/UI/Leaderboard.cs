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

        for(int i = 0; i < leaderboardData.Count; i++ )
        {
            GameObject leaderboardEntryObject = Instantiate(leaderboardEntryPrefab, leaderboardEntriesParent);
            LeaderboardEntry leaderboardEntry = leaderboardEntryObject.GetComponent<LeaderboardEntry>();
            leaderboardEntry.Initialize(leaderboardData[i], leaderboardData[i].Username == currentUserName);
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
