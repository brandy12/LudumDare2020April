using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField] Transform notes;

    [SerializeField] Note note1_prefab;
    [SerializeField] Note note2_prefab;
    [SerializeField] Note note3_prefab;

    float timer;
    float duration = 0.3f;

    bool is_generating;

    int counter;

    // Start is called before the first frame update
    void Start()
    {
        is_generating = false;
    }

    // Update is called once per frame
    void Update()
    {
        NotesGeneration();
    }

    public void GenerateNotes() {
        timer = 0;
        counter = 1;

        is_generating = true;

        GenerateNote(counter);
    }

    void NotesGeneration() {
        if (is_generating) {
            timer += Time.deltaTime;

            if (timer >= duration) {
                timer = 0;

                counter++;

                GenerateNote(counter);

                if (counter == 3) {
                    is_generating = false;
                }
            }
        }
    }

    void GenerateNote(int i) {
        Note n = null;

        if (i == 1) 
            n = Note.Instantiate(note1_prefab);
        else if (i == 2)
            n = Note.Instantiate(note2_prefab);
        else if (i == 3)
            n = Note.Instantiate(note3_prefab);

        n.transform.SetParent(notes, false);
        n.transform.position = transform.position;
    }
}
