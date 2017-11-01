using UnityEngine;
using System.Collections;
public class Orbit : MonoBehaviour
{
	Animator anim;
    PlayerController playerController;
	private GameObject player;
	private Rigidbody rgbd;
	public bool lockedON = false;
    public bool finalLockedON = false;
    Transform interactableTarget;
	GameObject[] enemyLocations;
	GameObject closest;
	public float lockOnDistance = 5f;
    public float lockOnRadiusTest = 4f;
    public float lockOnRadius = 800f;
    float buttonCoolDown;

    ClickManager click;
    public GameObject cameraCombatTarget;
    float camCombatAngle = 41.397f;
    float distanceAdjust = 2.04f;

    bool canTarget;
    int isRollingHash = Animator.StringToHash("isRolling");
    int rollLeftHash = Animator.StringToHash("roll left");
    int rollRightHash = Animator.StringToHash("roll right");
    int rollBackHash = Animator.StringToHash("roll back");

    void Start()
	{
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        player = this.gameObject;
		rgbd = GetComponent<Rigidbody> ();
        click = new ClickManager();
        lockedON = false;
		// Make the rigid body not change rotation
		if (rgbd)
			rgbd.freezeRotation = true;

        playerController.canMove = true;
    }
		


	void Update(){

        buttonCoolDown -= Time.deltaTime;
        canTarget = IsPaused();

        if (canTarget)
            Controls();	
    }

    void Controls(){
        if (Input.GetButtonDown("Target")){
            ClosestEnemy();
            lockedON = !lockedON;

        }
        if (closest != null){
            if(Chest.instance != null)
                Chest.instance.canUseChestChest = false;
            anim.SetBool("strafe", true);
            LockOnToEnemy();
            
        }
        else{
            lockedON = false;
            finalLockedON = false;
            closest = null;
            SwitchBackCamera();
        }

        if(lockedON == false){
            SwitchBackCamera();
        }
    }

    void SwitchBackCamera(){
        if (Chest.instance != null)
            Chest.instance.canUseChestChest = true;
        anim.SetBool("strafe", false);
        CameraMachine.instance.target = CameraMachine.instance.defaultTarget;
        CameraMachine.instance.adjustDistance = 1f;
        CameraMachine.instance.cameraAngle = CameraMachine.instance.defaultCameraAngle;
        StartCoroutine(WaitForCamera());
        playerController.canMove = true;
    }

    bool IsPaused(){
        if (Time.timeScale == 0f){
            return false;
        }
        else{
            return true;
        }
    }

    //LockOn to closest Enemy
    void LockOnToEnemy(){
		if(lockedON){
            if (closest != null){
                finalLockedON = true;
                Vector3 diff = (closest.transform.position - transform.position);
                float curDistance = diff.sqrMagnitude;
                //check if current distance is far from target enemy 
                playerController.canMove = true;
                RollingSystem();
                Vector3 dir = transform.position - closest.transform.position;
                dir = Vector3.ClampMagnitude(dir, lockOnRadiusTest);
                transform.position = closest.transform.position + dir;

                Vector3 lookDirection = closest.transform.position;
                lookDirection.y = this.transform.position.y;
                transform.LookAt(lookDirection);
                StartCoroutine(MoveInCamera());
                playerController.canMove = true;
            }
            
		}
             
	}

    public void LockOnToInteractable(Interactables newTarget){
        interactableTarget = newTarget.interactionTransform;
        if (interactableTarget != null){
            transform.LookAt(new Vector3(interactableTarget.transform.position.x, transform.position.y, interactableTarget.transform.position.z));
        }
    }

    public void StopLockingOnToInteractable(){
        interactableTarget = null;
    }

    void RollingSystem(){
        bool doubleClick;
        float xVel = transform.InverseTransformDirection(rgbd.velocity).x;
        
        if (xVel < -1){
            if (Input.GetButtonDown("Fire2") && buttonCoolDown < 0){            
                playerController.canMove = false;
                anim.SetTrigger(rollRightHash);
                buttonCoolDown = .6f;
            }
        }
        //He's going left
        else if (xVel > 1){
            if (Input.GetButtonDown("Fire2") && buttonCoolDown < 0){
                playerController.canMove = false;
                anim.SetTrigger(rollLeftHash);
                buttonCoolDown = .6f;
            }
        }
        else{
            if (Input.GetButtonDown("Fire2") && buttonCoolDown < 0){
                playerController.canMove = false;
                anim.SetTrigger(rollBackHash);
                buttonCoolDown = .6f;
            }
        }
       
    }
		

	//Find ClosestEnemy
	private GameObject ClosestEnemy() {
		// Find all game objects with tag Enemy
		enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyLocations != null){
            lockOnDistance = 20f;
            Vector3 position = transform.position;
            // Iterate through them and find the closest one
            foreach (GameObject go in enemyLocations){
                Vector3 diff = (go.transform.position - position);
                
                float curDistance = diff.sqrMagnitude;
                
                if (curDistance < lockOnDistance){
                    closest = go;
                    lockOnDistance = curDistance;
                }
                else{
                    closest = null;
                }
            }
        }
		return closest;
	}

    IEnumerator WaitForCamera(){
        yield return new WaitForSeconds(.9f);
 
        CameraMachine.instance.isPlayerMovement = true;
    }

    IEnumerator MoveInCamera(){
        CameraMachine.instance.isPlayerMovement = false;
        CameraMachine.instance.target = cameraCombatTarget.transform;
        CameraMachine.instance.adjustDistance = distanceAdjust;
        CameraMachine.instance.cameraAngle = camCombatAngle;
        yield return new WaitForSeconds(.9f);
    }

}