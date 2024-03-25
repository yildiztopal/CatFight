using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText, roundStartText, roundEndText;
    public int timer = 50;
    public GameObject levelEndPanel, playerOne, playerTwo;

    void Start()
    {
        playerOne.GetComponent<Cat>().enabled = false;
        playerTwo.GetComponent<Cat>().enabled = false;
        levelEndPanel.SetActive(false);
        timerText.text = timer.ToString();
        StartCoroutine(RoundStart());
    }

    IEnumerator RoundStart()
    {
        roundStartText.text = "Game starts in";
        roundStartText.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1);
        yield return new WaitForSeconds(1);
        roundStartText.gameObject.transform.localScale = Vector3.one;
        roundStartText.gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f);
        roundStartText.text = "3";
        yield return new WaitForSeconds(0.5f);
        roundStartText.gameObject.transform.localScale = Vector3.one;
        roundStartText.gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f);
        roundStartText.text = "2";
        yield return new WaitForSeconds(0.5f);
        roundStartText.gameObject.transform.localScale = Vector3.one;
        roundStartText.gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f);
        roundStartText.text = "1";
        yield return new WaitForSeconds(0.5f);
        roundStartText.gameObject.transform.localScale = Vector3.one;
        roundStartText.gameObject.transform.DOScale(Vector3.one * 1.2f, 1.0f);
        roundStartText.text = "Fight";
        yield return new WaitForSeconds(1.0f);
        roundStartText.text = "";
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        playerOne.GetComponent<Cat>().enabled = true;
        playerTwo.GetComponent<Cat>().enabled = true;
        yield return new WaitForSeconds(1);
        timer--;

        timerText.text = timer.ToString();
        if(timer == 0)
        {
            levelEndPanel.SetActive(true);            

            playerOne.GetComponent<Cat>().enabled = false;
            playerTwo.GetComponent<Cat>().enabled = false;
            StartCoroutine(RoundWinnerCheck());
        }
        else if (timer < 4)
        {
            timerText.color = Color.red;
            StartCoroutine(Timer());
        }
        else
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator RoundWinnerCheck()
    {
        yield return new WaitForSeconds(0);

        if(playerOne.GetComponent<Cat>().health > playerTwo.GetComponent<Cat>().health)
        {
            roundEndText.text = "Player One Wins";
        }
        else if (playerOne.GetComponent<Cat>().health == playerTwo.GetComponent<Cat>().health)
        {
            roundEndText.text = "Draw";
        }
        else if (playerOne.GetComponent<Cat>().health < playerTwo.GetComponent<Cat>().health)
        {
            roundEndText.text = "Player Two Wins";
        }
        yield return new WaitForSeconds(2);

        roundEndText.text = "Game Over";
    }
}
