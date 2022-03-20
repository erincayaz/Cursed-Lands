using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bibleScript : MonoBehaviour
{
    // Event System
    //public delegate void BibleAnimation(bool animControl);
    //public static event BibleAnimation OnChange;

    // SerializeField
    [SerializeField] GameObject tempBible;
    [SerializeField] int maxBible;
    public skillScriptableObject bibleStats;

    float bibleArea;

    Transform player;
    List<GameObject> bibles = new List<GameObject>();
    List<GameObject> activeBibles = new List<GameObject>();

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.parent.transform;
        startTime = Time.time;
        bibleArea = bibleStats.radius;

        for (int i = 0; i < maxBible; i++)
        {
            GameObject tempProjectile = Instantiate(tempBible, player.transform.position, Quaternion.identity, player);
            bibles.Add(tempProjectile);

            Transform temp = tempProjectile.transform.GetChild(0);
            temp.position = new Vector3(-bibleStats.radius, 0f, 0f);

            tempProjectile.SetActive(false);
        }
    }

    private void Update()
    {
        // Animator Control //
        if(Time.time - startTime < bibleStats.duration && bibles[0].transform.GetChild(0).GetComponent<Animator>().GetBool("opacity"))
        {
            EventManager.BrodcastOnChange(false);
        }
        else if(Time.time - startTime > bibleStats.duration && !bibles[0].transform.GetChild(0).GetComponent<Animator>().GetBool("opacity"))
        {
            EventManager.BrodcastOnChange(true);
        }
        else if(Time.time - startTime > bibleStats.cooldown)
        {
            startTime = Time.time;
        }
        ///////////////////////

        // Adding Bibles
        if(bibleStats.amount > activeBibles.Count)
        {
            bibles[activeBibles.Count].SetActive(true);
            activeBibles.Add(bibles[activeBibles.Count]);

            startTime = Time.time - bibleStats.cooldown;

            float degree = 360f / activeBibles.Count;
            float curDegree = 0f;
            foreach(GameObject bible in activeBibles)
            {
                bible.transform.rotation = Quaternion.Euler(0f, 0f, curDegree);
                curDegree += degree;
            } 
        }
        ///////////////////
        
        // Rotation of bibles
        foreach (GameObject bible in activeBibles)
        {
            bible.transform.Rotate(new Vector3(0f, 0f, 1f) * bibleStats.speed * Time.deltaTime);
        }
        ////////////////////
        
        // Bible Area Change Check
        if(bibleArea < bibleStats.radius)
        {
            foreach (GameObject bible in bibles)
            {
                bible.transform.rotation = Quaternion.Euler(0, 0, 0);

                Transform childOfBible = bible.transform.GetChild(0);
                childOfBible.GetComponent<bible>().increaseRadius();

                bibleArea = bibleStats.radius;
            }

            activeBibles.Clear();
        }

        //////////////////////////
    }
}
