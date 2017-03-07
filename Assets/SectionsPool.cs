using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsPool : MonoBehaviour {

    public List<GameObject> LevelSections;
    public int copies; // how many copies to make of each section

    public List<List<Transform>> pool;
    bool populated; // have we tenerated the level sections
    // Use this for initialization
    void Start ()
    {
      //  FillPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FillPool()
    {
        pool = new List<List<Transform>>();
        foreach(GameObject section in LevelSections)
        {
            List<Transform> copiesList = new List<Transform>();
            for (int i = 0; i < copies; i++)
            {
                GameObject sectionInstance = GameObject.Instantiate(section);
                sectionInstance.transform.parent = transform;
                sectionInstance.SetActive(false);

                copiesList.Add(sectionInstance.transform);
            }

            pool.Add(new List<Transform>(copiesList));
        }
    }

    public Transform GetRandomSection()
    {
        int index = Random.Range(0, pool.Count);

        List<Transform> copies = pool[index];
        foreach(Transform t in copies)
        {
            if (!t.gameObject.activeSelf)
                return t;
        }

        return GetRandomSection();
    }
     
}
