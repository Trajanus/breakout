using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerHealth = 100;

    private Text levelText;
    private GameObject levelImage;
    private int level = 1;
    //private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    private System.Random random = new System.Random();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    public void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        SetLevelText(level);

        //enemies.Clear();
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    private void SetLevelText(int levelCount)
    {
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + levelCount;
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemiesMoving || doingSetup)
        //{
        //    return;
        //}

        if(boardScript.ball.transform.position.y < -5)
        {
            var ballrb = boardScript.ball.GetComponent<Rigidbody2D>();
            ballrb.velocity = Vector2.zero;
            

            if(1 == random.Next(2))
            {
                ballrb.AddForce(new Vector2(UnityEngine.Random.Range(-330, -380), UnityEngine.Random.Range(330, 380)));
            }
            else
            {
                ballrb.AddForce(new Vector2(UnityEngine.Random.Range(330, 380), UnityEngine.Random.Range(330, 380)));
            }
            boardScript.ball.transform.position = new Vector3(3, 3, boardScript.ball.transform.position.z);
        }

        boardScript.bricks.RemoveAll(gameObject => gameObject == null); // remove any destroyed bricks

        if (0 == boardScript.bricks.Count)
        {
            boardScript.SetupScene(++level);
            SetLevelText(level);
        }

        //StartCoroutine(MoveEnemies());
    }

    //public void AddEnemyToList(Enemy script)
    //{
    //    enemies.Add(script);
    //}

    //IEnumerator MoveEnemies()
    //{
    //    enemiesMoving = true;
    //    yield return new WaitForSeconds(turnDelay);
    //    if (enemies.Count == 0)
    //    {
    //        yield return new WaitForSeconds(turnDelay);
    //    }

    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        enemies[i].MoveEnemy();
    //        yield return new WaitForSeconds(enemies[i].moveTime);
    //    }

    //    playersTurn = true;
    //    enemiesMoving = false;
    //}
}
