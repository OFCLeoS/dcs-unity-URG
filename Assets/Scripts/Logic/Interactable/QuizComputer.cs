using UnityEngine;

public class QuizComputer : MonoBehaviour, IInteractable
{
    [SerializeField] QuizManager quizManager;
    
    public void OnInteract(Player player)
    {
        quizManager.StartQuiz();
    }
}
