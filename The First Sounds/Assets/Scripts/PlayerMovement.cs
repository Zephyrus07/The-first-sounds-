using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq.Expressions;


public class PlayerMovement : MonoBehaviour
{
    
    public Animator transition;
    public bool entering;
    public bool songStarts;
    public float transitionTime = 1f;
    public Rigidbody2D rb;
    public Animator jump;
    public float JumpForce = 10f;
    public float moveSpeed = 10f;
    public float checkRadius;
    public ParticleSystem dust;
    

    public Transform groundCheck;
    public LayerMask Material;

    private bool isGrounded;
    
    private int extraJumps;
    public int extraJumpsValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Material);

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed; 


    }
    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps>0) {
            rb.velocity = Vector2.up * JumpForce;
            extraJumps--;
            dust.Play();
            jump.SetTrigger("IsJumping");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
            dust.Play();
        }
        if(songStarts && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextLevel();
        }
        if (entering && Input.GetKeyDown(KeyCode.W))
        {
            Galerry();
        }


    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Beatles")
        {
            songStarts = true;
        }
        if (col.gameObject.tag == "Exit")
        {
            LoadNextLevel();
        }
        if (col.gameObject.tag == "Entrance")
        {
            LoadNextLevel();
        }
        if (col.gameObject.tag == "EnterG")
        {
            entering = true;

        }
        if (col.gameObject.tag == "ExitG")
        {
            SceneManager.LoadScene("MainRoom 2");
        }
        if (col.gameObject.tag == "ExitTime")
        {
            LoadNextLevel();
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Beatles")
        {
            songStarts = false;
        }
        if (col.gameObject.tag == "EnterG")
        {
            entering = false;
        }
    }
    public void Galerry()
    {
     SceneManager.LoadScene("Gallery");
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        

    }
    



    public void Still()
    {
        JumpForce = 0f;
        moveSpeed = 0f;
    }
    public void Move()
    {
        JumpForce = 12f;
        moveSpeed = 10f;
    }
    
}
