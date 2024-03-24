using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject farAttack;
    public Image healthBar;

    public int runSpeed = 10, jumpForce = 100;
    public bool playerOne, playerTwo, onBlock, onGround, onLeft, onRight;
    public int health = 100;

    void Start()
    {
        if (gameObject.CompareTag("CatOne"))
        {
            transform.GetChild(0).GetComponent<NearAttack>().playerOne = true;
            playerOne = true;
            transform.GetChild(0).GetComponent<NearAttack>().enemy = GameObject.FindGameObjectWithTag("CatTwo");
        }
        else if (gameObject.CompareTag("CatTwo"))
        {
            playerTwo = true;
            transform.GetChild(0).GetComponent<NearAttack>().playerTwo = true;
            transform.GetChild(0).GetComponent<NearAttack>().enemy = GameObject.FindGameObjectWithTag("CatOne");
        }
    }

    void AnimationReset(string animName)
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        //animator.SetBool("Crouch", false);
        animator.SetBool("Defend", false);
        animator.SetBool(animName, true);
    }

    void Update()
    {
        healthBar.fillAmount = (float)health / 100.0f;
        #region PlayerOneController
        if (playerOne)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                AnimationReset("Walk");
                NormalCollider();
                onBlock = false;
            }
            if (Input.GetKeyDown(KeyCode.W) && onGround == true)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                NormalCollider();
            }
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    animator.SetTrigger("DashAttack");
            //    NormalCollider();
            //    onBlock = false;
            //    if (transform.GetChild(0).GetComponent<NearAttack>().enemyNear)
            //    {
            //        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Cat>().health -= 20;
            //        if (transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Cat>().health <= 0)
            //        {
            //            transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetBool("Death", true);
            //        }
            //        else
            //        {
            //            transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
            //        }
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Attack");
                onBlock = false;
                Invoke(nameof(Attack), 0.5f);
            }
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    animator.SetBool("Walk", false);
            //    animator.SetBool("Idle", false);
            //    animator.SetBool("Crouch", true);
            //    animator.SetBool("Defend", false);
            //    CrouchCollider();
            //    onBlock = false;
            //}
            if (Input.anyKey == false)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                //animator.SetBool("Crouch", false);
                animator.SetBool("Defend", false);
                NormalCollider();
                onBlock = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Crouch", false);
                animator.SetBool("Defend", true);
                NormalCollider();
                onBlock = true;
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
        if (playerTwo)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                //animator.SetBool("Crouch", false);
                animator.SetBool("Defend", false);
                NormalCollider();
                onBlock = false;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                NormalCollider();
                onBlock = false;
            }
            //if (Input.GetKeyDown(KeyCode.DownArrow))
            //{
            //    animator.SetBool("Walk", false);
            //    animator.SetBool("Idle", false);
            //    animator.SetBool("Crouch", true);
            //    animator.SetBool("Defend", false);
            //    CrouchCollider();
            //    onBlock = false;
            //}
            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("Attack");
                NormalCollider();
                onBlock = false;
            }
            //if (Input.GetKeyDown(KeyCode.RightShift))
            //{
            //    animator.SetTrigger("DashAttack");
            //    NormalCollider();
            //    onBlock = false;
            //    if (transform.GetChild(0).GetComponent<NearAttack>().enemyNear)
            //    {
            //        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Cat>().health -= 20;
            //        if (transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Cat>().health <= 0)
            //        {
            //            transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetBool("Death", true);
            //        }
            //        else
            //        {
            //            transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
            //        }
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Crouch", false);
                animator.SetBool("Defend", true);
                NormalCollider();
                onBlock = true;
            }
            if (Input.anyKey == false)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                //animator.SetBool("Crouch", false);
                animator.SetBool("Defend", false);
                NormalCollider();
                onBlock = false;
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
                Invoke(nameof(Attack), 0.5f);
            }
        }
        #endregion
    }

    void Attack()
    {
        GameObject newFurball = Instantiate(farAttack, transform.position, Quaternion.identity);
        newFurball.SetActive(true);
        if (playerOne)
            newFurball.GetComponent<Furball>().enemyTag = "CatTwo";
        else if (playerTwo)
            newFurball.GetComponent<Furball>().enemyTag = "CatOne";
    }

    void CrouchCollider()
    {
        //GetComponent<BoxCollider2D>().size = new Vector2(0.237f, 0.163f);
        //GetComponent<BoxCollider2D>().offset = new Vector2(-0.07f, -0.141f);
    }

    void NormalCollider()
    {
        //GetComponent<BoxCollider2D>().size = new Vector2(0.237f, 0.36f);
        //GetComponent<BoxCollider2D>().offset = new Vector2(-0.07f, -0.04f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;
    }

}