using UnityEngine;

public class CableGlowHandler : MonoBehaviour
{
    public Material cableMaterial;
    public float glowSpeed = 1.0f;
    private float glowProgress = 0.0f;
    private bool isGlowing = true;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) // Your trigger condition
        //{
        //    glowProgress = 0.0f;
        //    isGlowing = true;
        //}

        if (isGlowing)
        {
            glowProgress += Time.deltaTime * glowSpeed;
            cableMaterial.SetFloat("_GlowProgress", glowProgress);

            if (glowProgress >= 1.0f)
            {
                isGlowing = false;
            }
        }
    }
}
