using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;
    public float meshRefreshRate = 0.1f;
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate= 0.05f;
    private bool isTrailActive = false;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTrailActive)
        {
            Debug.Log("Trail activated");
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float activeTime)
    {
        while (activeTime > 0)
        {
            activeTime -= meshRefreshRate;

            if(meshRenderers == null)
            {
                meshRenderers = GetComponents<SkinnedMeshRenderer>();
                Debug.Log("MeshRenderers assigned");
            }
            else
            {
                Debug.Log("MeshRenderers already assigned");
            }

            for(int i = 0; i < meshRenderers.Length; i++)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                MeshFilter mf = gameObject.AddComponent<MeshFilter>();
                MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

                Mesh mesh = new Mesh();
                meshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;
                Debug.Log("Mesh created");  

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gameObject, 3);
            }
            

            yield return new WaitForSeconds(meshRefreshRate);
        }   

        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}

