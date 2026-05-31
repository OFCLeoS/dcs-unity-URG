using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SphereCollider))]
public class PlayerInteractionsHandler : MonoBehaviour
{
    InputAction interactAction;

    [SerializeField] Player player;

    SphereCollider interactibleChecker;
    LayerMask interactableLayerMask;

    float maximumInteractionRange;

    bool nearInteractible = false;

    [Tooltip("A reference to the player's head to properly check if the player is looking at an interactible")]
    [SerializeField] Transform playerHead;

    // Will be used for making the interaction description track the player cam.
    [SerializeField] Transform playerCamera;

    [SerializeField] Transform interactionTextObject;
    [SerializeField] TMP_Text interactionText;
    bool interactionDescriptionTextActive;

    #region Initialization
    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        interactibleChecker = GetComponent<SphereCollider>();
        interactibleChecker.isTrigger = true; //We don't want objects to collide with the interaction checking sphere
        interactibleChecker.includeLayers = LayerMask.GetMask("Interactable"); //We only check the "Interactable" layer
        interactibleChecker.excludeLayers = ~LayerMask.GetMask("Interactable"); //We exclude all the layers except the "Interactable" layer

        maximumInteractionRange = interactibleChecker.radius; //The maximum interaction range will be the radius of the interaction sphere

        interactableLayerMask = LayerMask.GetMask("Interactable");

        interactionTextObject.gameObject.SetActive(false);
        interactionDescriptionTextActive = false;
    }
    #endregion


    void EnableText(Vector3 position, string text)
    {
        interactionTextObject.position = position;
        interactionTextObject.gameObject.SetActive(true);
        interactionText.text = text;
        interactionDescriptionTextActive = true;
    }

    void DisableText()
    {
        interactionDescriptionTextActive = false;
        interactionTextObject.gameObject.SetActive(false);
    }

    void MakeTextTrackCamera()
    {
        //interactionTextObject.transform.LookAt(playerCamera, interactionTextObject.up);

    }

    Collider lastHit;
    IInteractable lastHitInteractable;
    /// <summary>
    /// Casts a ray that checks whether or not the player is looking at an interactible, and allows interaction with it if true
    /// </summary>
    void HandleRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerHead.position, playerHead.forward, out hit, maximumInteractionRange, interactableLayerMask, QueryTriggerInteraction.Collide))
        {
            if (lastHit != hit.collider)
            {
                lastHit = hit.collider;
                lastHitInteractable = hit.collider.transform.GetComponent<IInteractable>();
            }

            if (!interactionDescriptionTextActive) EnableText(hit.transform.position, lastHitInteractable.InteractText);
            else MakeTextTrackCamera();

            if (interactAction.WasPressedThisFrame()) lastHitInteractable.OnInteract(player);

            Debug.DrawRay(playerHead.position, playerHead.forward * maximumInteractionRange, Color.green);
        }
        else
        {
            if (interactionDescriptionTextActive) DisableText();
            Debug.DrawRay(playerHead.position, playerHead.forward * maximumInteractionRange, Color.red);
        }
    }

    void Update()
    {
        if (nearInteractible) HandleRayCast(); //If the player is within the range of an interactible, a ray cast will be performed
    }

    void OnTriggerStay(Collider other) => nearInteractible = true;

    void OnTriggerExit(Collider other) => nearInteractible = false;
}
