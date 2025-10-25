using System.Collections.Generic;
using UnityEngine;

public class PamkinRandomaizer : MonoBehaviour
{
    [SerializeField] private List<Mesh> meshes;
    private MeshFilter meshRenderer;
    private MeshCollider meshCollider;

    private void Start()
    {
        SetRandomSettings();
    }

    void SetRandomSettings()
    {
        if ( meshes.Count > 0)
        {
            meshRenderer = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
            int rndMesh = Random.Range(0, meshes.Count);
            meshRenderer.mesh = meshes[rndMesh];
            meshCollider.sharedMesh = meshes[rndMesh];
        }
    }
}
