using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;

    private float timeVal;

    public Vector3 bulletEulerAngles;

    private bool isDefended = true;

    private float defendTimeVal = 3;

    //引用
    public SpriteRenderer sr;

    public GameObject bulletPrefabs;

    public Sprite[] tankeSprite; //上，右，下，左

    public GameObject explosionPrefab;

    public GameObject defendEffectPrefab;

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
        if (PlayerManager.Instance.isDefeat) return;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.isDefeat) return;

        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }

        //攻击Cd
        if (timeVal >= 0.4f)
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
        if (Input.GetKeyDown(KeyCode.J))
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
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
        if (isDefended)
        {
            return;
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDead = true;
        Destroy (gameObject);
    }
}
