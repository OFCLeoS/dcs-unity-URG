using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    public GameObject quizWindow;
    public TMP_Text hintText;
    public TMP_Text question;
    public Button[] answerButtons = new Button[6];
    public QuizManager quizManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quizWindow.SetActive(false);

        // this is for tests only, when the quiz whould pop up, it should call OpenQuiz()!!
        OpenQuiz();
    }

    public void OpenQuiz()
    {
        quizManager.StartQuiz();
        quizWindow.SetActive(true);

        RefreshUI();
    }

    public void CloseQuiz()
    {
        quizManager.EndQuiz();
        quizWindow.SetActive(false);
    }

    void RefreshUI()
    {
        if (quizManager.getCurrentQuestion() == null)
        {
            Debug.Log("currentQuestion is null!");
        }

        Question q = quizManager.getCurrentQuestion();
        question.text = q.GetQuestion();
        hintText.text = quizManager.GetHint();

        int answerButtonsSize = answerButtons.Length;
        for (int i = 0; i < answerButtonsSize; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = q.GetChoices()[i];

            // local coy of i because of how closure works in cs, without this every button would send index 6
            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerButtonClicked(index));
        }
    }

    void OnAnswerButtonClicked(int choiceIndex)
    {
        Debug.Log("Button number clicked: " + choiceIndex);

        quizManager.SendAnswer(choiceIndex);

        if (!quizManager.getQuizActive())
        {
            CloseQuiz();
        }
        else
        {
            RefreshUI();
        }
    }

    // call this when wave/objective system called quizManager.NextHint()
    public void RefreshHint()
    {
        hintText.text = quizManager.GetHint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
