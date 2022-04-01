using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class playfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                CustomId = "SampleID",
                CreateAccount = true
            },
            result => Debug.Log("log in success"),
            error => Debug.Log(error)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // score up
            SubmitScore(100);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // score up
            SubmitScore(20);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // score up
            SubmitScore(200);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // get ranking
            RequestLeaderboard();
        }
    }

    private void RequestLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = "HighScore",
                StartPosition = 0,
                MaxResultsCount = 100,
            },
            result => {
                Debug.Log("");
                result.Leaderboard.ForEach(
                    x => Debug.Log(string.Format("rank {0}: score {1}", x.Position+1, x.StatValue))
                );
            },
            error => Debug.Log(error)
        );
    }

    private void SubmitScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = "HighScore",
                        Value = score
                    }
                }
            },
            result => Debug.Log("score update"),
            error => Debug.Log(error)
        );
    }
}
