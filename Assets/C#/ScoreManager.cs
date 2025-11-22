using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    Player player;
    private float score = 0; //スコア
    private int myScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetHp > 0)
        {
            score += Time.deltaTime;
        }
        else
        {
            myScore = (int)(score * 100); //スコアを整数化
        }
    }
}
