using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScene : MonoBehaviour
{
    Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public IEnumerator StoryProduction()
    {
        yield return new WaitForSeconds(3f);
        animator.enabled = true;
        SoundManager.instance.PlaySFX("TutorialChat1");
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Next");
        SoundManager.instance.PlaySFX("TutorialChat1");

        yield return new WaitForSeconds(3f);
    }
}
