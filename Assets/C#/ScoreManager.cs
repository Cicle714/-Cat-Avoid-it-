using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Player player;
    private float score = 0; //スコア
    private int myScore; //スコアの最終値

    [SerializeField]
    private Text scoreText; //スコアの表示

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
            myScore = (int)(score * 100); //スコアの最終値
            scoreText.text = "Score:" + myScore;
        }
    }
    public IEnumerator ScoreDisplay()
    {
        while (scoreText.color.a < 1)
        {
            scoreText.color += new Color(0,0,0,Time.deltaTime / 2);
            yield return null;
        }
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.Space) ||  Input.GetKeyDown(KeyCode.Return))
            break;
            yield return null;
        }
        while (scoreText.color.a > 0)
        {
            scoreText.color -= new Color(0, 0, 0, Time.deltaTime / 2);
            yield return null;
        }
    }
}