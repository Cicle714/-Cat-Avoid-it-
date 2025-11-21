using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField]
    private List<Image> Hearts; //ÉâÉCÉtï\é¶
    private Player player;

    [SerializeField]
    private Image BlackImage; //à√ì]óp



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        //BlackImage = BlackImage.GetComponent<Image>();
        StartCoroutine(BlackOut()); //à√ì]èàóù


    }

    // Update is called once per frame
    void Update()
    {
        //écÇËHpï\é¶
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < player.GetHp)
                Hearts[i].gameObject.SetActive(true);
            else
                Hearts[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// à√ì]èàóù
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
    /// à√ì]èàóù
    /// </summary>
    /// <returns></returns>
    public IEnumerator BlackOut2()
    {
        while (BlackImage.color.a < 1)
        {
            BlackImage.color += new Color(0, 0, 0, 1 * Time.deltaTime / 2);
            yield return null;
        }
        SceneManager.LoadScene("Title");
        yield return null;
    }

}
