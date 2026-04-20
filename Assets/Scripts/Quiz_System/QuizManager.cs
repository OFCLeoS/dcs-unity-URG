using UnityEngine;
using System.Collections.Generic;
using System;

public class QuizManager : MonoBehaviour
{
    // add checks if question is not null and if quiz is active !!!!!!!!!!

    private Question currentQuestion;
    private bool quizActive = false;
    private int hintLevel = 0; //0 = no hint, 1 = topic, 2 = subtopic, 3 = paragraph

    public Question getCurrentQuestion()
    {
        return currentQuestion;
    }

    public bool getQuizActive()
    {
        return quizActive;
    }

    public int getHintLevel()
    {
        return hintLevel;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        string questionPath = System.IO.Path.Combine(Application.streamingAssetsPath, "QuizQuestions.txt");
        string contentPath = System.IO.Path.Combine(Application.streamingAssetsPath, "QuizContent.txt");

        //QuestionList.LoadQuestionsFromFile("QuizQuestions.txt");
        //QuestionList.LoadContentFromFile("QuizContent.txt");
        QuestionList.LoadQuestionsFromFile(questionPath);
        QuestionList.LoadContentFromFile(contentPath);

        //Debug.Log("Question File exists: " + System.IO.File.Exists("QuizQuestion.txt"));
        //Debug.Log("Content File exists: " + System.IO.File.Exists("QuizContent.txt"));
        Debug.Log("Question File exists: " + System.IO.File.Exists(questionPath));
        Debug.Log("Content File exists: " + System.IO.File.Exists(contentPath));
    }

    public void StartQuiz() // call this when the player interacts with the pc/quiz system
    {
        if (QuestionList.GetSize() == 0)
        {
            Debug.Log("No Quesitons loaded! Cannot start Quiz.");
            return;
        }

        System.Random rand = new System.Random();

        int randomQuestionIndex = rand.Next(0, QuestionList.GetSize());
        currentQuestion = QuestionList.GetQuestionFromIndex(randomQuestionIndex);

        quizActive = true;
        hintLevel = 0;

        Debug.Log("Quiz Started!");
        Debug.Log("Question: " + currentQuestion);
    }

    public void EndQuiz()
    {
        currentQuestion = null;
        quizActive = false;
        hintLevel = 0;

        Debug.Log("Quiz Ended!");
    }

    public void SendAnswer(int choiceIndex)
    {
        if (!quizActive)
        {
            Debug.Log("Quiz is not active!");
            return;
        }

        if (choiceIndex == currentQuestion.GetCorrectIndex())
        {
            Debug.Log("Correct!");
            EndQuiz();
        } else
        {
            Debug.Log("Wrong!");
            Debug.Log("Punishment Incoming!");
            Punishment();
        }
    }

    void Punishment()
    {
        Debug.Log("punishment not finished!");
    }

    public void NextHint()
    {
        if(!quizActive)
        {
            Debug.Log("Quiz is not active!");
            return;
        }

        if (hintLevel >= 3)
        {
            Debug.Log("You already have all Hints unlocked!");
        }

        hintLevel += 1;

        Debug.Log("Hint Level went up! " + hintLevel + " HintLevel");
    }

    public string GetHint()
    {
        if (currentQuestion == null)
        {
            Debug.Log("No Question available!");
            return "";
        }

        if (hintLevel == 0)
        {
            return "No hint unlocked yet!";
        }
        else if (hintLevel == 1)
        {
            return "Topic: " + currentQuestion.GetTopic();
        }
        else if (hintLevel == 2)
        {
            return "Topic: " + currentQuestion.GetTopic() + ", SubTopic: " + currentQuestion.GetSubtopic();
        }
        else //(hintLevel == 3)
        {
            return "Topic: " + currentQuestion.GetTopic() + ", SubTopic: " + currentQuestion.GetSubtopic() + ", Paragraph Number: " + currentQuestion.GetParagraphNumber();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
