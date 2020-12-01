using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGen : MonoBehaviour 
{
    public void instantiateSpheres(List<Transform> joints, GameObject sphere) {
        foreach (Transform t in joints) {
                Instantiate(sphere, t);
        }
    }

}
