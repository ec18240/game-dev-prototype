using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerJump, collectPoints, doorsOpen, powerUp, growing, shrinking, fallingSpears, takingDamage, teleport, checkPoint, health, switchBtn;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerJump = Resources.Load<AudioClip>("Jump");
        collectPoints = Resources.Load<AudioClip>("CollectingPoints");
        doorsOpen = Resources.Load<AudioClip>("DoorsOpen");
        powerUp = Resources.Load<AudioClip>("PowerUp");
        growing = Resources.Load<AudioClip>("Growing");
        shrinking = Resources.Load<AudioClip>("Shrinking");
        fallingSpears = Resources.Load<AudioClip>("SpearsHittingGround");
        takingDamage = Resources.Load<AudioClip>("TakingDamage");
        teleport = Resources.Load<AudioClip>("Teleport");
        checkPoint = Resources.Load<AudioClip>("checkpoint");
        health = Resources.Load<AudioClip>("Health");
        switchBtn = Resources.Load<AudioClip>("SwitchBtn");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "Jump":
                audioSrc.PlayOneShot(playerJump);
                break;
            case "CollectingPoints":
                audioSrc.PlayOneShot(collectPoints);
                break;
            case "DoorsOpen":
                audioSrc.PlayOneShot(doorsOpen);
                break;
            case "PowerUp":
                audioSrc.PlayOneShot(powerUp);
                break;
            case "Growing":
                audioSrc.PlayOneShot(growing);
                break;
            case "Shrinking":
                audioSrc.PlayOneShot(shrinking);
                break;
            case "SpearsHittingGround":
                audioSrc.PlayOneShot(fallingSpears);
                break;
            case "TakingDamage":
                audioSrc.PlayOneShot(takingDamage);
                break;
            case "Teleport":
                audioSrc.PlayOneShot(teleport);
                break;
            case "checkpoint":
                audioSrc.PlayOneShot(checkPoint);
                break;
            case "Health":
                audioSrc.PlayOneShot(health);
                break;
            case "SwitchBtn":
                audioSrc.PlayOneShot(switchBtn);
                break;
        }
    }
}
