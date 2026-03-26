//using UnityEngine;

using System.Collections.Generic;

public class QuestionList
{
    public static List<Question> lQuestions = new List<Question>();

    public static void AddQuestion(Question question)
    {
        lQuestions.Add(question);
    }

    public static List<Question> GetTopicQuestions(string topic)
    {
        List<Question> result = new List<Question>();
        foreach (Question question in lQuestions)
        {
            if (question.topic == topic)
            {
                result.Add(question);
            }
        }
        return result;
    }

    public static List<Question> GetSubtopicQuestions(string topic, string subtopic)
    {
        List<Question> result = new List<Question>();
        foreach (Question question in lQuestions)
        {
            if (question.topic == topic && question.subtopic == subtopic)
            {
                result.Add(question);
            }
        }
        return result;
    }
}
