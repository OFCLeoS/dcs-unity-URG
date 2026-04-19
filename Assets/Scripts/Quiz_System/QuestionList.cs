//using UnityEngine;

using System.Collections.Generic;

public class QuestionList
{
    private static List<Question> lQuestions = new List<Question>();

    public static void AddQuestion(Question question)
    {
        lQuestions.Add(question);
    }

    public static int GetSize()
    {
        return lQuestions.Count;
    }

    public static Question GetQuestionFromIndex(int index)
    {
        return lQuestions[index];
    }

    public static List<Question> GetTopicQuestions(string topic)
    {
        List<Question> result = new List<Question>();
        foreach (Question question in lQuestions)
        {
            if (question.GetTopic() == topic)
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
            if (question.GetTopic() == topic && question.GetSubtopic() == subtopic)
            {
                result.Add(question);
            }
        }
        return result;
    }

    public static void LoadQuestionsFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            System.Console.WriteLine("ERROR! No file at: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (line.Trim() == "") 
            {
                continue;
            }

            // question|topic|subtopic|paragraphNumber|choice1\choice2\...\choice6|correctIndex
            string[] fields = line.Split('|');

            if (fields.Length != 6)
            {
                System.Console.WriteLine("ERROR: Expected 6 fields! But found: " + fields.Length);
                continue;
            }

            string question = fields[0];
            string topic = fields[1];
            string subtopic = fields[2];
            int paragraphNumber = Int32.Parse(fields[3]);
            string choicesLine = fields[4];
            int correctIndex = Int32.Parse(fields[5]);

            // split choices: choice1\choice2\...\choice6
            string[] choices = choicesLine.Split('\\');

            if (choices.Length != 6)
            {
                System.Console.WriteLine("ERROR! Expected 6 choices! But found: " + choices.Length);
                continue;
            }

            // check if correct index is in the right range
            if (correctIndex < 0 || correctIndex > 5)
            {
                System.Console.WriteLine("ERROR! correctIndex is out of bounds!");
                continue;
            }

            // create a new Question
            Question newQuestion = new Question(question, topic, subtopic, paragraphNumber, choices, correctIndex);
            AddQuestion(newQuestion);
        }

        System.Console.WriteLine("Finished loading! Questions loaded: " + lQuestions.Count);
    }
}
