using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Scriptable Objects/InventoryObject")]
public class InventoryObject : ScriptableObject
{
	[SerializeField] private byte UID = 0;

	[SerializeField] private Texture2D InventoryIcon;
	[SerializeField] private Mesh Mesh;
		
	#if UNITY_EDITOR
	public void SetUID(byte NewUID) { UID = NewUID; }

	private void OnValidate()
	{
		if (InventoryIcon == null)
		{
			InventoryIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Zombie/Art/UI/Dev/Inventory_Debug.png");
		}

		if (Mesh == null)
		{
			Mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Zombie/Art/UI/Dev/Inventory_Debug_Mesh.fbx");
		}
		
	}
#endif
}