using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsMachine : MonoBehaviour
{
    [SerializeField] float _timeToChange;
    [SerializeField] MeshRenderer _rend;

    [SerializeField] List<Texture> _textures;
    int idx=0;
    
    void OnEnable() {
        StartCoroutine(IntermitenLight());
    }

    IEnumerator IntermitenLight(){
        while (true){
            yield return new WaitForSeconds(_timeToChange);
            Debug.Log(idx);
            _rend.materials[2].SetTexture ("_EmissionMap", _textures[idx]);
            idx = (idx+1>=_textures.Count?0:idx+1);
        }
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}
