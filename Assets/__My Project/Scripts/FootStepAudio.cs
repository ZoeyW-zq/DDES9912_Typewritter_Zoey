using UnityEngine;

public class FootStepAudio : MonoBehaviour
{
    public AudioSource m_AudioSource;

    private void Start()
    {
        if(m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();

    }
    public void PlayStepAudio()
    {
        m_AudioSource.Play();
    }
}
