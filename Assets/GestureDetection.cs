using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDetection : MonoBehaviour {

    public TextMesh debugTest, debugTest2;
    public Camera eye;
    List<VectorAndTime> positions;
    public float peak = 25f;
    public float closenessToDetect = .25f;
    public float peakMaxTime = .5f;
    Vector2 peakDirection;
    public VRInput myInput;
    // Use this for initialization
	void Start ()
    {
        positions = new List<VectorAndTime>();
        peakDirection = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        VectorAndTime vt;
        vt.pos = eye.transform.localEulerAngles.XY() - new Vector2(180,180);
        vt.dTime = Time.time;
        positions.Add(vt);

        if (positions.Count > 60)
            positions.RemoveAt(0);

        if(DetectPeaks())
        {
            positions.Clear();
            
        }
       
        //debugTest.text = Input.mousePosition + "";
        debugTest2.text = vt.pos + "";

        if (Input.GetButton("Tap"))
        {
          //  debugTest.text = "tapped";
        }
    }

    bool DetectPeaks()
    {
        List<Vector2> previousPositions = new List<Vector2>();
        for(int i = 0; i < positions.Count; i++)
        {
            bool peaked = false;
            for (int j  = i + 1; j < positions.Count; j++)
            {
                if(!peaked)
                {
                    if (PeakX(i, j) > peak) peaked = true;
                }
                else
                {
                    if (PeakX(i, j) < closenessToDetect)
                    {
                        if(TimeBetween(i,j) < peakMaxTime) return true;
                    }
                        
                }
            }
        }
        return false;
       
    }

    
    float PeakX(int ind1, int ind2)
    {
        return Mathf.Abs(positions[ind1].pos.x - positions[ind2].pos.x);
    }

    float TimeBetween(int ind1, int ind2)
    {
        return Mathf.Abs(positions[ind1].dTime - positions[ind2].dTime);
    }

    // This script shows a simple example of how
    // swipe controls can be handled.
    
        [SerializeField]
        private float m_Torque = 10f;
        [SerializeField]
        public VRInput m_VRInput;
        [SerializeField]
        private Rigidbody m_Rigidbody;


        private void OnEnable()
        {
            m_VRInput.OnSwipe += HandleSwipe;
        }


        private void OnDisable()
        {
            m_VRInput.OnSwipe -= HandleSwipe;
        }


        //Handle the swipe events by applying AddTorque to the Ridigbody
        private void HandleSwipe(VRInput.SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case VRInput.SwipeDirection.NONE:
                    break;
                case VRInput.SwipeDirection.UP:
                debugTest.text = "up";
                    break;
                case VRInput.SwipeDirection.DOWN:
                debugTest.text = "down";
                    break;
                case VRInput.SwipeDirection.LEFT:
                debugTest.text = "left";
                    break;
                case VRInput.SwipeDirection.RIGHT:
                debugTest.text = "right";
                 break;
            }
        }
    


}

struct  VectorAndTime
{
    public Vector2 pos;
    public float dTime;
}

/*
 int ind2 = 0;
            foreach (Vector2 p in previousPositions)
            {
                if (ind > ind2 + 10)
                {
                    float distX = Mathf.Abs(p.x - vt.pos.x);
                    if (distX < closenessToDetect)
                    {
                        if(PeakX(ind2, ind) > peak)
                        {
                            if (TimeBetween(ind2, ind) < peakMaxTime)
                                return true;
                        }
                    }
                }
               
                ind2++;
            }
            previousPositions.Add(vt.pos);

            ind++;
        }
*/