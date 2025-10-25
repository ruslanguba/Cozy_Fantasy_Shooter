using System.Collections.Generic;
using UnityEngine;

public class GhostRandomiser : MonoBehaviour
{
    [SerializeField] private List<Mesh> bodies;
    [SerializeField] private List<GameObject> hats;
    [SerializeField] private List<Material> materials;
    [SerializeField] private List<Material> hatMaterials;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private MeshRenderer hat;

    private void Start()
    {
        SetRandomSettings();
    }

    private void SetRandomSettings()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        int rndMaterial = Random.Range(0, materials.Count);
        int rndHatMaterial = Random.Range(0, hatMaterials.Count);
        int rndBody = Random.Range(0, bodies.Count);
        int rndHead = Random.Range(0, hats.Count);
        hat.material = hatMaterials[rndHatMaterial];
        meshRenderer.sharedMesh = bodies[rndBody];
        meshRenderer.material = materials[rndMaterial];
        foreach (var hat in hats)
        {
            hat.SetActive(false);
        }
        hats[rndHead].SetActive(true);
    }
}
