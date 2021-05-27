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
    public int playerBallCount = 5;

    public double LeftLevelEdge = -4.4;
    public double RightLevelEdge = 11.2;

    public AudioClip ballLoss;

    public AudioClip levelClip;
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;
    public AudioClip six;
    public AudioClip seven;
    public AudioClip eight;
    public AudioClip nine;
    public AudioClip ten;
    public AudioClip aLot;

    public GameObject magneticPowerup;

    private Text ballCountText;
    private Text levelText;

    public int Level { get; private set; } = 1;

    private bool enemiesMoving;
    private bool doingSetup;
    private bool gameover = false;

    private List<GameObject> powerups;

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

    private void Start()
    {
        PlayLevelClips();
    }

    public void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        Level++;
        InitGame();
    }

    void InitGame()
    {
        Level = 1;
        playerBallCount = 5;
        doingSetup = true;

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        ballCountText = GameObject.Find("BallCountText").GetComponent<Text>();
        SetLevelText(Level);
        SetBallCountText(playerBallCount);

        //enemies.Clear();
        boardScript.SetupScene(Level);
        powerups = new List<GameObject>(GameObject.FindGameObjectsWithTag("Powerup"));
    }

    public void GameOver()
    {
        gameover = true;
        levelText.text = $"You made it to level {Level}.";
    }

    private void SetLevelText(int levelCount)
    {
        levelText.text = "Level " + levelCount;
    }

    private void SetBallCountText(int ballCount)
    {
        ballCountText.text = "Balls: " + ballCount;
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemiesMoving || doingSetup)
        //{
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!gameover)
        {
            if (boardScript.ball.transform.position.y < -5)
            {
                PlayerBallLost();
            }

            boardScript.bricks.RemoveAll(gameObject => gameObject == null); // remove any destroyed bricks
            if (0 == boardScript.bricks.Count)
            {
                boardScript.SetupScene(++Level);
                SetLevelText(Level);

                PlayLevelClips();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameover = false;
                InitGame();
            }
        }

        powerups.RemoveAll(gameObject => gameObject == null);
        if (0 == powerups.Count)
        {
            var powerup = Instantiate(magneticPowerup, new Vector3(4, 4, 0), Quaternion.identity);
            powerups.Add(powerup);
        }

        //StartCoroutine(MoveEnemies());
    }

    private void PlayerBallLost()
    {
        SoundManager.instance.PlayOneShot(ballLoss);
        playerBallCount--;
        SetBallCountText(playerBallCount);

        if (playerBallCount <= 0)
        {
            GameOver();
        }
        else
        {
            var ballrb = boardScript.ball.GetComponent<Rigidbody2D>();
            ballrb.velocity = Vector2.zero;

            if (1 == random.Next(2))
            {
                ballrb.AddForce(new Vector2(UnityEngine.Random.Range(-330, -380), UnityEngine.Random.Range(330, 380)));
            }
            else
            {
                ballrb.AddForce(new Vector2(UnityEngine.Random.Range(330, 380), UnityEngine.Random.Range(330, 380)));
            }
            boardScript.ball.transform.position = new Vector3(3, 3, boardScript.ball.transform.position.z);
        }
    }

    private void PlayLevelClips()
    {
        AudioClip[] levelAudio = new AudioClip[2];
        levelAudio[0] = levelClip;
        levelAudio[1] = GetLevelNumberClip();
        StartCoroutine(SoundManager.instance.PlayAudioSequentially(levelAudio));
    }

    private AudioClip GetLevelNumberClip()
    {
        AudioClip levelNumberClip;
        switch (instance.Level)
        {
            case 1: levelNumberClip = one; break;
            case 2: levelNumberClip = two; break;
            case 3: levelNumberClip = three; break;
            //case 4: levelNumberClip = four; break;
            //case 5: levelNumberClip = five; break;
            //case 6: levelNumberClip = six; break;
            //case 7: levelNumberClip = seven; break;
            //case 8: levelNumberClip = eight; break;
            //case 9: levelNumberClip = nine; break;
            //case 10: levelNumberClip = ten; break;
            default: levelNumberClip = aLot; break;
        }

        return levelNumberClip;
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
