using System;
using UnityEngine;

/// <summary>
/// Basis of the Player Entity
/// </summary>
[RequireComponent(typeof(PlayerMovement), typeof(PlayerRotation))]
public class Player : DamageableEntity
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerRotation playerRotation;

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        playerRotation = GetComponent<PlayerRotation>();
    }
    #endregion

    protected override void DestroyEntity()
    {
        playerMovement.enabled = false;
        playerRotation.enabled = false;

        Debug.Log("Player has died!");
    }


    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void KILL() => DestroyEntity();

#endif
    #endregion
}