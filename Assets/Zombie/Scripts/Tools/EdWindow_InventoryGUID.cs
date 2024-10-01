using System.Security.Cryptography;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EdWindow_InventoryGUID : EditorWindow
{
	[MenuItem("Zombie/Inventory Object Validator")]
	public static void ShowWindow()
	{
		EdWindow_InventoryGUID Wnd = GetWindow<EdWindow_InventoryGUID>();
		Wnd.titleContent = new GUIContent("EdWindow_InventoryGUID");
	}

	private InventoryObject[] InvObjects;
	
	public void OnGUI()
	{
		if (GUILayout.Button("Find All Inventory Objects"))
		{
			// Ittr through all objects and find our specific ones
			string[] GUIDs = AssetDatabase.FindAssets("t:" + typeof(InventoryObject).Name);
			InvObjects = new InventoryObject[GUIDs.Length];
			for(int i=0; i < GUIDs.Length; i++)
			{
				InvObjects[i] = AssetDatabase.LoadAssetAtPath<InventoryObject>(AssetDatabase.GUIDToAssetPath(GUIDs[i]));
				InvObjects[i].SetUID((byte)Mathf.Clamp(i, byte.MinValue, byte.MaxValue));
				Debug.Log("Set " + InvObjects[i].name + " UID to " + i);
			}
		}
/*		ReorderableList ReorderList = new ReorderableList(InvObjects, typeof(InventoryObject));
		ReorderList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
		{
			InventoryObject InvOBJ = InvObjects[index];
			EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width * 0.2f, rect.height), "Name:");
			InvOBJ.name = EditorGUI.TextField(new Rect(rect.x + rect.width * 0.2f, rect.y, rect.width * 0.8f, rect.height), InvOBJ.name);
			EditorGUI.LabelField(new Rect(rect.x, rect.y + rect.height, rect.width * 0.2f, rect.height), "Value:");
			InvOBJ.UID = (byte)EditorGUI.IntField(new Rect(rect.x + rect.width * 0.2f, rect.y + rect.height, rect.width * 0.8f, rect.height), Mathf.Clamp(InvOBJ.UID, byte.MinValue, byte.MaxValue) );
		};*/
	}
}