using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        // think about goals in steps:
        // move character based off of those values
        public static PlayerInputManager instance;
        PlayerControls playerControls;
        [SerializeField] Vector2 movementInput;

        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

        

        private void Awake()
        {
            // when scene changes, this function is running
            if (instance == null)
            {
                instance = this;
            } else 
            { 
                Destroy(gameObject);
            }

            
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            //SceneManager.activeSceneChanged += OnSceneChange;
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
            
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            //if loading into world, enable player inputs, else, not
            if (newScene.buildIndex == WorldSaveGameManager.instance.worldSceneIndex)
            {
                instance.enabled = true;
            } else
            {
                instance.enabled = false;
            }

        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // if we destroy this object, unsubscribe from the event
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else 
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            } else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }
        }
    }

}


