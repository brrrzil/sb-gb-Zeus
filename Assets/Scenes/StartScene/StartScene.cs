using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject[] Signs;
    [SerializeField] private float lag;

    void Start()
    {
        StartCoroutine(SignsQueue());
    }

    IEnumerator SignsQueue()
    {
        yield return new WaitForSeconds(2.5f);            
        for (int i = 0; i < Signs.Length; i++)
        {
            Signs[i].SetActive(true);
            yield return new WaitForSeconds(lag);            
            Signs[i].SetActive(false);
        }
    }

    public void SkipButton()
    {
        SceneManager.LoadScene(1);
    }

    public void HelpButton()
    {
        SceneManager.LoadScene(2);
    }
}