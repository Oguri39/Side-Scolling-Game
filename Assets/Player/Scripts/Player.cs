using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpforce = 10f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;

    private bool isGrounded = true;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }


    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }
    private void FixedUpdate()
    {
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxis("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;


    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            anim.SetBool("isRun", true);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (movementX < 0)
        {
            anim.SetBool("isRun", true);
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    int count = 0;
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("isJump", true);
            myBody.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            Debug.Log("jump" + count++);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("isJump", true);
        }
    }
}
