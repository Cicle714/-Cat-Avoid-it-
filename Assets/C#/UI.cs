using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField]
    private List<Image> Hearts; //ライフ表示
    private Player player;

    [SerializeField]
    private Image BlackImage; //暗転用

    private bool GameOver = false; //ゲームオーバー判定

    public float Score = 0; //スコア


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        //BlackImage = BlackImage.GetComponent<Image>();
        StartCoroutine(BlackOut()); //暗転処理


    }

    // Update is called once per frame
    void Update()
    {

        

        //残りHp表示
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < player.GetHp)
                Hearts[i].gameObject.SetActive(true);
            else
                Hearts[i].gameObject.SetActive(false);
        }

        if(player.gameObject.activeSelf == false && !GameOver)
        {
            GameOver = true;
            StartCoroutine(BlackOut2()); //暗転処理
        }
    }

    /// <summary>
    /// 暗転処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BlackOut()
    {

        while (BlackImage.color.a > 0)
        {
            BlackImage.color -= new Color(0, 0, 0, 1 * Time.deltaTime / 2);
            yield return null;
        }
    }
    /// <summary>
    /// 暗転処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator BlackOut2()
    {
        yield return new WaitForSeconds(1.0f);
        while (BlackImage.color.a < 1.0f)
        {
            BlackImage.color += new Color(0, 0, 0, 1 * Time.deltaTime / 2);
            yield return null;
        }
        SceneManager.LoadScene("Title");
        yield return null;
    }

}
