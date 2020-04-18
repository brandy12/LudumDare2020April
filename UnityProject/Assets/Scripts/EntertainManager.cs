using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntertainManager : MonoBehaviour
{
    [SerializeField] Text textDisplay;
    [SerializeField] CanvasGroup songCanvas;

    string[] wordArray = new string[] { "lorem", "ipsum", "dolor", "sit" };
    string currentSequence = "";
    int maxWords = 2;
    bool isOn = false;
    string input;
    int counterLetter;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
        songCanvas.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("entertain");
            Reset();
            InitializeSequence();
            isOn = true;
            songCanvas.alpha = 1;
        }

        if (isOn)
        {
            foreach (char c in Input.inputString)
            {
                if (c == currentSequence[counterLetter])
                {
                    counterLetter++;
                    
                }
                else if (c == 8)
                {
                    counterLetter--;
                }

                input = currentSequence.Substring(0, counterLetter);
                string coloredText = "<color=red>" + input + "</color>";
                string tmp2 = currentSequence.Substring(counterLetter, currentSequence.Length - (counterLetter));
                textDisplay.text = coloredText + tmp2;

                if (input == currentSequence)
                {
                    Reset(); 
                    songCanvas.alpha = 0;
                    Debug.Log("done");
                }
            }
        }
    }

    void Reset()
    {
        currentSequence = "";
        input = "";
        counterLetter = 0;
        textDisplay.text = currentSequence;
    }

    void InitializeSequence()
    {
        for (int i = 0; i < maxWords; i++)
        {
            if (i == 0)
            {
                currentSequence += wordArray[Random.Range(0, wordArray.Length)];
            }
            else
            {
                currentSequence += " " + wordArray[Random.Range(0, wordArray.Length)];
            }
        }

        textDisplay.text = currentSequence;
    }
}
