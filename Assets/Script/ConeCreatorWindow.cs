using UnityEngine;
using UnityEditor;

public class ConeCreatorWindow : EditorWindow
{
    float radius = 1f;
    float height = 2f;
    int segments = 20;
    Material coneMaterial;

    [MenuItem("Window/Cone Creator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ConeCreatorWindow));
    }

    void OnGUI()
    {
        GUILayout.Label("Create Cone", EditorStyles.boldLabel);

        radius = EditorGUILayout.FloatField("Radius", radius);
        height = EditorGUILayout.FloatField("Height", height);
        segments = EditorGUILayout.IntField("Segments", segments);
        coneMaterial = EditorGUILayout.ObjectField("Material", coneMaterial, typeof(Material), false) as Material;

        if (GUILayout.Button("Create Cone"))
        {
            GameObject cone = CreateCone(radius, height, segments, coneMaterial);
            Selection.activeGameObject = cone;
        }
    }

    GameObject CreateCone(float radius, float height, int segments, Material material)
    {
        GameObject cone = new GameObject("Cone");

        MeshFilter meshFilter = cone.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cone.AddComponent<MeshRenderer>();

        Mesh coneMesh = new Mesh();
        meshFilter.mesh = coneMesh;

        // ������ �������� �迭�� ����
        Vector3[] vertices = new Vector3[segments + 2];

        // ������ ����� ��
        vertices[0] = new Vector3(0f, height, 0f);

        // ������ �ٴڸ� ������ ���
        float angleStep = 2f * Mathf.PI / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            vertices[i + 1] = new Vector3(x, 0f, z);
        }

        // ������ �ٴڸ� �߽���
        vertices[segments + 1] = new Vector3(0f, 0f, 0f);

        coneMesh.vertices = vertices;

        // ������ �ﰢ�� �ε��� �迭
        int[] triangles = new int[segments * 6];

        // ���� ���� �ﰢ��
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;           // ����� �� �ε���
            triangles[i * 3 + 1] = i + 1;   // ���� �ٴڸ� ���� �ε���
            triangles[i * 3 + 2] = (i + 1) % segments + 1; // ���� �ٴڸ� ���� �ε���
        }

        // ���� �ٴڸ� �ﰢ��
        for (int i = 0; i < segments; i++)
        {
            triangles[segments * 3 + i * 3] = segments + 1; // �ٴڸ� �߽��� �ε���
            triangles[segments * 3 + i * 3 + 1] = (i + 1) % segments + 1; // ���� �ٴڸ� ���� �ε���
            triangles[segments * 3 + i * 3 + 2] = i + 1; // ���� �ٴڸ� ���� �ε���
        }

        coneMesh.triangles = triangles;

        meshRenderer.material = material;

        return cone;
    }
}
