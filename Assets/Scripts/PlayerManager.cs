using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeVal1 = 3;

    public int lifeVal2 = 3;

    public int playScore1 = 0;

    public int playScore2 = 0;

    public bool isDead;

    public bool isDefeat;

    //引用
    public GameObject born;

    private static PlayerManager instance;

    public Text playerSocreText;

    public Text playerLifeValueText;

    public GameObject isDefeatUI;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            SceneManager.LoadScene(1);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playerSocreText.text = playScore1.ToString();
        playerLifeValueText.text = lifeVal1.ToString();
    }

    private void Recover()
    {
        if (lifeVal1 < 0)
        {
            //游戏失败
            isDefeat = true;
        }
        else
        {
            lifeVal1--;
            GameObject go =
                Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
}
