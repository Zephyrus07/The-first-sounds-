using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq.Expressions;


public class PlayerMovement : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource fall;
    public AudioSource gate;
    public Vector3 spawnPoint;
    public Animator interact;
    public Animator transition;
    public bool interactible;
    public bool entering;
    public bool songStarts;
    public float transitionTime = 1f;
    public Rigidbody2D rb;
    public Animator jump;
    public float JumpForce = 15f;
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
            jumpSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
            dust.Play();
            jump.SetTrigger("IsJumping");
            jumpSound.Play();
        }
        if(songStarts && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextLevel();
        }
        if (entering && Input.GetKeyDown(KeyCode.W))
        {
            Galerry();
            gate.Play();
        }
        if (interactible && Input.GetKeyDown(KeyCode.W))
        {
            LoadNextLevel();
            gate.Play();
        }


    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Beatles")
        {
            songStarts = true;
            interact.SetBool("Interacted", true);
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
            interact.SetBool("Interacted", true);

        }
        if (col.gameObject.tag == "ExitG")
        {
            SceneManager.LoadScene("MainRoom 2");
        }
        if (col.gameObject.tag == "ExitTime")
        {
            LoadNextLevel();
        }
        if (col.gameObject.tag == "FALL")
        {
            rb.transform.position = spawnPoint;
            fall.Play();
        }
        if (col.gameObject.tag == "GoNext")
        {
            LoadNextLevel();
        }
        
        if (col.gameObject.tag == "EnterT")
        {
            interactible = true;
            interact.SetBool("Interacted", true);

        }
        if (col.gameObject.tag == "Time")
        {
            interactible = true;
            interact.SetBool("Interacted", true);

        }
        if (col.gameObject.tag == "Ending")
        {
            SceneManager.LoadScene("Start");
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Beatles")
        {
            songStarts = false;
            interact.SetBool("Interacted",false);
        }
        if (col.gameObject.tag == "EnterG")
        {
            entering = false;
            interact.SetBool("Interacted", false);
        }
        if (col.gameObject.tag == "EnterT")
        {
            interactible = false;
            interact.SetBool("Interacted", false);

        }
        if (col.gameObject.tag == "Time")
        {
            interactible = false;
            interact.SetBool("Interacted", false);

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
        JumpForce = 15f;
        moveSpeed = 10f;
    }
    
}
