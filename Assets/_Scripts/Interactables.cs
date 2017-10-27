using UnityEngine;

public class Interactables : MonoBehaviour {

    public float radius = 2f;
    public Transform interactionTransform;

    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;
    bool hasAdded = false;
    bool canUseChestInt;

    public virtual void Interact()
    {

    }

    public virtual void ChestInteract()
    {

    }

    

    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }

        if (canUseChestInt && !hasAdded)
        {
            ChestInteract();
            hasAdded = true;
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false; 
    }

    public void ChestFocused()
    {
        canUseChestInt = true;
        hasAdded = false;
    }

    public void ChestDefocused()
    {
        canUseChestInt = false;
        hasAdded = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
