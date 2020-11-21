using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotePress : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public AudioSource Hit;
    public AudioSource Crash;
    public KeyCode key;
    public KeyCode key2;
    public KeyCode key3;

    public Animator down;
    public Animator down1;
    public Animator down2;
    public Animator down3;
    public Animator down4;

    public Animator up;
    public Animator up1;
    public Animator up2;
    public Animator up3;


    public bool notePress;
    public bool notePress2;
    public bool notePress3;
    public bool notePress4;
    public bool notePress5;
    public bool notePress6;
    public bool notePress7;
    public bool notePress8;
    public bool notePress9;
    public void Start()
    {
        notePress = true;
        notePress2 = false;
        notePress3 = false;
        notePress4 = false;
        notePress5 = false;
        notePress6 = false;
        notePress7 = false;
        notePress8 = false;
        notePress9 = false;

        down.SetBool("Pressed", true);  
        down1.SetBool("Pressed1", true);
        down2.SetBool("Pressed2", true);
        down3.SetBool("Pressed3", true);
        down4.SetBool("Pressed4", true);

        up.SetBool("Upp", true);
        up1.SetBool("Upp1", true);
        up2.SetBool("Upp2", true);
        up3.SetBool("Upp3", true);
    }
    public void Update()
    {
        if (notePress && Input.GetKeyDown(key2))
        {
            down.SetBool("Pressed", false);
            notePress2 = true;
            Hit.Play();
            notePress = false;
        }

        if (notePress2 && Input.GetKeyDown(key))
        {
            up.SetBool("Upp", false); ;
            notePress3 = true;
            Crash.Play();
            notePress2 = false;
        }
        if (notePress3 && Input.GetKeyDown(key2))
        {
            down1.SetBool("Pressed1", false);
            notePress4 = true;
            Hit.Play();
            notePress3 = false;
        }
        if (notePress4 && Input.GetKeyDown(key))
        {
            up1.SetBool("Upp1", false); ;
            notePress5 = true;
            Crash.Play();
            notePress4 = false;
        }
        if (notePress5 && Input.GetKeyDown(key2))
        {
            down2.SetBool("Pressed2", false);
            notePress6 = true;
            Hit.Play();
            notePress5 = false;
        }
        if (notePress6 && Input.GetKeyDown(key))
        {
            up2.SetBool("Upp2", false); ;
            notePress7 = true;
            Crash.Play();
            notePress6 = false;
        }
        if (notePress7 && Input.GetKeyDown(key2))
        {
            down3.SetBool("Pressed3", false);
            notePress8 = true;
            Hit.Play();
            notePress7 = false;
        }
        if (notePress8 && Input.GetKeyDown(key3))
        {
            down4.SetBool("Pressed4", false);
            notePress9 = true;
            Hit.Play();
            notePress8 = false;
        }

        if (notePress9 && Input.GetKeyDown(key))
        {
            up3.SetBool("Upp3", false); ;
            
            Crash.Play();
            LoadNextLevel();


        }






    }
    public void Repeat()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
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
}
