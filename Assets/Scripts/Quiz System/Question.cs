//using UnityEngine;

public class Question
{
    private string question = "";
    #region Tips
    private string topic = "";
    private string subtopic = "";
    private int paragraphNumber  = -1;
    #endregion
    private string[] choices = new string[0];
    private int correctIndex = -1;

    public Question(string question, string topic, string subtopic, int paragraphNumber, string[] choices, int correctIndex)
    {
        this.question = question;

        this.topic = topic;
        this.subtopic = subtopic;
        this.paragraphNumber = paragraphNumber;
        
        this.choices = choices;
        this.correctIndex = correctIndex;
    }

    public string GetQuestion()
    {
        return question;
    }

    public string GetTopic()
    {
        return topic;
    }

    public string GetSubtopic()
    {
        return subtopic;
    }

    public int GetParagraphNumber()
    {
        return paragraphNumber;
    }

    public string[] GetChoices()
    {
        return choices;
    }

    public int GetCorrectIndex()
    {
        return correctIndex;
    }

    public override string ToString()
    {
        return "Question: " + question + ", Topic: " + topic + ", Subtopic: " + subtopic + ", Paragraph: " + paragraphNumber + ", Choices: " + string.Join(", ", choices) + ", Correct Answer: " + correctIndex;
    }
}
