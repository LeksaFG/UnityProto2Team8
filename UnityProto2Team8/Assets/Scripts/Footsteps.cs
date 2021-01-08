using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstep_Sound;
    [SerializeField]
    private AudioClip[] footstep_Clip;
    public CharacterController controller;

    private void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();
    }
    private void Step()
    {
        AudioClip footstep_Clip = GetRandomClip();
        footstep_Sound.PlayOneShot(footstep_Clip);      
        //Debug.Log("Giant leap for Mat kind");
    }
    private AudioClip GetRandomClip()
    {
        return footstep_Clip[UnityEngine.Random.Range(0,footstep_Clip.Length)];
    }
}
