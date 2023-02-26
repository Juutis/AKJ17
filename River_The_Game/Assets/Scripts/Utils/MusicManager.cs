using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource menuMusic;
    private AudioSource gameMusic;


    [SerializeField]
    private AudioClip gameMusicClip;
    [SerializeField]
    private AudioClip menuMusicClip;

    private List<AudioFade> fades = new List<AudioFade>();


    [SerializeField]
    float volumeMenu = 0.5f;
    [SerializeField]
    float volumeGame = 0.5f;

    [SerializeField]
    float crossfadeDurationOut = 2.5f;
    [SerializeField]
    float crossfadeDurationIn = 2.5f;

    private bool isCurrentlyNormal = true;

    [SerializeField]
    private AudioSource audioSourcePrefab;

    [SerializeField]
    private bool isMainMenu = false;


    private void Awake()
    {
        InitializeAudioSources();
    }

    public void StartMusic(bool mainMenu)
    {
        isMainMenu = mainMenu;
        if (!isMainMenu)
        {
            gameMusic.volume = volumeGame;
            if (!gameMusic.isPlaying)
            {
                gameMusic.Play();
            }
        }
        else
        {
            menuMusic.volume = volumeMenu;
            if (!menuMusic.isPlaying)
            {
                menuMusic.Play();
            }
        }
    }

    public void SwitchMusic(bool mainMenu)
    {
        isMainMenu = mainMenu;
        if (isMainMenu)
        {
            Debug.Log($"Fade from {gameMusic} to {menuMusic}");
            CrossFade(gameMusic, menuMusic, crossfadeDurationOut, crossfadeDurationIn, volumeMenu, 1.0f);
        }
        else
        {
            Debug.Log($"Fade from {menuMusic} to {gameMusic}");
            CrossFade(menuMusic, gameMusic, crossfadeDurationOut, crossfadeDurationIn, volumeGame, 1.0f);
        }
    }


    private void InitializeAudioSources()
    {
        if (gameMusic == null)
        {
            gameMusic = InitializeAudioSource("Normal music", gameMusicClip);
        }
        if (menuMusic == null)
        {
            menuMusic = InitializeAudioSource("Menu music", menuMusicClip);
        }
    }

    private AudioSource InitializeAudioSource(string name, AudioClip clip)
    {
        AudioSource source = Instantiate(audioSourcePrefab);
        source.clip = clip;
        source.volume = 0;
        source.transform.SetParent(transform);
        source.transform.position = Vector2.zero;
        source.playOnAwake = false;
        source.loop = true;
        source.name = name;
        return source;
    }

    public void Fade(AudioSource fadeSource, float targetVolume, float duration = 0.5f, float targetPitch = 1.0f)
    {
        AudioFade fade = new AudioFade(duration, targetVolume, fadeSource, targetPitch);
        fades.Add(fade);
    }

    public void FadeOutMenuMusic(float duration = 0.5f)
    {
        Fade(menuMusic, 0, duration);
    }

    public void CrossFade(AudioSource fadeOutSource, AudioSource fadeInSource, float durationOut, float durationIn, float volume, float targetPitch)
    {
        AudioFade fadeOut = new AudioFade(durationOut, 0f, fadeOutSource, targetPitch);
        AudioFade fadeIn = new AudioFade(durationIn, volume, fadeInSource, targetPitch);
        fades.Add(fadeOut);
        fades.Add(fadeIn);
    }

    public void Update()
    {
        for (int index = 0; index < fades.Count; index += 1)
        {
            AudioFade fade = fades[index];
            if (fade != null && fade.IsFading)
            {
                fade.Update();
            }
            if (!fade.IsFading)
            {
                fades.Remove(fade);
            }
        }
    }

}

public class AudioFade
{
    public AudioFade(float duration, float target, AudioSource track, float targetPitch)
    {
        if (!track.isPlaying)
        {
            track.Play();
        }
        this.duration = duration;
        IsFading = true;
        timer = 0f;
        originalVolume = track.volume;
        targetVolume = target;
        audioSource = track;

        originalPitch = track.pitch;
        this.targetPitch = targetPitch;
    }
    public bool IsFading { get; private set; }
    private float duration;
    private float timer;
    private float targetVolume;
    private AudioSource audioSource;
    private float originalVolume;

    private float originalPitch, targetPitch;

    public void Update()
    {
        timer += Time.unscaledDeltaTime / duration;
        audioSource.volume = Mathf.Lerp(originalVolume, targetVolume, timer);
        audioSource.pitch = Mathf.Lerp(originalPitch, targetPitch, timer);
        if (timer >= 1)
        {
            audioSource.volume = targetVolume;
            audioSource.pitch = targetPitch;
            IsFading = false;
            if (audioSource.volume <= 0.01f)
            {
                audioSource.Pause();
            }
        }
    }
}