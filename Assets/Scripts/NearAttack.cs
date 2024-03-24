using UnityEngine;

public class NearAttack : MonoBehaviour
{
    public bool enemyNear;
    public bool playerOne, playerTwo;
    public GameObject enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerOne && collision.CompareTag("CatTwo"))
        {
            enemyNear = true;
        }
        else if (playerTwo && collision.CompareTag("CatOne"))
        {
            enemyNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerOne && collision.CompareTag("CatTwo"))
        {
            enemyNear = false;
        }
        else if (playerTwo && collision.CompareTag("CatOne"))
        {
            enemyNear = false;
        }
    }
}
