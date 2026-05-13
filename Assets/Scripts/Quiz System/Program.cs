//using UnityEngine;

using System.Collections.Generic;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        //Console.WriteLine("TEST");
        //Question q1 = new Question("What is AI?", "AI", "tokens", 1, new string[] { "a", "b", "c", "d", "e", "f" }, 0);
        //Question q2 = new Question("What is Security?", "Security", "permissions", 3, new string[] { "a", "b", "c", "d", "e", "f" }, 2);
        //Question q3 = new Question("What is Software?", "Software", "threads", 2, new string[] { "a", "b", "c", "d", "e", "f" }, 5);
        //Question q4 = new Question("What is AI2?", "AI", "ml", 67, new string[] { "a", "b", "c", "d", "e", "f" }, 3);
//
        //QuestionList.AddQuestion(q1);
        //QuestionList.AddQuestion(q2);
        //QuestionList.AddQuestion(q3);
        //QuestionList.AddQuestion(q4);

        QuestionList.LoadQuestionsFromFile("QuizQuestions.txt");
        List<Question> lTestLoadQuestions = QuestionList.GetTopicQuestions("AI");
        foreach (Question question in lTestLoadQuestions)
        {
            Console.Write(question);
        }


        //List<Question> lTestTopicQuestions = QuestionList.GetTopicQuestions("AI");
        //foreach (Question question in lTestTopicQuestions)
        //{
        //    Console.WriteLine(question);
        //}
//
        //List<Question> lTestSubtopicQuestions = QuestionList.GetSubtopicQuestions("Software", "threads");
        //foreach (Question question in lTestSubtopicQuestions)
        //{
        //    Console.WriteLine(question);
        //}
//
        //List<Question> lWrongTopicQuestions = QuestionList.GetTopicQuestions("Cars");
        //foreach (Question question in lWrongTopicQuestions)
        //{
        //    Console.WriteLine(question);
        //}
    }
}
