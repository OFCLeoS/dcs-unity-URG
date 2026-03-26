//using UnityEngine;

public class Question
{
    public string question;
    public string topic;
    public string subtopic;
    public int paragraphNumber;
    public string[] choices;
    public int correctIndex;

    public Question(string question, string topic, string subtopic, int paragraphNumber, string[] choices, int correctIndex)
    {
        this.question = question;
        this.topic = topic;
        this.subtopic = subtopic;
        this.paragraphNumber = paragraphNumber;
        this.choices = choices;
        this.correctIndex = correctIndex;
    }

    public override string ToString()
    {
        return "Question: " + question + ", Topic: " + topic + ", Subtopic: " + subtopic + ", Paragraph: " + paragraphNumber + ", Choices: " + string.Join("Q, Q", choices) + ", Correct Answer: " + correctIndex;
    }
}
