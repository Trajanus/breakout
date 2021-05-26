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
    public Count brickCount = new Count(10, 30);
    public GameObject[] brickTypeTiles;
    public List<GameObject> bricks;
    public GameObject[] boundryTiles;

    public int MaximumValidBrickY;
    public int MinimumValidBrickY;

    public int MaximumValidBrickX;
    public int MinimumValidBrickX;

    public Vector2 BallStartLocation = new Vector2(3, 3);

    public GameObject ball;

    private List<Vector3> validBrickPositions = new List<Vector3>();

    void InitializeValidBrickLocations()
    {
        validBrickPositions.Clear();
        for (int x = MinimumValidBrickX; x <= MaximumValidBrickX; x++)
        {
            for (int y = MinimumValidBrickY; y <= MaximumValidBrickY; y++)
            {
                Vector3 gridPosition = new Vector3(x, y, 0f);
                validBrickPositions.Add(gridPosition);
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
        int objectCount = Random.Range(minimum, maximum + 1); // Plus one is due to Maximum of Random.Range being exclusive, but we want to include our maximum value.

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition(validBrickPositions);
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            bricks.Add(Instantiate(tileChoice, randomPosition, Quaternion.identity));
        }
    }

    public void SetupScene(int level)
    {
        bricks = new List<GameObject>();
        InitializeValidBrickLocations();
        LayoutObjectAtRandom(brickTypeTiles, brickCount.minimum, brickCount.maximum);

        if (1 == level)
        {
            ball = Instantiate(ball, new Vector3(rows / 2, 1, 0f), Quaternion.identity) as GameObject;
            ball.name = Ball.Name;
        }
        else
        {
            ball.transform.position = new Vector3(BallStartLocation.x, BallStartLocation.y, ball.transform.position.z);
        }

        Rigidbody2D ballRigidBody2D = ball.GetComponent<Rigidbody2D>();
        ballRigidBody2D.AddForce(new Vector2(Random.Range(330, 380), Random.Range(330, 380)));
    }
}
