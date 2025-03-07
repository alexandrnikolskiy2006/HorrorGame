using UnityEngine;

[ImageEffectAllowedInSceneView, ExecuteInEditMode]
public class ColorSplit : MonoBehaviour
{
    public Vector2 redOffset;
    public Vector2 greenOffset;
    public Vector2 blueOffset;

    private Camera cam;
    private Shader shader;
    private Material material;

    private void OnPreCull()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (shader == null) shader = Shader.Find("Assets/Shaders/PostEffects/ColorSplit.shader");
        if (shader != null && material == null) material = new Material(shader);
    }
    private void OnDisable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) Destroy(material);
        else DestroyImmediate(material);
#else
                Destroy(material);
#endif
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (cam == null || material == null) Graphics.Blit(source, destination);
        else
        {
            material.SetVector("_ROffset", redOffset);
            material.SetVector("_GOffset", greenOffset);
            material.SetVector("_BOffset", blueOffset);
            Graphics.Blit(source, destination, material);
        }
    }
}
