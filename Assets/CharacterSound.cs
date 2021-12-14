using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public AudioSource AttackVoice1;
    public AudioSource AttackVoice2;
    public AudioSource AttackVoice3;
    public AudioSource DamageVoice1;
    public AudioSource SwingSound1;
    public AudioSource SwingSound2;
    public AudioSource SkillSound1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackVoice11()
    {
        AttackVoice1.Play();
    }

    public void AttackVoice22()
    {
        AttackVoice2.Play();
    }

    public void AttackVoice33()
    {
        AttackVoice3.Play();
    }

    public void StopAllVoice()
    {
        AttackVoice1.Stop();
        AttackVoice2.Stop();
        AttackVoice3.Stop();
        DamageVoice1.Stop();
    }

    public void DamageVoice11()
    {
        DamageVoice1.Play();
    }

    public void Swing1()
    {
        SwingSound1.Play();
    }

    public void Swing2()
    {
        SwingSound2.Play();
    }

    public void Skill1()
    {
        SkillSound1.Play();
    }
}
