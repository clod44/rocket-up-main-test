using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    [SerializeField]
    private float minForce = 0.5f;
    [SerializeField]
    private float speed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Mathf.Abs(Input.GetAxisRaw("Vertical")) + minForce);
    }
    void FixedUpdate()
    {
        rb.AddForce(moveVector * speed * Time.fixedDeltaTime, ForceMode2D.Force);
    }

}
