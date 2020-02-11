using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)

        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count brickCount = new Count(5, 9);
    //public Count foodCount = new Count(1, 5);
    //public GameObject exit;
    //public GameObject[] floorTitles;
    public GameObject[] brickTiles;
    //public GameObject[] foodTiles;
    //public GameObject[] enemyTiles;
    public GameObject[] boundryTiles;

    public GameObject ball;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    private List<Vector3> validBrickPositions = new List<Vector3>();

    void InitalizeList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                Vector3 gridPosition = new Vector3(x, y, 0f);
                gridPositions.Add(gridPosition);
                if(y > columns / 2)
                {
                    validBrickPositions.Add(gridPosition);
                }
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {             
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    GameObject toInstantiate = boundryTiles[Random.Range(0, boundryTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    /// <summary>
    /// Finds a random item in the given List<Vector3> then removes and returns it.
    /// </summary>
    /// <returns>A random Vector3 from the given grid input.</returns>
    Vector3 RandomPosition(List<Vector3> grid)
    {
        int randomIndex = Random.Range(0, grid.Count);
        Vector3 randomPosition = grid[randomIndex];
        grid.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition(validBrickPositions);
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitalizeList();
        LayoutObjectAtRandom(brickTiles, brickCount.minimum, brickCount.maximum);

        GameObject gameBall = Instantiate(ball, new Vector3(rows / 2, 1, 0f), Quaternion.identity) as GameObject;
        gameBall.transform.SetParent(boardHolder);
        Rigidbody2D ballRigidBody2D = gameBall.GetComponent<Rigidbody2D>();
        ballRigidBody2D.AddForce(new Vector2(Random.Range(130, 180), Random.Range(130, 180)));

        //int enemyCount = (int)Mathf.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0F), Quaternion.identity);
    }
}
