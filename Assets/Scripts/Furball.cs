using UnityEngine;

public class Furball : MonoBehaviour
{
    public int fireSpeed = 10;
    public string enemyTag;

    void Update()
    {
        if (enemyTag == "CatOne")
        {
            if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Cat>().onLeft)
                transform.Translate(Vector3.left * Time.deltaTime * fireSpeed);
            else if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Cat>().onRight)
                transform.Translate(Vector3.right * Time.deltaTime * fireSpeed);
        }
        else if (enemyTag == "CatTwo")
        {
            if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Cat>().onLeft)
                transform.Translate(Vector3.left * Time.deltaTime * fireSpeed);
            else if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Cat>().onRight)
                transform.Translate(Vector3.right * Time.deltaTime * fireSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            if (collision.gameObject.GetComponent<Cat>().health > 0)
            {
                if (collision.gameObject.GetComponent<Cat>().onBlock)
                {
                    collision.gameObject.GetComponent<Cat>().health -= 2;
                }
                else
                {
                    collision.gameObject.GetComponent<Cat>().health -= 15;
                    collision.GetComponent<Animator>().SetTrigger("Hurt");
                }
                if (collision.gameObject.GetComponent<Cat>().health <= 0)
                {
                    collision.GetComponent<Animator>().SetBool("Death", true);
                    this.gameObject.SetActive(false);
                }
            }

        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
