using UnityEngine;

public class FloatingCubeSpawner : MonoBehaviour
{
    public GameObject cube;
    private Renderer objRenderer;
    public void DestroyCube()
    {
        Destroy(cube);
    }
    public void MakeInvisible()
    {
        objRenderer = cube.GetComponent<Renderer>();
        objRenderer.enabled = false;
    }
    public void MakeVisible()
    {
        objRenderer = cube.GetComponent<Renderer>();
        objRenderer.enabled = true;
    }
}
