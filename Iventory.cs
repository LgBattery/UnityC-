using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iventory : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    [SerializeField] private GameObject slot3;
    private Vector3 s1bs; //Stands for Slot x before scale, we capture this number so when we tween the object in and out its scale is preserved.
    private Vector3 s2bs;
    private Vector3 s3bs;
    public KeyCode s1; //The keyboard inputs for selecting slots.
    public KeyCode s2;
    public KeyCode s3;
    public KeyCode ThrowItem;
    public AnimationCurve Curve; //This is used to adjust what the tween in and tween out looks like.
    private float switchTime;
    private float switchInterval = .5f; //How fast you can switch items.
    public Transform activeItem; //Which item you are holding.
    //public transform activeItem;

    // Start is called before the first frame update
    void Start()
    {
        if (Inventory.transform.childCount > 0)
        {
        slot1 = Inventory.transform.GetChild(0).gameObject;
        }
        else {slot1 = null; slot3 = null; slot2 = null;}
        if (Inventory.transform.childCount > 1)
        {
        slot2 = Inventory.transform.GetChild(1).gameObject;
        }
        else {slot3 = null; slot2 = null;}
        if (Inventory.transform.childCount > 2)
        {
        slot3 = Inventory.transform.GetChild(2).gameObject;
        }
        else {slot3 = null;}
        pickup();
        StartCoroutine(ds1()); //I called this so if you have items equipped before the script starts their rigid bodies get disabled.
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Inventory.transform.childCount; i++)
        {
            if(Inventory.transform.GetChild(i).gameObject.activeSelf == true)
            {
            activeItem = Inventory.transform.GetChild(i);
            }
        }
        //Item throwing start
        if (Input.GetKeyDown(ThrowItem))
        {
            if (activeItem.GetComponent<Animator>() != null){activeItem.GetComponent<Animator>().enabled = false;}
            activeItem.GetComponent<Collider>().enabled = true;
            activeItem.GetComponent<Rigidbody>().isKinematic = false;
            activeItem.parent = null;
        }
        //Item throwing end
        //Slot definition start 
        if (Inventory.transform.childCount > 0)
        {
        slot1 = Inventory.transform.GetChild(0).gameObject;
        }
        else {slot1 = null; slot3 = null; slot2 = null;}
        if (Inventory.transform.childCount > 1)
        {
        slot2 = Inventory.transform.GetChild(1).gameObject;
        }
        else {slot3 = null; slot2 = null;}
        if (Inventory.transform.childCount > 2)
        {
        slot3 = Inventory.transform.GetChild(2).gameObject;
        }
        else {slot3 = null;}
        //slot definition end
        if(Input.GetKeyDown(s1) && (slot2 == null || slot2.activeInHierarchy == false) && (slot3 == null || slot3.activeInHierarchy == false) && Time.time > switchTime && slot1 != null)
        {
            switchTime = Time.time + switchInterval;
            if(slot1.activeInHierarchy == true)
            {// item deselection
                LeanTween.scale(slot1, new Vector3 (0, 0, 0), 0.5f).setEase(Curve);
                StartCoroutine(ds1());
            }
            else
            {// item reselection
            slot1.SetActive(true);
            LeanTween.scale(slot1, new Vector3 (0, 0, 0), 0f);
            LeanTween.scale(slot1, s1bs, 0.5f).setEase(Curve);
            }
        }
        if(Input.GetKeyDown(s2) && (slot3 == null || slot3.activeInHierarchy == false) && (slot1 == null || slot1.activeInHierarchy == false) && Time.time > switchTime && slot2 != null)
        {
            switchTime = Time.time + switchInterval;
            if(slot2.activeInHierarchy == true)
            {// item deselection
                LeanTween.scale(slot2, new Vector3 (0, 0, 0), 0.5f).setEase(Curve);
                StartCoroutine(ds1());
            }
            else
            {// item reselection
            slot2.SetActive(true);
            LeanTween.scale(slot2, new Vector3 (0, 0, 0), 0f);
            LeanTween.scale(slot2, s2bs, 0.5f).setEase(Curve);
            }
        }
        if(Input.GetKeyDown(s3) && (slot2 == null || slot2.activeInHierarchy == false) && (slot1 == null || slot1.activeInHierarchy == false) && Time.time > switchTime && slot3 != null)
        {
            switchTime = Time.time + switchInterval;
            if(slot3.activeInHierarchy == true)
            {// item deselection
                LeanTween.scale(slot3, new Vector3 (0, 0, 0), 0.5f).setEase(Curve);
                StartCoroutine(ds1());
            }
            else
            {// item reselection
            slot3.SetActive(true);
            LeanTween.scale(slot3, new Vector3 (0, 0, 0), 0f);
            LeanTween.scale(slot3, s3bs, 0.5f).setEase(Curve);
            }
        }
    }
    private void pickup()
    {
        s1bs = slot1.transform.localScale;
        s2bs = slot2.transform.localScale;
        s3bs = slot3.transform.localScale;
    }
    IEnumerator ds1()
    {
        yield return new WaitForSeconds(switchInterval + 0.1f);
        if (slot1 != null){                            //all of this disables the rigidbodies and colliders every time you switch an item to prevent problems
        slot1.SetActive(false); if (slot1.GetComponent<Collider>() != null){slot1.GetComponent<Collider>().enabled = false;} slot1.GetComponent<Rigidbody>().isKinematic = true; if(slot1.GetComponent<Animator>() != null){slot1.GetComponent<Animator>().enabled = true;}}
        if (slot2 != null){
        slot2.SetActive(false); if (slot2.GetComponent<Collider>() != null){slot2.GetComponent<Collider>().enabled = false;} slot2.GetComponent<Rigidbody>().isKinematic = true; if(slot2.GetComponent<Animator>() != null){slot2.GetComponent<Animator>().enabled = true;}}
        if (slot3 != null){
        slot3.SetActive(false); if (slot3.GetComponent<Collider>() != null){slot3.GetComponent<Collider>().enabled = false;} slot3.GetComponent<Rigidbody>().isKinematic = true; if(slot3.GetComponent<Animator>() != null){slot3.GetComponent<Animator>().enabled = true;}}
    }
}
