using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.2f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		CapsuleCollider m_Capsule;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}


		public void Move(Vector3 move)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}


		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);


			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			m_Animator.speed = 1;
			
		}

        public void TurnToPicture(Vector3 move )
        {
        
            Quaternion targetRotation = Quaternion.LookRotation( move - transform.position );
            targetRotation.x = 0f;
            targetRotation.z = 0f;

            transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, Time.deltaTime * 1.3f );

            float angleBetweenPlayerAndTarget = Vector3.Angle( transform.forward, ( move - transform.position ) );

            Debug.Log( "Angolo: " + angleBetweenPlayerAndTarget );

            if ( angleBetweenPlayerAndTarget > 40 )
            {
                move = move.normalized;
                move = transform.InverseTransformDirection( move );
                move = Vector3.ProjectOnPlane( move, m_GroundNormal );
                m_TurnAmount = Mathf.Atan2( move.x, move.z );
                m_Animator.SetFloat( "Forward", 0, 1f, Time.deltaTime );
                m_Animator.SetFloat( "Turn", m_TurnAmount, 0.2f, Time.deltaTime );

            }
            else
            {
                m_Animator.SetFloat( "Forward", 0, 1f, Time.deltaTime );
                m_Animator.SetFloat( "Turn", 0, 0.2f, Time.deltaTime );
            }

            Debug.Log( "Forward: " + m_Animator + " | Turn: " + m_TurnAmount);


        }

        void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


	}
}
