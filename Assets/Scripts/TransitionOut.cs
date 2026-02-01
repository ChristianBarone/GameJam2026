using UnityEngine;
using System.Collections;

public class TransitionOut : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<Animator>().SetTrigger("Out");
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
