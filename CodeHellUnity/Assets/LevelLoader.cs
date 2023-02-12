using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public GameObject xel;
    // Start is called before the first frame update
  

    // Update is called once per frame
    /*oid Update()
    {
        if (xel.GetComponent<XELA>().playerClosed)
        {
           LoadLevel();
        }
    }*/

    public void LoadLevel()
    {
        StartCoroutine(LoadingAnim(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadingAnim(int levelInd)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelInd);
    }
}
