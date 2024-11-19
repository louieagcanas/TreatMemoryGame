using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour, IScreen
{
    [SerializeField]
    private GameObject leaderboardEntryPrefab;

    [SerializeField]
    private MemoryGameDatabaseManager databaseManager;

    [SerializeField]
    private Transform leaderboardEntriesParent;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLeaderboard()
    {
        databaseManager.OnLeaderboardLoaded += PopulateBoard;
        databaseManager.LoadLeaderboardData();
    }

    private void PopulateBoard()
    {
        List<LeaderboardEntryData> leaderboardData = databaseManager.LeaderboardEntries;
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
