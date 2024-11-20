using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class MemoryGameDatabaseManager : MonoBehaviour
{
    private static MemoryGameDatabaseManager instance;
    public static MemoryGameDatabaseManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject gameObject = new GameObject("DatabaseManager");
                instance = gameObject.AddComponent<MemoryGameDatabaseManager>();
            }

            return instance;
        }
    }

    private const string USER_SESSIONS_KEY = "userSessions";

    private DatabaseReference database;
    private UserSession highestUserSession;

    private event Action  onLeaderboardLoaded;

    public List<LeaderboardEntryData> LeaderboardEntries { private set; get; } = new List<LeaderboardEntryData>();

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GetHighestUserSession(string username)
    {
        StartCoroutine(GetUserSession(username));
    }

    private IEnumerator GetUserSession(string username)
    {
        var usersessionTask = database.Child(USER_SESSIONS_KEY).Child(username).GetValueAsync();
        
        yield return new WaitUntil(predicate: () => usersessionTask.IsCompleted);

        if (usersessionTask.Exception != null)
        {
            Debug.Log($"User Session Task Exception: {usersessionTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = usersessionTask.Result;
            string jsonData = snapshot.GetRawJsonValue();

            if( !string.IsNullOrEmpty(jsonData) )
            {
                highestUserSession = JsonUtility.FromJson<UserSession>(jsonData);
            }
            else
            {
                Debug.Log("Data not found!");
            }
        }
    }

    public bool TryToSaveUserSession(string username, int difficulty, int totalMoves)
    {
        if( highestUserSession != null )
        {
            if (difficulty > highestUserSession.Difficulty)
            {
                Debug.Log($"Highest Difficulty: {highestUserSession.Difficulty}");
                Debug.Log($"Current Difficulty: {difficulty}");

                Debug.Log("Beaten a higher difficulty!");
                SaveUserSession(username, difficulty, totalMoves);
                return true;
            }
            else if (difficulty == highestUserSession.Difficulty)
            {
                if (totalMoves < highestUserSession.TotalMoves)
                {
                    Debug.Log($"Lowest Total Moves: {highestUserSession.TotalMoves}");
                    Debug.Log($"Current Difficulty: {totalMoves}");

                    Debug.Log("Made less moves on the same level!");
                    SaveUserSession(username, difficulty, totalMoves);
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("No prior session yet. Saving this one..");
            SaveUserSession(username, difficulty, totalMoves);
            return true;
        }

        Debug.Log("Highest user session remain!");

        return false;
    }

    private void SaveUserSession(string username, int difficulty, int totalMoves)
    {
        highestUserSession = new UserSession(difficulty, totalMoves);
        string jsonData = JsonUtility.ToJson(highestUserSession);

        var saveUserSessionTask = database.Child(USER_SESSIONS_KEY).Child(username).SetRawJsonValueAsync(jsonData);

        if (saveUserSessionTask != null)
        {
            Debug.Log($"Save User Session Task Exception: {saveUserSessionTask.Exception}");
        }
    }

    public void LoadLeaderboardData(Action leaderboardCallback)
    {
        onLeaderboardLoaded += leaderboardCallback;
        StartCoroutine(LoadLeaderboard());
    }

    private IEnumerator LoadLeaderboard()
    {
        var leaderBoardTask = database.Child(USER_SESSIONS_KEY).OrderByChild("Difficulty").GetValueAsync();

        yield return new WaitUntil(predicate: () => leaderBoardTask.IsCompleted);

        if (leaderBoardTask.Exception != null)
        {
            Debug.Log($"Leaderboard Task Exception: {leaderBoardTask.Exception}");
        }
        else
        {
            LeaderboardEntries.Clear();

            DataSnapshot snapshot = leaderBoardTask.Result;

            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
                //foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string name = childSnapshot.Key;
                int difficulty = int.Parse(childSnapshot.Child("Difficulty").Value.ToString());
                int totalMoves = int.Parse(childSnapshot.Child("TotalMoves").Value.ToString());

                LeaderboardEntryData leaderboardEntryData = new LeaderboardEntryData(name, difficulty, totalMoves);
                LeaderboardEntries.Add(leaderboardEntryData);
            }

            onLeaderboardLoaded?.Invoke();
            onLeaderboardLoaded = null;
        }
    }
}

[Serializable]
public class UserSession
{
    public int Difficulty;
    public int TotalMoves;

    public UserSession(int difficulty, int totalMoves)
    {
        Difficulty = difficulty;
        TotalMoves = totalMoves;
    }
}

[Serializable]
public class LeaderboardEntryData
{
    public string Username;
    public int Difficulty;
    public int TotalMoves;

    public LeaderboardEntryData(string username, int difficulty, int totalMoves)
    {
        Username = username;
        Difficulty = difficulty;
        TotalMoves = totalMoves;
    }
}
