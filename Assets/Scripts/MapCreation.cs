using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    public GameObject[] item;

    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5. 草 6.空气墙
    private List<Vector3> itemPositonList = new List<Vector3>();

    private void Awake()
    {
        createItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        createItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        createItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            createItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        //实例化围墙
        for (int i = -11; i < 12; i++)
        {
            createItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            createItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            createItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            createItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //实例化玩家
        GameObject play1 =
            Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);

        play1.GetComponent<Born>().createPlayer = true;

        //实例化敌人
        createItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        createItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        createItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        InvokeRepeating("createEnemy", 4, 5);

        //实例化地图
        for (int i = 0; i < 60; i++)
        {
            createItem(item[1], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(item[2], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(item[4], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(item[5], createRandomPosition(), Quaternion.identity);
        }
    }

    private void createItem(
        GameObject createGameObject,
        Vector3 createPosition,
        Quaternion createRotation
    )
    {
        GameObject itemGame =
            Instantiate(createGameObject, createPosition, createRotation);
        itemGame.transform.SetParent(gameObject.transform);

        itemPositonList.Add (createPosition);
    }

    private Vector3 createRandomPosition()
    {
        //不生成x=-10,10,y=-8,8
        while (true)
        {
            Vector3 createPosition =
                new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!hasThePosition(createPosition)) return createPosition;
        }
    }

    private bool hasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositonList.Count; i++)
        {
            if (createPos == itemPositonList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void createEnemy()
    {
        int num = Random.Range(0, 3);

        Vector3 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 2)
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        createItem(item[3], EnemyPos, Quaternion.identity);
    }
}
