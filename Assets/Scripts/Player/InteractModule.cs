using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractModule : MonoBehaviour
{
    Player attachedPlayer;

    [SerializeField] private LayerMask InteractLayer;
    [SerializeField] private float InteractCheckDistance = 0.2f;
    [SerializeField] private Vector2 InteractCheckOffset = Vector2.zero;

    private void Awake()
    {
        attachedPlayer = GetComponent<Player>();
    }

    public void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + InteractCheckOffset, attachedPlayer.Facing, InteractCheckDistance, InteractLayer);

        //Debug.Log("Try and Interact");

        if(hit)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null )
            {
                interactable.Interact();
            }
            else
            {
                Debug.LogWarning($"Hit object {hit.collider.name} is on the interactable layer but doesnt have an interactable script");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(attachedPlayer != null )
        {
            Gizmos.DrawRay((Vector2)transform.position + InteractCheckOffset, attachedPlayer.Facing * InteractCheckDistance);
        }
        
    }
}
