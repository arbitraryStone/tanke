using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;

    public bool isPlayerbullet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform
            .Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Tanke":
                if (!isPlayerbullet)
                {
                    other.SendMessage("Die");
                    Destroy (gameObject);
                }
                break;
            case "Heart":
                other.SendMessage("Die");
                Destroy (gameObject);
                break;
            case "Enemy":
                if (isPlayerbullet)
                {
                    other.SendMessage("Die");
                    Destroy (gameObject);
                }
                break;
            case "War":
                Destroy(other.gameObject);
                Destroy (gameObject);
                break;
            case "River":
                break;
            case "Grass":
                break;
            case "Barrier":
                Destroy (gameObject);
                break;
            default:
                break;
        }
    }
}
