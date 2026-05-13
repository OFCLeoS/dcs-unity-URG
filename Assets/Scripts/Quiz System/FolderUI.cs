using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class FolderUI : MonoBehaviour
{
    public GameObject folderRowPanel;

    public Button folderButton;

    public GameObject textViewerPanel;
    public TMP_Text textViewerTitle;
    public TMP_Text textViewerContent;


    private int currentLevel = 1;
    private string currentTopic = "";
    private string currentSubtopic = "";

    private List<Button> activeButtons = new List<Button>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textViewerPanel.SetActive(false);
        ShowTopics();
    }

    void ClearButtons()
    {
        foreach (Button button in activeButtons)
        {
            Destroy(button.gameObject);
        }
        activeButtons.Clear();
    }

    void CreateButton(string label, UnityEngine.Events.UnityAction onClick)
    {
        Button newButton = Instantiate(folderButton, folderRowPanel.transform);
        newButton.GetComponentInChildren<TMP_Text>().text = label;
        newButton.onClick.AddListener(onClick);
        activeButtons.Add(newButton);
    }

    public void ShowTopics()
    {
        ClearButtons();
        currentLevel = 1;
        currentTopic = "";
        currentSubtopic = "";

        List<string> topics = QuestionList.GetTopics();

        foreach (string topic in topics)
        {
            string t = topic;
            CreateButton(t, () => OnTopicClicked(t));
        }
    }

    void OnTopicClicked(string topic)
    {
        ClearButtons();
        currentLevel = 2;
        currentTopic = topic;

        CreateButton("Go Back", () => ShowTopics());

        List<string> subtopics = QuestionList.GetSubtopics(topic);

        foreach (string subtopic in subtopics)
        {
            string s = subtopic;
            CreateButton(s, () => OnSubtopicClicked(s));
        }
    }

    void OnSubtopicClicked(string subtopic)
    {
        ClearButtons();
        currentLevel = 3;
        currentSubtopic = subtopic;

        CreateButton("Go Back", () => OnTopicClicked(currentTopic));
        
        CreateButton(subtopic + ".txt", () => OnFileClicked(currentTopic, subtopic));
    }

    void OnFileClicked(string topic, string subtopic)
    {
        string content = QuestionList.GetParagraphs(topic, subtopic);

        textViewerTitle.text = subtopic + ".txt";
        textViewerContent.text = content;

        textViewerPanel.SetActive(true);
    }

    public void CloseTextViewer()
    {
        textViewerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
