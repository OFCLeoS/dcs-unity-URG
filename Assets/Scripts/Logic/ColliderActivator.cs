using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderActivator : MonoBehaviour
{
    [SerializeField] BoxCollider activatorBox;
    [SerializeField] Collider colliderToActivate;

    void Awake()
    {
        Rigidbody activatorRB = GetComponent<Rigidbody>();
        activatorRB.useGravity = false;
        activatorRB.isKinematic = true;

        activatorBox = GetComponent<BoxCollider>();
        activatorBox.isTrigger = true;

        colliderToActivate.enabled = false;
    }

    public void EnableActivator()
    {
        activatorBox.enabled = true;
    }

    public void DisableActivator()
    {
        activatorBox.enabled = false;
        colliderToActivate.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            colliderToActivate.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            colliderToActivate.enabled = false;
        }
    }
}