using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTester : MonoBehaviour
{
    private Tester tester;
    public bool isEnabled = false;
    public bool isEnabledS = false;
    // Start is called before the first frame update
    void Start()
    {
        tester = GetComponent<Tester>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isEnabled) {
        //    tester.enabled = true;
        //}
        if (isEnabledS) {
            tester.gameObject.SetActive(true);
        }
        //if (!isEnabled) {
        //    tester.enabled = false;
        //}
        if (!isEnabledS) {
            tester.gameObject.SetActive(false);
        }

    }
}
