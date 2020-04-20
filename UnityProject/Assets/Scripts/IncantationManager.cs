using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IncantationManager : MonoBehaviour
{
    [SerializeField] Text textDisplay;
    [SerializeField] CanvasGroup songCanvas;

    List<string> sequence;
    int counter_sequence;

    string[] sentenceArray = new string[] { 
        "lorem ipsum dolor sit",
        "consectetur adipiscing elit",
        "tempor incididunt ut labore",
        "magna aliqua ut enim",
        "quis nostrud exercitation",
        "nisi ut aliquip ex ea",
        "duis aute irure dolor",
        "voluptate velit esse",
        "fugiat nulla pariatur",
        "occaecat cupidatat non",
        "in culpa qui officia",
        "anim id est laborum",
        "sed ut perspiciatis unde",
        "iste natus error sit",
        "doloremque laudantium totam",
        "eaque ipsa quae ab illo",
        "et quasi architecto beatae",
        "sunt explicabo nemo enim",
        "quia voluptas sit",
        "aut fugit sed quia",
        "dolores eos qui ratione",
        "nesciunt neque porro",
        "qui dolorem ipsum quia",
        "consectetur adipisci velit",
        "numquam eius modi tempora",
        "labore et dolore magnam",
        "voluptatem ut enim ad"};

    string currentSequence = "";
    int maxWords = 1;
    bool isOn = true;
    string input;
    int counterLetter;
    int counterLetterTotal;
    MainGame mainGame;

    float counterAudio = 0f;

    public bool IsOn { get => isOn; set => isOn = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        NewSequence();
        sequence = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

        counterAudio -= Time.deltaTime * 2f;
        if (counterAudio <= 0f)
        {
            GetComponent<AudioSource>().DOFade(0.1f, 1f);
        }
        if (IsOn)
        {
            foreach (char c in Input.inputString)
            {
                if (c == currentSequence[counterLetter])
                {
                    counterAudio = 2f;
                    GetComponent<AudioSource>().DOFade(1f, 1f);
                    counterLetter++;
                    counterLetterTotal++;
                    mainGame.Incantation_counter++;
                }
                //else if (c == 8)
                //{
                //    counterLetter--;
                //}
                
                input = currentSequence.Substring(0, counterLetter);
                string coloredText = "<color=#D61616>" + input + "</color>";
                string tmp2 = currentSequence.Substring(counterLetter, currentSequence.Length - (counterLetter));
                textDisplay.text = coloredText + tmp2;

                if (input == currentSequence)
                {
                    NextSentence();
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

    public void NextSentence()
    {
        Reset();

        counter_sequence++;
        if (counter_sequence >= sequence.Count) {
            return;
        }
        currentSequence = sequence[counter_sequence];
        textDisplay.text = currentSequence;
    }

    public void NewSequence() {
        //choose 5 sentences randomly

        Reset();

        counterLetterTotal = 0;

        sequence = new List<string>();
        sequence.Clear();

        for (int i = 0; i < 5; ++i) {
            sequence.Add(sentenceArray[UnityEngine.Random.Range(0, sentenceArray.Length)]);
        }
        counter_sequence = 0;
        
        currentSequence = sequence[counter_sequence];
        textDisplay.text = currentSequence;
    }

    public float PercentageCompleted() {
        if (sequence != null && sequence.Count > 0) {
            int sum = 0;
            for (int i = 0; i < sequence.Count; ++i) {
                sum += sequence[i].Length;
            }

            return (float)counterLetterTotal / (float)sum * 100.0f;
        }
        return 0;
    }
}
