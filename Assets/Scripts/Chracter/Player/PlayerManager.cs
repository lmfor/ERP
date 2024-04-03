using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class PlayerManager : ChracterManager
    {

        PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();
            // do more stuff below

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();


        }

        protected override void Update()
        {
            base.Update();
            // HANDLE ALL MOVEMENT
            if (!IsOwner)
            {
                return;
            }


            playerLocomotionManager.HandleAllMovement();
        }

    }
}

