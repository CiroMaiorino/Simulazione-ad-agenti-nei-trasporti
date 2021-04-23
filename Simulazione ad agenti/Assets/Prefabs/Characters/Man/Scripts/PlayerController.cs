using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    RaycastHit hit;

    public Camera cam;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;
    
    void Start ()
    {
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Ray ray = cam.ScreenPointToRay( Input.mousePosition );

            if ( Physics.Raycast( ray, out hit ) )
            {
                agent.SetDestination( hit.point );
            }
        }


        if ( agent.remainingDistance > agent.stoppingDistance )
        {
            character.Move( agent.desiredVelocity );
        }
        else
        {

            if( hit.transform.gameObject.CompareTag("PicturePlane") )
            {
                Debug.Log( "Colpisco", hit.transform.gameObject );

                Vector3 position = hit.transform.parent.GetComponent<RectTransform>().position;
                float angleBetweenPlayerAndTarget = Vector3.Angle( transform.forward, ( position - transform.position ) );

                if ( angleBetweenPlayerAndTarget > 10 )
                {
                    Debug.Log( "Mi volto" );
                    character.TurnToPicture( position );
                    return;
                }

            }

            character.Move( Vector3.zero );

            //Vector3 position = GameObject.FindWithTag( "Picture" ).transform.position;
            //float angleBetweenPlayerAndTarget = Vector3.Angle( transform.forward, ( position - transform.position ) );
            //if ( angleBetweenPlayerAndTarget > 10 )
            //{
            //    Debug.Log( "Mi volto" );
            //    character.TurnToPicture( position );

            //}
            //else
            //{
            //    character.Move( Vector3.zero );
            //}
        }

    }


}