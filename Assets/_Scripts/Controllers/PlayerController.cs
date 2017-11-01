using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Animator animator;
    private Orbit orbitClass;

    public Interactables focus;
    bool isInteracting;
    //Control
    public float movementSpeed;
	public float lookSpeed = 10;
	private Vector3 curLoc;
	private Vector3 prevLoc;
    float buttonCoolDown;
    ClickManager clickMang;

    public float interactableRadius = 2f;
	Vector3 movement;


	[HideInInspector]
	public bool canMove = true;

    public float moveHorizontal, moveVertical;

	private void Start(){
		animator = GetComponent<Animator> ();
        clickMang = new ClickManager();
        orbitClass = GetComponent<Orbit>();
        canMove = true;
	}


    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        if (canMove) {
            ControllPlayer();
        }
    }

    private void Update()
    {
        //Are we currently hovering over UI. If yes then exit out before controlling player.
        buttonCoolDown -= Time.deltaTime;
        InteractableFunc();
    }

    void ControllPlayer()
	{

        if (!orbitClass.lockedON)
        {
            if (Input.GetButtonDown("Fire2") && buttonCoolDown < 0)
            {
                animator.SetTrigger("roll forward");
                buttonCoolDown = .6f;

            }
        }
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		transform.Translate (movement * movementSpeed * Time.deltaTime, Space.World);

		prevLoc = curLoc;
		curLoc = transform.position;
        
		if (moveHorizontal == 0) {
			animator.SetFloat ("forward", moveHorizontal);
		}
		if (moveVertical == 0) {
			animator.SetFloat ("forward", moveVertical);
		}

		if (moveHorizontal < 0) {
			curLoc.x -= 1 * Time.fixedDeltaTime;
			animator.SetFloat ("forward", moveHorizontal*-1 );

		}
		if (moveHorizontal > 0) {
			curLoc.x += 1 * Time.fixedDeltaTime;
			animator.SetFloat ("forward", moveHorizontal );
		}
		if (moveVertical > 0) {
			curLoc.z += 1 * Time.fixedDeltaTime;
			animator.SetFloat ("forward", moveVertical );
		}
		if (moveVertical < 0) {
			curLoc.z -= 1 * Time.fixedDeltaTime;
			animator.SetFloat ("forward", moveVertical*-1 );
		}

		transform.position = curLoc;
		if (movement.magnitude > .0001) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);
		}


	}

    public void AttackAnimation(int attackNumber)
    {
        animator.SetTrigger("attack");
        Debug.Log(attackNumber);
        animator.SetInteger("attackNumber", attackNumber);
    }

    void InteractableFunc()
    {
        if (Input.GetButtonDown("Use"))
        {
            isInteracting = !isInteracting;

            RaycastHit hit;

            //Ray cast scan forward
            //Change back to raycast and just check 4 directions
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, interactableRadius))
            {
                //if hit interactable
                Interactables interactableHit;
                interactableHit =  hit.collider.GetComponent<Interactables>();
                
               
                if (interactableHit != null && isInteracting == true)
                {
                    SetFocus(interactableHit);
                }
                if (interactableHit != null && isInteracting == false)
                {
                    RemoveFocus();
                }
                
            }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, interactableRadius))
            {
                //if hit interactable
                Interactables interactableHit;
                interactableHit = hit.collider.GetComponent<Interactables>();


                if (interactableHit != null && isInteracting == true)
                {
                    SetFocus(interactableHit);
                }
                if (interactableHit != null && isInteracting == false)
                {
                    RemoveFocus();
                }

            }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, interactableRadius))
            {
                //if hit interactable
                Interactables interactableHit;
                interactableHit = hit.collider.GetComponent<Interactables>();


                if (interactableHit != null && isInteracting == true)
                {
                    SetFocus(interactableHit);
                }
                if (interactableHit != null && isInteracting == false)
                {
                    RemoveFocus();
                }

            }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, interactableRadius))
            {
                //if hit interactable
                Interactables interactableHit;
                interactableHit = hit.collider.GetComponent<Interactables>();


                if (interactableHit != null && isInteracting == true)
                {
                    SetFocus(interactableHit);
                }
                if (interactableHit != null && isInteracting == false)
                {
                    RemoveFocus();
                }

            }
        }
    }

    void SetFocus(Interactables focusHit)
    {
        if(focusHit != focus)
        {
            if(focus != null)
                focus.OnDefocused();
            focus = focusHit;
            orbitClass.LockOnToInteractable(focusHit);
        }

        focus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        orbitClass.StopLockingOnToInteractable();
    }

    void FootR()
    {

    }

    void FootL()
    {

    }
}
