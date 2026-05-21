using UnityEngine;

public class QuizComputer : MonoBehaviour, IInteractable
{
    [SerializeField] QuizManager quizManager;
    Collider quizComputerCollider;

    public string InteractText => "Start Quiz";

    void Awake()
    {
        quizComputerCollider = GetComponent<Collider>();
        // Game does not start with a quiz
        DisableQuizComputer();
    }

    public void DisableQuizComputer()
    {
        quizComputerCollider.enabled = false;
    }

    public void EnableQuizComputer()
    {
        quizComputerCollider.enabled = true;
    }

    public void OnInteract(Player player)
    {
        quizManager.StartQuiz();
    }
}
