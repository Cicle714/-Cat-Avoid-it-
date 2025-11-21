using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemys;

    [SerializeField]
    private float AttackInterval;
    private float GameSpeed = 1;

    [SerializeField]
    private float AttackCount;

    private int HeartCount;
    [SerializeField]
    private int HeartInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeartCount = HeartInterval;
        AttackCount = AttackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(AttackCount < 0)
        {
            HeartCount--;
            int ran = Random.Range(0,enemys.Count);
            enemys[ran].Attack = true;
            if (HeartCount <= 0)
            {
                enemys[ran].heart = true;
                HeartCount = HeartInterval;
            }
            AttackCount = AttackInterval;
        }
        AttackCount -= Time.deltaTime * GameSpeed;
        GameSpeed += Time.deltaTime / 20; 
    }
}
