using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CubeTester : MonoBehaviour
{
	[SerializeField] private float Scale;
	Vector3[] Cubes = new Vector3[3 * 3 * 3];
	private Color[] CubeColor = new Color[3 * 3 * 3];

	private int id = 0;
	private int maxIds = 3;

	[ContextMenu("Trigger Cube Grid")]
	void TriggerCubeGrid()
	{
		id = 0;

		for (byte z = 0; z < maxIds; z++)
		{
			for (byte y = 0; y < maxIds; y++)
			{
				for (byte x = 0; x < maxIds; x++)
				{
					Cubes[id] = transform.position + new Vector3(x, y, z) * Scale;
					CubeColor[id] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),
						Random.Range(0.0f, 1.0f),
						255.0f);
					id++;
				}

				CubeColor[id] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),
					255.0f);
				id++;
			}

			CubeColor[id] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),
				255.0f);
			id++;
			print(id);
		}
	}

	void OnDrawGizmos()
	{
		for (int i = 0; i < id; i++)
		{
			Gizmos.DrawCube(Cubes[i], Vector3.one * Scale);
			Gizmos.color = CubeColor[i];
		}
	}
}