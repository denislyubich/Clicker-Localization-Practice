using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected float startingTimer = 10;
    protected bool isGameOver;

    protected void GameOver()
    {
        Debug.Log("Game over!");
        isGameOver = true;
    }
}
