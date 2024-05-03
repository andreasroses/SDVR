using System.Collections;
using UnityEngine;


public class Door : MonoBehaviour
{
    public bool isOpen=false;
    [SerializeField] private Vector3 slideDirection = Vector3.back;
    [SerializeField] private float slideAmount = 1.9f;
    [SerializeField] private float Speed = 1f;

    private Vector3 startPos;

    private void Awake() {
        startPos= transform.position;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            if(isOpen){
                Close();
            }else{
                OpenDoor();
            }
        }
    }

    // private void OnTriggerEnter(Collider other) {
    //     if(other.tag == "Player"){
    //         StartCoroutine(OpenClose());
    //     }
    // }

    public void OpenDoor(){
        StartCoroutine(SlideOpen());
    }

    public void Close(){
        StartCoroutine(SlideClose()); 
    }
    
    public IEnumerator OpenClose(){
        StartCoroutine(SlideOpen());
        yield return new WaitForSeconds(3f); 
        StartCoroutine(SlideClose()); 
    }


    public IEnumerator SlideOpen()
    {
        Vector3 endPos = startPos + slideAmount*slideDirection;
        Vector3 startPositon= transform.position;
        float time = 0;
        isOpen=true;
        while(time<1){
            transform.position = Vector3.Lerp(startPositon, endPos, time);
            yield return null;
            time+=Time.deltaTime * Speed;
        }
    }

    public IEnumerator SlideClose(){
        Vector3 endPos = startPos;
        Vector3 startPositon= transform.position;
        float time = 0;
        isOpen=false;
        while(time<1){
            transform.position = Vector3.Lerp(startPositon, endPos, time);
            yield return null;
            time+=Time.deltaTime * Speed;
        }
    }
    


}
