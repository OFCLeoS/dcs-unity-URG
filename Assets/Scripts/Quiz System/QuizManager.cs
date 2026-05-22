using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    private Question currentQuestion;
    private bool quizActive = false;

    private HintLevel hintLevel = HintLevel.NO_HINT;

    [SerializeField] GameObject quizCanvas;
    [SerializeField] QuizUI quizUI;

    [SerializeField] Player player;

    public Question GetCurrentQuestion() => currentQuestion;

    public bool IsQuizActive() => quizActive;

    public HintLevel GetHintLevel() => hintLevel;
    public HintLevel SetHintLevel(HintLevel hintLevel) => this.hintLevel = hintLevel;

    int correctAnswersCount;
    int wrongAnswersCount;
    String zeroMinutesString = "";
    String zeroSecondsString = "";

    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] ColliderActivator hubMapElevatorActivator;

    [SerializeField] float minutes;

    [SerializeField] float seconds;

    [SerializeField] QuizComputer quizComputer;

    float timeLeft;

    [SerializeField] PlayerUIBehaviour playerUIBehaviour;
    [SerializeField] TextMeshProUGUI anwserEffectText;

    void Awake()
    {
        correctAnswersCount = 0;
        wrongAnswersCount = 0;
        // TODO: STREAMING ASSET PATH NOT AVAILABLE ON RUNTIME?!
        string questionPath = System.IO.Path.Combine(Application.streamingAssetsPath, "QuizQuestions.txt");
        string contentPath = System.IO.Path.Combine(Application.streamingAssetsPath, "QuizContent.txt");

        QuestionList.LoadQuestionsFromFile(questionPath);
        QuestionList.LoadContentFromFile(contentPath);

        //Debug.Log("Question File exists: " + System.IO.File.Exists(questionPath));
        //Debug.Log("Content File exists: " + System.IO.File.Exists(contentPath));

        quizUI.CloseQuizUI();
    }

    public void StartQuiz() // Call this when the player interacts with the pc/quiz system
    {
        timeLeft = minutes * 60 + seconds;
        if (quizActive) return;
        if (QuestionList.GetSize() == 0)
        {
            Debug.Log("No Questions loaded! Cannot start Quiz.");
            return;
        }

        player.DisableControls();
        player.CrosshairController.ShowCursor();

        int randomQuestionIndex = Random.Range(0, QuestionList.GetSize());
        currentQuestion = QuestionList.GetQuestionFromIndex(randomQuestionIndex);

        quizActive = true;

        quizCanvas.SetActive(true);

        //Debug.Log("Quiz Started!");
        //Debug.Log("Question: " + currentQuestion);
        quizUI.OpenQuizUI();
    }

    public void EndQuiz()
    {
        quizUI.CloseQuizUI();
        player.EnableControls();
        player.CrosshairController.HideCursor();
        currentQuestion = null;
        quizActive = false;
        hintLevel = HintLevel.NO_HINT;
        quizCanvas.SetActive(false);
        quizComputer.DisableQuizComputer();
        hubMapElevatorActivator.EnableActivator();
        playerUIBehaviour.SetObjectiveText("Take the Elevator");
    }

    public void SendAnswer(int choiceIndex)
    {
        if (!quizActive) return;

        if (choiceIndex == currentQuestion.GetCorrectIndex()) CorrectAnswerChosen();
        else WrongAnswerChosen();
    }

    void CorrectAnswerChosen()
    {
        correctAnswersCount++;
        anwserEffectText.alpha = 255;
        anwserEffectText.color = Color.green;
        anwserEffectText.text = "Correct Anwser!";
        Debug.Log("Correct!");
        EndQuiz();
    }

    void WrongAnswerChosen()
    {
        wrongAnswersCount++;
        anwserEffectText.alpha = 255;
        anwserEffectText.color = Color.red;
        anwserEffectText.text = "Wrong Anwser!\n-15 HP";
        Debug.Log("Wrong!");
        player.StatusEffectController.AddDebuff(StatusEffectFactory.CreateRandomDebuff(player));
        EndQuiz();
    }

    public void NextHint()
    {
        if (!quizActive)
        {
            Debug.Log("Quiz is not active!");
            return;
        }

        if ((int)hintLevel > System.Enum.GetValues(typeof(HintLevel)).Length)
        {
            Debug.Log("You already have all Hints unlocked!");
        }
        else
        {
            hintLevel += 1;
            Debug.Log("Hint Level went up! " + hintLevel + " HintLevel");
        }
    }

    public string GetHint()
    {
        if (currentQuestion == null)
        {
            Debug.Log("No Question available!");
            return "";
        }

        switch (hintLevel)
        {
            case HintLevel.NO_HINT: return "No hint unlocked yet!";
            case HintLevel.TOPIC: return "Topic: " + currentQuestion.GetTopic();
            case HintLevel.SUB_TOPIC: return "Topic: " + currentQuestion.GetTopic() + ", SubTopic: " + currentQuestion.GetSubtopic();
            case HintLevel.PARAGRAPH: return "Topic: " + currentQuestion.GetTopic() + ", SubTopic: " + currentQuestion.GetSubtopic() + ", Paragraph Number: " + currentQuestion.GetParagraphNumber();
            default:
                {
                    Debug.LogError("INVALID HINT LEVEL!");
                    return "";
                }
        }
    }

    public void Countdown()
    {
        timeLeft -= Time.deltaTime;
        if (TimeSpan.FromSeconds(timeLeft).Minutes <= 9)
        {
            zeroMinutesString = "0";
        }
        else
        {
            zeroMinutesString = "";
        }
        if (TimeSpan.FromSeconds(timeLeft).Seconds <= 9)
        {
            zeroSecondsString = "0";
        }
        else
        {
            zeroSecondsString = "";
        }
        if (timeLeft <= 0)
        {
            timerText.text = "";
            zeroMinutesString = "";
            zeroSecondsString = "";
            WrongAnswerChosen();
        }
        else
        {
            timerText.text = timerText.text = zeroMinutesString + Convert.ToString(TimeSpan.FromSeconds(timeLeft).Minutes) + ":" + zeroSecondsString + Convert.ToString(TimeSpan.FromSeconds(timeLeft).Seconds);
        }
    }

    void Update()
    {
        if (quizActive) Countdown();
    }

}
