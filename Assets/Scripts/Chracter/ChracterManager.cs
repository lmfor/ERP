using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SG
{
    public class ChracterManager : NetworkBehaviour
    {

        public CharacterController characterController;

        CharacterNetworkManager characterNetworkManager;


        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            characterController = GetComponent<CharacterController>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                characterNetworkManager.networkPos.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            } else
            {
                //pos
                transform.position = Vector3.SmoothDamp(transform.position, 
                    characterNetworkManager.networkPos.Value, 
                    ref characterNetworkManager.networkPositionVelocity,
                    characterNetworkManager.networkPositionSmoothTime);

                //rot
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    characterNetworkManager.networkRotation.Value, 
                    characterNetworkManager.networkRotationSmoothTime);
            }
        }

    }

}
