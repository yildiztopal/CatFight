using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public Animator animator;
    public GameObject farAttack;
    public Image healthBar;
    
    public bool catOne, catTwo, onDefend, onGround, onLeft, onRight, catOneDead, catTwoDead;
    public int health = 100;

    
    Rigidbody2D rb;
    NearAttack nearAttack1, nearAttack2;

    int runSpeed = 10, jumpForce = 350;

    void Start()
    {
        if (gameObject.CompareTag("CatOne"))
        {
            catOne = true;
            rb = GetComponent<Rigidbody2D>();
            nearAttack1 = transform.GetChild(0).GetComponent<NearAttack>();
            nearAttack1.playerOne = true;
            nearAttack1.enemy = GameObject.FindGameObjectWithTag("CatTwo");

        }
        else if (gameObject.CompareTag("CatTwo"))
        {
            catTwo = true;
            rb = GetComponent<Rigidbody2D>();
            nearAttack2 = transform.GetChild(0).GetComponent<NearAttack>();
            nearAttack2.playerTwo = true;
            nearAttack2.enemy = GameObject.FindGameObjectWithTag("CatOne");
        }
    }

    void AnimationReset(string animName)
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Defend", false);
        animator.SetBool(animName, true);
    }

    void Update()
    {
        healthBar.fillAmount = health / 100.0f;

        #region PlayerOneController
        if (catOne)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                AnimationReset("Walk");
                onDefend = false;
            }
            if (Input.GetKeyDown(KeyCode.W) && onGround == true)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("nearAttack");
                onDefend = false;

                if (nearAttack1.enemyNear)
                {
                    nearAttack1.enemy.GetComponent<Cat>().health -= 20;
                    if (nearAttack1.enemy.GetComponent<Cat>().health <= 0)
                    {
                        nearAttack1.enemy.GetComponent<Animator>().SetBool("Death", true);
                        catTwoDead = true;
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Attack");
                onDefend = false;
                Invoke(nameof(Attack), 0.1f);
            }
            if (Input.anyKey == false)
            {
                AnimationReset("Idle");
                onDefend = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                AnimationReset("Defend");
                onDefend = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * runSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * runSpeed);
            }
        }
        #endregion
        #region PlayerTwoController
        if (catTwo)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                AnimationReset("Walk");
                onDefend = false;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                onDefend = false;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("Attack");
                onDefend = false;
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                animator.SetTrigger("nearAttack");
                onDefend = false;

                if (nearAttack2.enemyNear)
                {
                    nearAttack2.enemy.GetComponent<Cat>().health -= 20;
                    if (nearAttack2.enemy.GetComponent<Cat>().health <= 0)
                    {
                        nearAttack2.enemy.GetComponent<Animator>().SetBool("Death", true);
                        catOneDead = true;
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AnimationReset("Defend");
                onDefend = true;
            }
            if (Input.anyKey == false)
            {
                AnimationReset("Idle");
                onDefend = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * runSpeed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * runSpeed);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Invoke(nameof(Attack), 0.1f);
            }
        }
        #endregion
    }

    void Attack()
    {
        GameObject newFurball = Instantiate(farAttack, transform.position + new Vector3(0,0,0.5f), Quaternion.identity);
        newFurball.SetActive(true);
        if (catOne)
        {
            newFurball.GetComponent<Furball>().enemyTag = "CatTwo";
        } 
        else if (catTwo)
        {
            newFurball.GetComponent<Furball>().enemyTag = "CatOne";
            newFurball.transform.localScale = new Vector3(-newFurball.transform.localScale.x,
                newFurball.transform.localScale.y, newFurball.transform.localScale.z);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;
    }

}