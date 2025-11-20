using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    [SerializeField]
    private Image BlackImage; //暗転用

    private bool GameStart = false;　//ゲーム開始
    [SerializeField]
    private Text PushText;　//Tap to Screenの点滅表示
    private float PushCount;　//点滅のカウント
    private float DelayTime = 1;　//点滅の間隔

    void Start()
    {
        BlackImage = BlackImage.GetComponent<Image>();
        BlackImage.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (BlackImage.color.a > 0 && !GameStart)
            BlackImage.color -= new Color(0, 0, 0, 1 * Time.deltaTime / 2);　//暗転




        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            GameStart = true;　//ゲームスタート
        }
        if (GameStart)
        {
            PushCount += Time.deltaTime * 5;
            if (BlackImage.color.a != 1)
                BlackImage.color += new Color(0, 0, 0, 1 * Time.deltaTime / 2);　//暗転
            if (BlackImage.color.a >= 1)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("PlayScene");　//プレイ画面に移動
            }
        }
        else
        {
            PushCount += Time.deltaTime;
        }

        // 点滅処理
        if (PushCount > DelayTime)
        {
            if (PushText.gameObject.activeSelf)
                PushText.gameObject.SetActive(false);
            else PushText.gameObject.SetActive(true);
            PushCount = 0;
        }
    }



}
