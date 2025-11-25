using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemys;

    [SerializeField]
    private float AttackInterval; //攻撃の感覚
    private float GameSpeed = 1; //攻撃スピードの倍率

    [SerializeField]
    private float AttackCount; //攻撃のカウント

    private int HeartCount; //ハートのカウント
    [SerializeField]
    private int HeartInterval; //ハートが出る感覚

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeartCount = HeartInterval;
        AttackCount = AttackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackCount < 0)
        {
            HeartCount--;
            int ran = Random.Range(0, enemys.Count);　//エネミーがランダムに選ばれ攻撃する
            enemys[ran].Attack = true; //エネミーの攻撃をtrueにする

            //ハートのカウントが0になったら、弾ではなく回復アイテムを出す
            if (HeartCount <= 0)
            {
                enemys[ran].heart = true;
                HeartCount = HeartInterval; //カウントのリセット
            }
            AttackCount = AttackInterval; //カウントのリセット
        }
        AttackCount -= Time.deltaTime * GameSpeed;
        GameSpeed += Time.deltaTime / 20;
    }
}
