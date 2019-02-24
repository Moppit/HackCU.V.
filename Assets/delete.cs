using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    public AudioSource m_MyAudioSource;
    public AudioSource m_MyAudioSource1;
    public AudioSource m_MyAudioSource2;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Renderer newBlockRenderer = other.gameObject.GetComponent<Renderer>();
        if (other.gameObject.name == "Cube")
        {
            //Destroy(other.gameObject);
            if (newBlockRenderer.material.color == Color.red)
            {
                m_MyAudioSource.Play();
            }
            else if(newBlockRenderer.material.color == Color.green)
            {
                m_MyAudioSource1.Play();
            }
            newBlockRenderer.material.color = Color.blue;
            other.gameObject.SetActive(false);
        }

        else if(other.gameObject.name == "Start")
        {
            other.gameObject.SetActive(false);
            GlobalVariables.buttonPressed = true;
            m_MyAudioSource2.Play();
        }
    }
}
