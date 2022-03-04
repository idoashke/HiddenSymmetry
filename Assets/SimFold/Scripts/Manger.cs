using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//the manger control the random fold (turos,ring,klein)
//the manger control if clicked object on plane is the currect answer 

public class Manger : MonoBehaviour
{
    [SerializeField] private GameObject[] itemstype;
    private bool clickedOnceToCheckFlag = false, success = false;
    [SerializeField] private TMP_Text messageBlockText;

    public int gametype;
    private string[] answer;
    public void CheckAnswer (string obj)
    {
        if (clickedOnceToCheckFlag) return; // dont check again, because it was already checked.
        int counter = 0;
        for(int i=0;i<answer.Length;i++) // check if we clicked right thing to check.
        {
            if (obj != answer[i])
            {
                counter++;
            }  
        }
        if(counter==answer.Length)
        {
            return;
        }

        if(obj==answer[gametype])
        {
            print("Correct answer ");
            messageBlockText.text = "Succeeded";
            success = true;
        }
        else
        {
            print("Wrong answer");
            messageBlockText.text = "Failed";
            success = false;
        }
        EndGame();

    }
    void Awake()
    {
        answer = new string[3];
        for(int i=0;i<itemstype.Length;i++)
        {
            answer[i] = itemstype[i].name;
        }
        //gametype =Random.Range(0, 3);
        gametype = GameHandler.currentDataForGame;
    }

    private void EndGame()
    {
        if (success)
        {
            GameHandler.gatheredKeys++;
            GameHandler.gotKeys[GameHandler.currentDataForGame] = true; // 0 1 2 || 3 4 5 6 7 8
            // 0 1 2
            // 0 1 2 3 4 5 
        }
        clickedOnceToCheckFlag = true;
        messageBlockText.transform.parent.gameObject.SetActive(true);
        StartCoroutine(SendPlayerToWorld());
    }

    private IEnumerator SendPlayerToWorld()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(GameHandler.worldSceneName);
    }

}
