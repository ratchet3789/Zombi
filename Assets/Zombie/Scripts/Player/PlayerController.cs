using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
	private GameObject _CameraGO = null;
	private Camera _Camera = null;
	private CharacterController _Controller = null;

	private NetworkTransformUnreliable _Transform = null;
	private NetworkIdentity _NetID = null;
	
	[Header("Camera")]
	[SerializeField] private Vector3 CameraOffset = new Vector3(0.0f, 0.75f, 0.0f);

	[Header("Controller")]
	[SerializeField] private float MousePitchSenstivity = 1.0f;
	[SerializeField] private float MouseYawSenstivity = 1.0f;
	
	[SerializeField] private float MouseMaxPitch = 80.0f;
	[SerializeField] private float MouseMaxYaw = 360.0f;

	[SerializeField] private float MoveSpeed = 0.25f;
	[SerializeField] private float GravityMultiplier = 0.1f;
	
	[SerializeField] private Vector3 RotationInputs = Vector3.zero;

	private Inventory _Inventory = null;
	
#if UNITY_EDITOR
	[ContextMenu("Validate")]
	private void OnValidate()
	{
		name = "PlayerController";

		// Scan all children for Camera
		if (_Camera == null)
		{
			foreach (Transform Child in transform)
			{
				if (Child.GetComponent<Camera>() != null)
				{
					_CameraGO = Child.gameObject;
					_Camera = Child.GetComponent<Camera>();
					break;
				}
			}
		}

		// Still null? Add a new one!
		if (_Camera == null || _CameraGO == null)
		{
			_CameraGO = new GameObject("PlayerCamera");
			_CameraGO.transform.SetParent(transform, false);
			_CameraGO.transform.position = Vector3.zero;

			_Camera = _CameraGO.transform.AddComponent<Camera>();
			_Camera.transform.localPosition = CameraOffset;
		}

		_Controller = transform.GetComponent<CharacterController>();
		if (_Controller == false)
		{
			_Controller = transform.AddComponent<CharacterController>();
		}

		_Transform = transform.GetComponent<NetworkTransformUnreliable>();
		if (_Transform == null)
		{
			_Transform = transform.AddComponent<NetworkTransformUnreliable>();
		}

		_NetID = transform.GetComponent<NetworkIdentity>();
		if (_NetID == null)
		{
			_NetID = transform.AddComponent<NetworkIdentity>();
		}
	}
#endif // UNITY_EDITOR - OnValidate
	
	// Start is called before the first frame update
	void Start()
	{
		if (!isLocalPlayer)
		{StartServer();}
		else
		{StartClient();}
	}

	// Update is called once per frame
	void Update()
	{
		if(!isLocalPlayer)
		{UpdateServer();}
		else
		{UpdateClient();}
	}

	void FixedUpdate()
	{
		if(!isLocalPlayer)
		{FixedUpdateServer();}
		else
		{FixedUpdateClient();}
	}
	
	#region SERVER
	void StartServer()
	{
	}

	void UpdateServer()
	{
	}
	
	void FixedUpdateServer()
	{
	}
	#endregion
	
	#region CLIENT
	void StartClient()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void UpdateClient()
	{
		// CAMERA
		RotationInputs += new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0.0f);
		RotationInputs.y = RotationInputs.y > 360.0f ? 0.0f : RotationInputs.y < 0.0f ? 360.0f : RotationInputs.y;
		RotationInputs.x = Mathf.Clamp(RotationInputs.x, -45.0f, 45.0f);
		_Camera.transform.rotation = Quaternion.Euler(RotationInputs);
	}
	
	void FixedUpdateClient()
	{
		// PAWN
		// Get our camera facing directions
		Vector3 Forward = Input.GetAxisRaw("Vertical") * _Camera.transform.forward;
		Vector3 Right = Input.GetAxisRaw("Horizontal") * _Camera.transform.right;
		
		// merge them
		Vector3 MovementInputs = Forward + Right;
		// Strip out the pitch from it because we don't want to move up
		MovementInputs.y = 0.0f;
		// Normalize the axis to stop angled movement > 1.0f
		MovementInputs.Normalize();
		// Add our move speed
		MovementInputs *= MoveSpeed;
		
		MovementInputs += Physics.gravity * GravityMultiplier;
		_Controller.Move(MovementInputs * Time.deltaTime);
	}
#endregion
}