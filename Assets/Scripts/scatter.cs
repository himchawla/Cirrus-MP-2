using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scatter : MonoBehaviour
{
        System.Random rnd = new System.Random();

    Transform Return;

    GameObject Animals;
    GameObject Waypoints;

    animal[] animals;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Animals = transform.GetChild(0).gameObject;
        Waypoints = transform.GetChild(1).gameObject;
        animals = new animal[Animals.transform.childCount];
        //scatterAway();
    }

    private void Start()
    {
        Animals = transform.GetChild(0).gameObject;
        Waypoints = transform.GetChild(1).gameObject;
        animals = new animal[Animals.transform.childCount];
        // scatterAway();
        Return = transform;

        //returnBack();

        
    }

    public struct animal
    {
        public GameObject an;
        public GameObject way;
    }

   
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animals.Length);
        for(int i=0;i<animals.Length;i++)
        {
            animals[i].an.transform.rotation = Quaternion.LookRotation(animals[i].an.GetComponent<Rigidbody>().velocity);
           // animals[i].an.transform.rotation.eulerAngles -= new Vector390.0f;
            if ((Return.position - animals[i].an.transform.position).magnitude >20.0f)
            {
                //Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                

                
                    animals[i].an.GetComponent<WanderAI>().enabled = true;
                

                //Animals.transform.GetChild(i).right = Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity.normalized;
            }

            
        }
    }

    void scatterAway()
    {
        int num;

        for (int i = 0; i < Animals.transform.childCount; i++)
        {
            animals[i].an = Animals.transform.GetChild(i).gameObject;
            num = rnd.Next(0, Waypoints.transform.childCount);
            animals[i].way = Waypoints.transform.GetChild(i).gameObject;
            Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = (Waypoints.transform.GetChild(num).transform.position - Animals.transform.GetChild(i).position).normalized * 5.0f;
            //Animals.transform.GetChild(i).forward = Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity.normalized;
            animals[i].an.GetComponent<WanderAI>().enabled = false;

        }
    }

    void returnBack()
    {
        for (int i = 0; i < Animals.transform.childCount; i++)
        {
            animals[i].an = Animals.transform.GetChild(i).gameObject;
            //num = rnd.Next(0, Waypoints.transform.childCount);
            animals[i].way = Return.gameObject;
            Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = (animals[i].way.transform.position - Animals.transform.GetChild(i).position).normalized * 5.0f;
            Animals.transform.GetChild(i).forward = Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity.normalized;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            scatterAway();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            returnBack();
        }
    }

}
