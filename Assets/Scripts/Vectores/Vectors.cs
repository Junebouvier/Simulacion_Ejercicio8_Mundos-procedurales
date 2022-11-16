using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Vectors : MonoBehaviour
{
    [SerializeField] MyVector myFirstVector = new MyVector(1, 3);
    [SerializeField] MyVector mySecondVector = new MyVector (2, 5);
    [Range(0, 1)][SerializeField] float escalar = 0; 

    private void Update()
    {
        MyVector differ = (mySecondVector - myFirstVector) * escalar;
        MyVector final = myFirstVector + differ;
        
        #region Debug
        
        myFirstVector.Draw(Color.red);
        mySecondVector.Draw(myFirstVector, Color.green);
        differ.Draw(Color.yellow);
        differ.Draw(mySecondVector, Color.yellow);
        final.Draw(Color.blue);
        
        #endregion
    }
    
}
