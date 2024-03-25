using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText, roundStartText, roundEndText;
    public int timer = 15, roundNo = 1;
    public GameObject levelEndPanel, playerOne, playerTwo;

    void Start()
    {
        roundNo = PlayerPrefs.GetInt("RoundNumber", 1);
        playerOne.GetComponent<Cat>().enabled = false;
        playerTwo.GetComponent<Cat>().enabled = false;
        levelEndPanel.SetActive(false);
        timerText.text = timer.ToString();
        StartCoroutine(RoundStart());
    }

    IEnumerator RoundStart()
    {
        roundStartText.text = "Round " + roundNo.ToString();
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
        //sure basladi
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        playerOne.GetComponent<Cat>().enabled = true;
        playerTwo.GetComponent<Cat>().enabled = true;
        yield return new WaitForSeconds(1);
        timer--;
        //timer -= 1;
        //timer = timer - 1;
        timerText.text = timer.ToString();
        if(timer == 0)
        {
            //round finish kodu gelecek
            print("sure bitti");
            levelEndPanel.SetActive(true);            
            //oyun mekaniklerini de durdur
            playerOne.GetComponent<Cat>().enabled = false;
            playerTwo.GetComponent<Cat>().enabled = false;
            RoundWinnerCheck();
            StartCoroutine(RoundWinnerCheck());
        }
        else if (timer < 4)
        {
            timerText.color = Color.red;
            StartCoroutine(Timer());
            //StartCoroutine(Timer());
        }
        else if(timer < 0)
        {
            //timerText.color = Color.red;
            //StartCoroutine(Timer());
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
        if (roundNo == 2)
        {
            //oyun bitti sonucu goster
            roundEndText.text = "Game Over";
            //
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetInt("RoundNumber", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//ayni leveli tekrar baslatmak icin
        }
        if (roundNo == 1)
        {
            //oyun bitti sonucu goster
            roundNo++;
            PlayerPrefs.SetInt("RoundNumber", roundNo);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//ayni leveli tekrar baslatmak icin
        }        
    }
}
