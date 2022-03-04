using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SSgameManager : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private List<SymCard> symCards;
    [SerializeField] private SymQuestion currentQuestion;
    [SerializeField] private Renderer[] planes;
    [SerializeField] private TMP_Text messageBlockText;
    [SerializeField] private TMP_Text redText, blueText;

    private bool success = false, clickedOnceToCheckFlag = false;

    private void Awake()
    {
        playerCamera = player.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();
        QuestionLoader();
        yield return new WaitForEndOfFrame();
        SetUpPlaneSprites();
        yield return new WaitForEndOfFrame();
        SetupQuestionTextBox();
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (clickedOnceToCheckFlag) return;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit rayHit))
            {
                var b = CheckAnswer(rayHit.collider.gameObject);

                switch(b)
                {
                    case 2: break;
                    case 1:
                        messageBlockText.text = "Failed";
                        success = false;
                        EndGame();
                        break;
                    case 0:
                        messageBlockText.text = "Succeeded";
                        success = true;
                        EndGame();
                        break;
                    default:
                        break;
                }
            }
        }

    }

    private void QuestionLoader()
    {
        var temp = new List<SymCard>(symCards); // clone
        currentQuestion.answers = new List<SymCard>();

        //add the correct one sprite to the array.
        //var num = Random.Range(0, temp.Count);
        var num = GameHandler.currentDataForGame;
        currentQuestion.questionCard = temp[num];
        currentQuestion.answers.Add(temp[num]);
        temp.RemoveAt(num);

        //add wrong answers.
        for (var i = 0; i < 2; i++)
        {
            num = Random.Range(0, temp.Count);
            currentQuestion.answers.Add(temp[num]);
            temp.RemoveAt(num);
        }

        //fake shuffle.
        for (int i = 0; i < currentQuestion.answers.Count; i++)
        {
            var temp2 = currentQuestion.answers[i];
            int randomIndex = Random.Range(i, currentQuestion.answers.Count);
            currentQuestion.answers[i] = currentQuestion.answers[randomIndex];
            currentQuestion.answers[randomIndex] = temp2;
        }

    }

    private void SetUpPlaneSprites()
    {
        for (var i = 0; i < planes.Length; i++)
        {
            planes[i].material.mainTexture = currentQuestion.answers[i].sprite;
            currentQuestion.answers[i].goName = planes[i].name;
        }
    }


    private void SetupQuestionTextBox()
    {
        blueText.text = currentQuestion.questionCard.blueText;
        redText.text = currentQuestion.questionCard.redText;
    }

    private int CheckAnswer(GameObject go)
    {
        
        var c = 0;
        foreach(var n in planes)
        {
            if (n.name != go.name)
                c++;
        }
        if (c >= planes.Length) return 2; // not something to check
        clickedOnceToCheckFlag = true;
        if (go.name == currentQuestion.questionCard.goName) // is this correct?
            return 0; // yes
        return 1; // no
    }

    private void EndGame()
    {
        if (success)
        {
            GameHandler.gatheredKeys++;
            GameHandler.gotKeys[GameHandler.currentDataForGame + 3] = true; // 0 1 2 || 3 4 5 6 7 8
            // 0 1 2
            // 0 1 2 3 4 5 
        }
            
        messageBlockText.transform.parent.gameObject.SetActive(true);
        StartCoroutine(SendPlayerToWorld());
    }

    private IEnumerator SendPlayerToWorld()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(GameHandler.worldSceneName);
    }
}

[System.Serializable]
public class SymCard
{
    public string name, blueText, redText, goName;
    public Texture sprite;
}


[System.Serializable]
public class SymQuestion
{
    public SymCard questionCard;
    public List<SymCard> answers;
}