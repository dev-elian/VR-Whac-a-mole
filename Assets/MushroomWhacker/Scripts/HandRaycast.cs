using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRaycast : MonoBehaviour
{
    GameObject _selectedObject;

    void FixedUpdate(){
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Hammers");
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30, layerMask)){
            if (_selectedObject != hit.collider.gameObject){
                if (_selectedObject != null)
                    _selectedObject.GetComponent<IHammerActions>().DropHammer(); 
                hit.collider.GetComponent<IHammerActions>().GetHammer();                
            }
            _selectedObject = hit.collider.gameObject;
        }else{
            if (_selectedObject != null)
                _selectedObject.GetComponent<IHammerActions>().DropHammer();  
            _selectedObject = null;
        }
    }
}
