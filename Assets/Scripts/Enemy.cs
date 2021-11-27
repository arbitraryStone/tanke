using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;

    private float v = -1;

    private float h = 0;

    public Vector3 bulletEulerAngles;

    //引用
    public SpriteRenderer sr;

    public GameObject bulletPrefabs;

    public Sprite[] tankeSprite; //上，右，下，左

    public GameObject explosionPrefab;

    //计时器
    public float timeValChangeDirection;

    private float timeVal;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        //攻击Cd
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    //坦克的攻击方法
    private void Attack()
    {
        int num = Random.Range(0, 10);
        if (num == 0)
        {
            Instantiate(bulletPrefabs,
            transform.position,
            Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    //坦克的移动方法
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (
                num > 5 //向下
            )
            {
                v = -1;
                h = 0;
            }
            else if (
                num == 0 //向上
            )
            {
                v = 1;
                h = 0;
            }
            else if (
                num >= 0 && num <= 2 //向左
            )
            {
                h = -1;
                v = 0;
            }
            else if (
                num > 2 && num <= 4 //向右
            )
            {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        transform
            .Translate(Vector3.right * h * moveSpeed * Time.deltaTime,
            Space.World);
        if (h > 0)
        {
            sr.sprite = tankeSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        else if (h < 0)
        {
            sr.sprite = tankeSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        if (h != 0) return;
        transform
            .Translate(Vector3.up * v * moveSpeed * Time.deltaTime,
            Space.World);
        if (v > 0)
        {
            sr.sprite = tankeSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        else if (v < 0)
        {
            sr.sprite = tankeSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
    }

    //坦克的死亡方法
    private void Die()
    {
        PlayerManager.Instance.playScore1++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy (gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            h = -h;
            v = -v;
        }
    }
}
