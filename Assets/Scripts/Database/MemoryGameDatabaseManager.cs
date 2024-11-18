using System.Collections;
using UnityEngine;
using Firebase.Database;
using System.Linq;
using System;

public class MemoryGameDatabaseManager : MonoBehaviour
{
    private const string USER_SESSIONS_KEY = "userSessions";

    private DatabaseReference database;
    private UserSession highestUserSession;

    private void Awake()
    {
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        StartCoroutine(LoadLeaderboard());
    }

    public void GetHighestUserSession(string username)
    {
        StartCoroutine(GetUserSession(username));
    }

    public IEnumerator GetUserSession(string username)
    {
        var usersessionTask = database.Child(USER_SESSIONS_KEY).Child(username).GetValueAsync();
        
        yield return new WaitUntil(predicate: () => usersessionTask.IsCompleted);

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

        database.Child(USER_SESSIONS_KEY).Child(username).SetRawJsonValueAsync(jsonData);
    }

    public IEnumerator LoadLeaderboard()
    {
        var leaderBoardTask = database.Child(USER_SESSIONS_KEY).OrderByChild("Difficulty").GetValueAsync();

        yield return new WaitUntil(predicate: () => leaderBoardTask.IsCompleted);

        DataSnapshot snapshot = leaderBoardTask.Result;

        foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
        {
            var name = childSnapshot.Key;
            var difficulty = childSnapshot.Child("Difficulty").Value;
            var totalMoves = childSnapshot.Child("TotalMoves").Value;

            Debug.Log($"{name} {difficulty} {totalMoves}");
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
