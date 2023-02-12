using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XELA : MonoBehaviour
{
    public GameObject levelloader;

    // Update is called once per frame
    /*   void Update()
       {
           if (Input.GetKeyUp(KeyCode.E) && playerClosed)
           {
               if(dialoguePanel.activeInHierarchy)
               {
                   zeroText();
               } else
               {
                   dialoguePanel.SetActive(true);
                   StartCoroutine(Typing());
               }
           }
           if (dialogueText.text == dialogue[index])
           {
               contButton.SetActive(true);
           }
       }

       public void zeroText()
       {
           dialogueText.text = "";
           index = 0;
           dialoguePanel.SetActive(false);
       }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelloader.GetComponent<LevelLoader>().LoadLevel();
        }
    }
}