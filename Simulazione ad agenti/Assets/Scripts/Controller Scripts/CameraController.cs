using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class create the controls for the camera. */
public class CameraController : MonoBehaviour
{
    /* The values for the movement and rotation speed */
    public float movementSpeed, movementTime, rotationAmount;
    /*Transfrom of the camera to get her position*/
    public Transform cameraTransform;
    private bool follow;
    public Bus bus;
    /*The amout of zoom*/
    public Vector3 zoomAmount;
    Vector3 newZoom;
    Vector3 newPosition;
    Vector3 startingPosition;
    Vector3 dragStartPosition, dragCurrentPosition;
    Vector3 rotateStartPosition, rotateCurrentPosition;
    Quaternion newRotation;
    // Start is called before the first frame update

    void Start()
    {
        startingPosition = transform.position;
        newPosition=transform.position;    
        newRotation=transform.rotation;
        newZoom=cameraTransform.localPosition;
     
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
        FollowBus();
        if (follow)
            transform.position = bus.transform.position + startingPosition;

    }

    void HandleMovementInput(){
        /* Move Forward. */
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
            newPosition+=transform.forward*movementSpeed;
        }
        /*Move Backward*/
        if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
            newPosition-=transform.forward*movementSpeed;
        }
        /*Move to Right*/
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
            newPosition+=transform.right*movementSpeed;
        }
        /* Move to Left. */
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
            newPosition-=transform.right*movementSpeed;
        }
        /* Turn right. */
        if(Input.GetKey(KeyCode.E)){
            newRotation*=Quaternion.Euler(Vector3.up*rotationAmount);
        }
        /* Turn Left. */
        if(Input.GetKey(KeyCode.Q)){
            newRotation*=Quaternion.Euler(Vector3.up*-rotationAmount);
        }
     
        transform.position=Vector3.Lerp(transform.position,newPosition,Time.deltaTime*movementTime);
        transform.rotation=Quaternion.Lerp(transform.rotation,newRotation,Time.deltaTime*movementTime);
    }


    void HandleMouseInput(){

        //Start drag move
        if(Input.GetMouseButtonDown(0)){
            Plane plane=new Plane(Vector3.up,Vector3.zero);
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if(plane.Raycast(ray,out entry)){
                dragStartPosition=ray.GetPoint(entry);
            }
        }
        //Stop drag move
        if(Input.GetMouseButton(0)){
            Plane plane=new Plane(Vector3.up,Vector3.zero);
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if(plane.Raycast(ray,out entry)){
                dragCurrentPosition=ray.GetPoint(entry);
                newPosition=transform.position+dragStartPosition-dragCurrentPosition;
            }
        }
        //Zoom
        if(Input.mouseScrollDelta.y!=0){

           Vector3 zoomValue=Input.mouseScrollDelta.y*zoomAmount;//Valore dello zoom

            if((zoomValue+newZoom).y >= -0.5 && (zoomValue+newZoom).y <= 35) //se sta nei limiti modifica newZoom
                newZoom+=zoomValue;
        }

        if(Input.GetMouseButtonDown(1)){
            rotateStartPosition=Input.mousePosition;
        }
        
        if(Input.GetMouseButton(1)){
            rotateCurrentPosition=Input.mousePosition;
            Vector3 difference=rotateStartPosition-rotateCurrentPosition;
            rotateStartPosition=rotateCurrentPosition;
            Debug.Log(difference);
            newRotation*=Quaternion.Euler(Vector3.up*(-difference.x/5f));
       
        }



        cameraTransform.localPosition=Vector3.Lerp(cameraTransform.localPosition,newZoom,Time.deltaTime*movementTime);
    }

    void FollowBus()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            follow = !follow;
        }
    }
}
