using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    [SerializeField] private float jumpheight = 5;
    private bool landed = false;
    private bool doubleJump = false;

    private Animator anim;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        //Player moving right and left
        #region Player moving right and left
        //transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        #endregion

        #region normal Jump & doubleJump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (landed)
            {
                rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
                landed = false;
                doubleJump = true;
                return;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
            doubleJump = false;
        }
        #endregion

        #region Animation
        //Animation for Jumping
        if (direction != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (direction < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -direction * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        }
        #endregion
    }

        #region erster Code zum springen
        ////Player can jump
        //if (Input.GetKeyDown(KeyCode.Space) && landed)
        //{
        //    rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
        //    landed = false;
        //}
        #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            landed = true;
        }
    }

}
