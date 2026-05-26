//using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionList
{
    private static List<Question> questionsList = new List<Question>();

    // TODO: Replace foreach loops with for due to this being run countless times at runtime?

    // [0] = topic, [1] = subtopic, [2] = paragraph number, [3] = paragraph text
    private static List<string[]> contentList = new List<string[]>();

    public static void AddQuestion(Question question)
    {
        questionsList.Add(question);
    }

    public static int GetSize()
    {
        return questionsList.Count;
    }

    public static Question GetQuestionFromIndex(int index)
    {
        return questionsList[index];
    }

    public static List<Question> GetTopicQuestions(string topic)
    {
        List<Question> result = new List<Question>();
        foreach (Question question in questionsList)
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
        foreach (Question question in questionsList)
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
            Debug.LogError("ERROR! No file at: " + filePath);
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
                Debug.LogError("ERROR: Expected 6 fields! But found: " + fields.Length);
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
                Debug.LogError("ERROR! Expected 6 choices! But found: " + choices.Length);
                continue;
            }

            // check if correct index is in the right range
            if (correctIndex < 0 || correctIndex > 5)
            {
                Debug.LogError("ERROR! correctIndex is out of bounds!");
                continue;
            }

            // create a new Question
            Question newQuestion = new Question(question, topic, subtopic, paragraphNumber, choices, correctIndex);
            AddQuestion(newQuestion);
        }

        //Debug.Log("Finished loading! Questions loaded: " + questionsList.Count);
    }

    public static void LoadContentFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("ERROR! No file at: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (line.Trim() == "") continue;

            // topic|subtopic|paragraphNumber|paragraphText
            string[] fields = line.Split('|');

            if (fields.Length != 4)
            {
                Debug.LogError("ERROR: Expected 4 fields! But found: " + fields.Length);
                continue;
            }

            contentList.Add(fields);
        }

        //Debug.Log("Finished loading! Paragraphs loaded: " + questionsList.Count);
    }

    // returns all unique topic
    public static List<string> GetTopics()
    {
        List<string> topics = new List<string>();
        foreach (string[] uniqueTopic in contentList)
        {
            string topic = uniqueTopic[0].Trim();
            if (!topics.Contains(topic))
                topics.Add(topic);
        }
        return topics;
    }

    // returns all unique subtopic for a given topic
    public static List<string> GetSubtopics(string topic)
    {
        List<string> subtopics = new List<string>();
        foreach (string[] uniqueSubopic in contentList)
        {
            if (uniqueSubopic[0].Trim() == topic)
            {
                string subtopic = uniqueSubopic[1].Trim();
                if (!subtopics.Contains(subtopic))
                    subtopics.Add(subtopic);
            }
        }
        return subtopics;
    }

    // returns all paragraphs for each topic and subtopic
    public static string GetParagraphs(string topic, string subtopic)
    {
        string result = "";
        foreach (string[] entry in contentList)
        {
            if (entry[0].Trim() == topic && entry[1].Trim() == subtopic)
            {
                result += entry[2].Trim() + "   " + entry[3].Trim() + "\n";
            }
        }
        return result;
    }
}
