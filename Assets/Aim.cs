using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the Player character and allows them to use a riflescope
/// The script moves cameraHead and changes movement speed.
/// 
/// This script accesses the player's CharacterMotor script to change
/// the player's movement speeds and their jump height.
/// 
/// This script accesses the PlayerStats script to determine what the player's max 
/// movement speed is.
/// </summary>

public class Aim : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//Have a quick reference to the CharacterMotor script.
	
	private CharacterMotor motorScript;
	
	
	//aim and normal movement values.
	
	private float aimSpeed = 2;
	
	private float normalSpeed = 5;

	private float aimBaseHeight = 0.5f;
	
	private float normalBaseHeight = 1;
	
	private float aimExtraHeight = 0.5f;
	
	private float normalExtraHeight = 1;
	
	
	//Used in determining whether the player will be aiming or standing
	public bool crouchEngaged = false;

	//Camera Variables
	private Camera myCamera;
	
	private Transform cameraHeadTransform;

	public float ZoomDistance = 25;
	
	//Variables End_____________________________________
	
	// initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			//Obtain the player's max speed on spawning.
			
			GameObject gameManager = GameObject.Find("GameManager");
		
			PlayerStats statScript = gameManager.GetComponent<PlayerStats>();
			
			normalSpeed = statScript.maxSpeed;

			// get player's camera 
			cameraHeadTransform = transform.FindChild("CameraHead");
			
			
			//Set the player's normal speed on spawning.
			
			motorScript = gameObject.GetComponent<CharacterMotor>();
			
			motorScript.movement.maxForwardSpeed = normalSpeed;
			
			motorScript.movement.maxBackwardsSpeed = normalSpeed;
			
			motorScript.movement.maxSidewaysSpeed = normalSpeed;
			
			//Set the player's normal jumping height.
			
			motorScript.jumping.baseHeight = normalBaseHeight;
			
			motorScript.jumping.extraHeight = normalExtraHeight;
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Engage aiming when the player presses w, and is not already aiming, and the cursor is not unlocked.
		
		if(Input.GetButtonDown("Aim") && crouchEngaged == false && Screen.lockCursor == true)
		{
			crouchEngaged = true;
			
			
			//Moves CameraHead
			
			//calculate position of CameraHead after zooming engaged
					
			Vector3 cameraPos = new Vector3(cameraHeadTransform.localPosition.x, cameraHeadTransform.localPosition.y,cameraHeadTransform.localPosition.z+ZoomDistance);
			
			cameraHeadTransform.localPosition = cameraPos;

			
			//Access the CharacterMotor scripts and change the player's speed and jump height values.
			
			motorScript.movement.maxForwardSpeed = aimSpeed;
			
			motorScript.movement.maxBackwardsSpeed = aimSpeed;
			
			motorScript.movement.maxSidewaysSpeed = aimSpeed;
			
			motorScript.jumping.baseHeight = aimBaseHeight;
			
			motorScript.jumping.extraHeight = aimExtraHeight;
			
		}
		
		
		//Disengage aiming when the player presses w, and is currently crouching and the cursor is not unlocked.
		
		if(Input.GetButtonUp("Aim") && crouchEngaged == true && Screen.lockCursor == true)
		{
			crouchEngaged = false;
			
			//calculate position of CameraHead after zooming disengaged
			Vector3 cameraPos = new Vector3(cameraHeadTransform.localPosition.x, cameraHeadTransform.localPosition.y,cameraHeadTransform.localPosition.z-ZoomDistance);

			cameraHeadTransform.localPosition = cameraPos;
			
			//Access the CharacterMotor scripts and change the player's speed and jump height values.
			
			motorScript.movement.maxForwardSpeed = normalSpeed;
			
			motorScript.movement.maxBackwardsSpeed = normalSpeed;
			
			motorScript.movement.maxSidewaysSpeed = normalSpeed;
			
			motorScript.jumping.baseHeight = normalBaseHeight;
			
			motorScript.jumping.extraHeight = normalExtraHeight;
			
		}
	}

	
}

