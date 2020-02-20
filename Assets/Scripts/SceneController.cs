using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-100)]
public class SceneController : MonoBehaviour//, IPointerDownHandler
{
    public float Horizontal;
    [Header("Object References")]
        [SerializeField]
        private Player player;
        [SerializeField]
        private ScoreManager scoreManager;
        [SerializeField]
        private GameObject corpse;
        [SerializeField]
        private FloatingJoystick floatingJoystick;
        [SerializeField]
        public Canvas PauseMenu, WinMenu, LoseMenu;

    bool readyToClear;
    void Update()
    {
        player.Died += LoseLevel;
        //ClearInput();
        //if (GameManager.IsGameOver())
        //return;
        //ProcessInputs();
        Horizontal = floatingJoystick.Horizontal;
        Horizontal = Mathf.Clamp(Horizontal, -1f, 1f);
        //if (scoreManager.score == 5) WinLevel();        
        scoreManager.Won += WinLevel;

    }
    private void FixedUpdate()
    {
        //This ensure that all code gets to use the current inputs
        readyToClear = true;
    }
    private void WinLevel()
    {
        WinMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }
    private void LoseLevel()
    {
        LoseMenu.gameObject.SetActive(true) ;
        Time.timeScale = 0f;

    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Resume()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;

    }


    /// <summary>method <c>_findStartingPoint</c> find the possible position to spawn corpse, to prevent spawning on collisions</summary>
    /*private Vector2 _findStartingPoint(Vector2 position)

    {
        int i = 1;
        Debug.Log(i);
        i++;
        while (Physics2D.BoxCast(position, player.Collider.bounds.size, 0f, Vector2.down, .1f).collider != null) {
            Debug.Log("collision at starting point");
            position.y += i;
        }
        Debug.Log(i);
        return position;
    }
    public void Spawn()
    {
        Vector2 spawnPosition;
        var deadPosition = player.transform.position;
        player.RigidBody.velocity = Vector2.zero;
        spawnPosition = _findStartingPoint(Vector2.zero);//in future: SceneManager.Scene.startingPoint
        player.transform.position = spawnPosition; 
        GameObject newCorpse = Instantiate(corpse) as GameObject;
        newCorpse.transform.position = deadPosition;
    }
    public void ClearInput()
    {
        if (!readyToClear)
            return;
        //reset inputs
        Horizontal = 0f;
        readyToClear = false;

    }
    void ProcessInputs()
    {
        Horizontal = Input.GetAxis("Horizontal");
        IsJumpPressed = IsJumpPressed || Input.GetButtonDown("Jump");
    }*/

    /*public void OnPointerDown(PointerEventData eventData)
    {
        HandleSnowmanDied();
    }

    void HandleFingerSwipe(Lean.Touch.LeanFinger finger)
    {
        HandleSnowmanDied();
    }*/
}

//anchors to buttons
//podpinanie sie do eventu += albo -= - odpinanie
// CTRL + ,