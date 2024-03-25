using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerOnePos, playerTwoPos;
    public Cat catScriptOne, catScriptTwo;

    void Update()
    {
        if(playerOnePos.position.x < playerTwoPos.position.x)
        {
            //player one left
            playerOnePos.localScale = new Vector3(10, 10, 10);
            playerOnePos.gameObject.GetComponent<Cat>().onLeft = true;
            playerOnePos.gameObject.GetComponent<Cat>().onRight = false;
            //
            playerTwoPos.localScale = new Vector3(-10, 10, 10);
            playerTwoPos.gameObject.GetComponent<Cat>().onLeft = false;
            playerTwoPos.gameObject.GetComponent<Cat>().onRight = true;
        }
        else
        {
            playerOnePos.localScale = new Vector3(-10, 10, 10);
            playerOnePos.gameObject.GetComponent<Cat>().onLeft = false;
            playerOnePos.gameObject.GetComponent<Cat>().onRight = true;
            //
            playerTwoPos.gameObject.GetComponent<Cat>().onLeft = true;
            playerTwoPos.gameObject.GetComponent<Cat>().onRight = false;
            playerTwoPos.localScale = new Vector3(10, 10, 10);
        }

        if (catScriptTwo.catOneDead)
            catScriptOne.enabled = false;
        if (catScriptOne.catTwoDead)
            catScriptTwo.enabled = false;
    }
}
