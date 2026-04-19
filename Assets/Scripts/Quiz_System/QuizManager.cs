using UnityEngine;
using System.Collections.Generic;
using System;

public class QuizManager : MonoBehaviour
{
    // add checks if question is not null and if quiz is active !!!!!!!!!!

    private Question currentQuestion;
    private bool quizActive = false;
    private int hintLevel = 0; //0 = no hint, 1 = topic, 2 = subtopic, 3 = paragraph
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestionList.LoadQuestionsFromFile("QuizQuestions.txt");
    }

    public void StartQuiz() // call this when the player interacts with the pc/quiz system
    {
        // check if list is not empty first !!!!!!!!!!!!!!!!!!!!
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
        if (hintLevel >= 3)
        {
            Debug.Log("You already have all Hints unlocked!");
        }

        hintLevel += 1;

        Debug.Log("Hint Level went up! " + hintLevel + " HintLevel");
    }

    public string GetHint()
    {
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
