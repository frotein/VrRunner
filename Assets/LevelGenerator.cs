using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject startingSection;   
    // the pool that stores all the sections
    public SectionsPool pool;
    // the total amount of secxtion on screen at once
    public int totalSections;
    // the current direction the track is going in, 0 = forward, 1 = left, 2 = right
    int direction = 0;
    Transform currentSection;
    int currentIndex;
    // Use this for initialization
	void Start ()
    {
        currentSection = startingSection.transform;
        currentIndex = 0;
        pool.FillPool();
        for(int i = 0; i < 3; i++)
            AddSection();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void AddSection()
    {
        Transform section = pool.GetRandomSection();
        section.gameObject.SetActive(true);
        Section newSection = section.GetComponent<Section>();
        Section currentS = currentSection.GetComponent<Section>();
       




        Vector3 translate = currentS.end.position - newSection.start.position;
        newSection.transform.position +=translate;

        currentSection = section;
        section.forward = -currentS.end.forward;
    }

    
}
