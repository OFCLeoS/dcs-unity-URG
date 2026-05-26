using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    [SerializeField] GameObject quizWindow;
    [SerializeField] TMP_Text hintText;
    [SerializeField] TMP_Text question;
    [SerializeField] Button[] answerButtons;
    [SerializeField] QuizManager quizManager;

    public void OpenQuizUI()
    {
        quizWindow.SetActive(true);
        RefreshUI();
    }

    public void CloseQuizUI()
    {
        quizWindow.SetActive(false);
    }

    void RefreshUI()
    {
        Question question = quizManager.GetCurrentQuestion();
        if (quizManager.GetCurrentQuestion() == null) return;

        this.question.text = question.GetQuestion();
        hintText.text = quizManager.GetHint();

        int answerButtonsSize = answerButtons.Length;
        for (int i = 0; i < answerButtonsSize; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.GetChoices()[i];

            // local coy of i because of how closure works in cs, without this every button would send index 6
            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerButtonClicked(index));
        }
    }

    void OnAnswerButtonClicked(int choiceIndex)
    {
        quizManager.SendAnswer(choiceIndex);
    }

    // call this when wave/objective system called quizManager.NextHint()
    public void RefreshHint()
    {
        hintText.text = quizManager.GetHint();
    }
}
