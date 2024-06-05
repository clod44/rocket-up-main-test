using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{

    private GameManager gameManager;
    private Rigidbody2D rb;
    [SerializeField]
    private float randomTorque = 20f;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, 10.0f);

        rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(Random.Range(-randomTorque, randomTorque), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.RestartLevel();
        }
    }

}
