using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Camera _Camera = null;
	[SerializeField] private CharacterController _CharacterController = null;

	private GameObject CameraParent = null;

	[SerializeField] [Range(0.0f, 1.0f)] private float CameraHeight = 0.75f;

	
	
#if UNITY_EDITOR
	private void OnValidate()
	{
		// NULL Checks
		if (_Camera == null || CameraParent == null)
		{
			foreach (Transform Child in transform)
			{
				if (Child.GetComponent<Camera>())
				{
					_Camera = Child.GetComponent<Camera>();
					CameraParent = Child.gameObject;
					break;
				}
			}

			// We still didn't find a camera!
			if (_Camera == null)
			{
				CameraParent = Instantiate(new GameObject(), transform, false);
				CameraParent.transform.localPosition += new Vector3(0.0f, CameraHeight, 0.0f);
				_Camera = CameraParent.AddComponent<Camera>();
			}
		}

		_CharacterController = gameObject.GetComponent<CharacterController>();

		if (_CharacterController == null)
		{
			_CharacterController = gameObject.AddComponent<CharacterController>();
		}


		// Position Checks
		if (CameraParent && CameraParent.transform.localPosition != new Vector3(0.0f, CameraHeight, 0.0f))
		{
			CameraParent.transform.localPosition = new Vector3(0.0f, CameraHeight, 0.0f);
		}

		CameraParent.name = "CameraController";
		name = "PlayerController";
	}
#endif

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{}

	// Update is called once per frame
	void Update()
	{
	}
}