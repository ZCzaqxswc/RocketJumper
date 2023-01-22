using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_QuitPartcle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject onemat;
    public GameObject twomat;
    Renderer renderer;
    Renderer renderer1;
    private Transform Target;
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            var player = GameObject.FindWithTag("Player");
            Target = player.transform;
        }
        renderer = onemat.GetComponent<Renderer>();
        renderer1 = twomat.GetComponent<Renderer>();
        StartCoroutine("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position;
    }

    IEnumerator FadeOut()
    {
        int i = 0;
        while (i > 99)
        {
            i += 1;
            float f = i / 100.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            renderer1.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }

}
