using UnityEngine;

[CreateAssetMenu(menuName = "Auido/UIAudioPlayer")]
public class UIAudioPlayer : ScriptableObject
{
    [SerializeField] AudioClip ClickAudioClip;
    [SerializeField] AudioClip CommitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;
    [SerializeField] AudioClip WinAudioClip;
    [SerializeField] AudioSource _audioSource;
    public void PlayClick()
    {
        PlayAudio(ClickAudioClip);
    }

    public void PlayCommit()
    {
        PlayAudio(CommitAudioClip);
    }

    public void PlaySelect()
    {
        PlayAudio(SelectAudioClip);
    }

    internal void PlayWin()
    {
        PlayAudio(WinAudioClip);
    }

    void PlayAudio(AudioClip audioToPlay)
    {
        if (_audioSource != null) { _audioSource.PlayOneShot(audioToPlay); }
    }
}
