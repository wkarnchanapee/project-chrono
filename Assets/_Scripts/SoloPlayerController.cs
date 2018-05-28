using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class SoloPlayerController : MonoBehaviour {

    public static SoloPlayerController main;
    public string state = "normal";

    [SerializeField] GameObject recentEchoLoop;
    [SerializeField] int activeTimeLoops = 0;
    public int maxTimeLoops = 1;
	public float moveSpd;
    public float grabDist;
    public bool holding = false;
    public float holdDist = 2.5f;
    Transform pickupObj;
    
	public Vector3 camOffset = Vector3.zero;
    public bool active = true;
    bool recording = false;
   

    //
    public GameState gameState;

    CapsuleCollider coll;
    public Renderer rend;

    public GameObject echoPrefab;

    public Transform camObj;

    int rewindSteps = 0;
    public float rewindCounter = 0;
    public float rewindLimit = 7f;
    public int maxAmountOfSteps = 700;
    int sendStep = 0;
    object[,] recordArray = new object[4000,6];
    object[,] sendArray = new object[4000, 6];
    object[] tempArray = new object[6];
    public List<GameObject> echoList = new List<GameObject>();
    public CheckpointControl checkpoint;

    [SerializeField] GameObject GameControllerPrefab;
    SoloAiming horzAim, vertAim;
    public float pickupDist = 2f;
    public float pickupRange = 2f;
    Transform grabbedObj = null;
    bool isGrabbing = false;

    //unityevents
    public UnityEvent resetEvent;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        main = this;

    }
    // Use this for initialization
    void Start () {

        //create unity event
        if (resetEvent == null) resetEvent = new UnityEvent();

        camObj = transform.GetChild(0);
        camOffset = camObj.transform.position - transform.position;

        // Get collider component
        coll = GetComponent<CapsuleCollider>();
        // Get Aiming scripts
        horzAim = GetComponent<SoloAiming>();
        vertAim = GetComponentInChildren<SoloAiming>();

        ResetArrays();

        //start game
        gameState.state = "live";
        
    }
    // Update is called once per frame
    private void Update()
    {
        if (gameState.state == "live")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                active = false;
            }
            else
            {
                //active = true;
            }

            if (Input.GetButtonDown("Fire2"))
            {

                if (grabbedObj != null)
                {   // drop your current held object.
                    grabbedObj.GetComponent<GrabbableObject>().col.isTrigger = false;
                    grabbedObj.GetComponent<GrabbableObject>().isGrabbed = false;
                    grabbedObj = null;

                } else
                {   //attempt to grab something.
                    RaycastHit hit;
                    if (Physics.Raycast(camObj.position, camObj.forward, out hit, pickupRange))
                    {
                        if (hit.transform.tag == "pickup")
                        {
                            //resetEvent.AddListener(hit.transform.GetComponent<GrabbableObject>().Reset);
                            hit.transform.GetComponent<GrabbableObject>().isGrabbed = true;
                            grabbedObj = hit.transform;
                            pickupDist = Vector3.Distance(camObj.position, grabbedObj.position);
                        }
                    }
                    else
                    {
                        print("nothing to grab");
                    }

                }

            }

            //reset game button
            if (Input.GetKeyDown(KeyCode.R))
            {
                Die();

            }

            // check for and record fire button
            if (Input.GetButton("Fire1"))
            {
                recordArray[0, 3] = true;
            }

            if (state == "rewinding")
            {
                if (Input.GetButtonUp("Fire1") || rewindCounter >= rewindLimit)
                {
                    state = "normal";
                    CreateTimeLoop();
                    sendStep = 0;
                    rewindCounter = 0f;
                }
            }
            
        }
        
    }
    void FixedUpdate () {

        switch (gameState.state)
        {
            case "start":
                break;
            case "live":

                if (active == true)
                {
                    // Record pos and rotation
                    recordArray[0, 0] = transform.position;
                    recordArray[0, 1] = transform.rotation;

                    // record camera rotation
                    recordArray[0, 2] = camObj.rotation;

                    //record aiming script rotation
                    recordArray[0, 4] = horzAim.RotX;
                    recordArray[0, 5] = vertAim.RotY;

                    //Set Cam Dist
                    SetCameraOffset();

                    //maxAmountOfSteps = rewindLimit * 60;
                    //shuffle recorded steps
                    for (int i = maxAmountOfSteps - 1; i >= 0; i--)
                    {
                        if (i != 0)
                        {                       
                            recordArray[i, 0] = recordArray[i - 1, 0];
                            recordArray[i, 1] = recordArray[i - 1, 1];
                            recordArray[i, 2] = recordArray[i - 1, 2];
                            recordArray[i, 3] = recordArray[i - 1, 3];
                            recordArray[i, 4] = recordArray[i - 1, 4];
                            recordArray[i, 5] = recordArray[i - 1, 5];
                        }
                    }
                }
                else
                {
                    PlaybackCharacterActions();
                }

                break;
            case "pre-rewind":
                break;
            case "rewind":
                
                PlaybackCharacterActions();
                break;
        }
            
	}

    float Fibonnaci(int n)
    {
        int f = 0;
        float[] sequence = new float[n];

        int v = 1;
        int vv = 1;

        sequence[0] = v;
        sequence[1] = vv;

        // generate fibonnaci sequence
        for (int i = 2; i < n; i++)
        {
            sequence[i] = sequence[i - 1] + sequence[i - 2];

        }

        for (int i = 0; i < n; i++)
        {

            v = 

            f = v + vv;
             
            
        }

        return f;
        
    }

    void PlaybackCharacterActions()
    {
        state = "rewinding";
        // increment rewindcounter
        rewindCounter += Time.deltaTime;

        // Get events for recordArray and set them.
        transform.position = (Vector3)recordArray[0, 0];
        transform.rotation = (Quaternion)recordArray[0, 1];
        camObj.rotation = (Quaternion)recordArray[0, 2];

        // Set aiming direction
        horzAim.RotX = (float)recordArray[0, 4];
        vertAim.RotY = (float)recordArray[0, 5];

        tempArray[0] = (Vector3)recordArray[0, 0];
        tempArray[1] = (Quaternion)recordArray[0, 1];
        tempArray[2] = (Quaternion)recordArray[0, 2];
        tempArray[3] = (bool)recordArray[0, 3];
        tempArray[4] = (float)recordArray[0, 4];
        tempArray[5] = (float)recordArray[0, 5];

        for (int i = 0; i < maxAmountOfSteps; i++)
        {           
            if ( i != maxAmountOfSteps-1)
            {
                recordArray[i, 0] = recordArray[i + 1, 0];
                recordArray[i, 1] = recordArray[i + 1, 1];
                recordArray[i, 2] = recordArray[i + 1, 2];
                recordArray[i, 3] = recordArray[i + 1, 3];
                recordArray[i, 4] = recordArray[i + 1, 4];
                recordArray[i, 5] = recordArray[i + 1, 5];
            }
        }

        // Loop the information back to the end
        recordArray[maxAmountOfSteps - 1, 0] = (Vector3)tempArray[0];
        recordArray[maxAmountOfSteps - 1, 1] = (Quaternion)tempArray[1];
        recordArray[maxAmountOfSteps - 1, 2] = (Quaternion)tempArray[2];
        recordArray[maxAmountOfSteps - 1, 3] = (bool)tempArray[3];
        recordArray[maxAmountOfSteps - 1, 4] = (float)tempArray[4];
        recordArray[maxAmountOfSteps - 1, 5] = (float)tempArray[5];  


        sendArray[sendStep, 0] = tempArray[0];
        sendArray[sendStep, 1] = tempArray[1];
        sendArray[sendStep, 2] = tempArray[2];
        sendArray[sendStep, 3] = tempArray[3];
        sendArray[sendStep, 4] = tempArray[4];
        sendArray[sendStep, 5] = tempArray[5];

        sendStep++;

        /*
        if (Input.GetButtonUp("Fire1") || rewindCounter >= rewindLimit)
        {
            state = "normal";
            CreateTimeLoop();
            sendStep = 0;
            rewindCounter = 0f;
        }
        */
    }
    public void Die()
    {
        transform.position = checkpoint.gameObject.transform.position;
        transform.rotation = checkpoint.gameObject.transform.rotation;

        ResetEchoes();
        gameState.ResetState();
        grabbedObj = null;
        ResetArrays();
        resetEvent.Invoke();
    }

    public void RegisterResetListener(GameObject other)
    {   //this function registers with the player game object to check when the player Reset function is called.
        
        switch (other.tag)
        {
            case "pickup":
                resetEvent.AddListener(other.GetComponent<GrabbableObject>().Reset);
                break;
        }
        
            
    }
    public void ResetEchoes()
    {
        for (int i = 0; i < echoList.Count; i++)
        {
            Destroy(echoList[i]);
        }

        echoList.Clear();
        activeTimeLoops = 0;
        
    }
    
    void CreateTimeLoop()
    {
        active = true;
        if (activeTimeLoops < maxTimeLoops)
        {
            activeTimeLoops++;
            GameObject inst = Instantiate(echoPrefab, transform.position, transform.rotation);
            recentEchoLoop = inst;
            EchoController echoCtrl = inst.GetComponent<EchoController>();
            
            echoList.Insert(0, inst);

            for (int i = 0; i < sendStep; i++)
            {
                echoCtrl.recordArray[i, 0] = (Vector3)sendArray[i, 0];
                echoCtrl.recordArray[i, 1] = (Quaternion)sendArray[i, 1];
                echoCtrl.recordArray[i, 2] = (Quaternion)sendArray[i, 2];
                echoCtrl.recordArray[i, 3] = (bool)sendArray[i, 3];
                echoCtrl.recordArray[i, 4] = (float)sendArray[i, 4];
                echoCtrl.recordArray[i, 5] = (float)sendArray[i, 5];

                echoCtrl.endStep = sendStep;
            }
        } else if (activeTimeLoops >= maxTimeLoops)
        {
            
            //destroy oldest echo gameobject
            Destroy(echoList[maxTimeLoops-1]);
            //remove oldest echo
            echoList.RemoveAt(maxTimeLoops-1);

            // create new echo
            GameObject inst = Instantiate(echoPrefab, transform.position, transform.rotation);
            recentEchoLoop = inst;
            EchoController echoCtrl = inst.GetComponent<EchoController>();
            //add new echo to list.
            echoList.Insert(0, inst);
            

            for (int i = 0; i < sendStep; i++)
            {
                echoCtrl.recordArray[i, 0] = (Vector3)sendArray[i, 0];
                echoCtrl.recordArray[i, 1] = (Quaternion)sendArray[i, 1];
                echoCtrl.recordArray[i, 2] = (Quaternion)sendArray[i, 2];
                echoCtrl.recordArray[i, 3] = (bool)sendArray[i, 3];
                echoCtrl.recordArray[i, 4] = (float)sendArray[i, 4];
                echoCtrl.recordArray[i, 5] = (float)sendArray[i, 5];

                echoCtrl.endStep = sendStep;
            }
        }
    }
	void SetCameraOffset()
	{	camObj.transform.position = transform.position + camOffset;
	}
    void ResetArrays()
    {
        //initialise array values.
        for (int i = 0; i < maxAmountOfSteps; i++)
        {
            recordArray[i, 0] = Vector3.zero;
            recordArray[i, 1] = Quaternion.identity;
            recordArray[i, 2] = Quaternion.identity;
            recordArray[i, 3] = false;
            recordArray[i, 4] = 0f;
            recordArray[i, 5] = 0f;

        }

        tempArray[0] = Vector3.zero;
        tempArray[1] = Quaternion.identity;
        tempArray[2] = Quaternion.identity;
        tempArray[3] = false;
        tempArray[4] = 0f;
        tempArray[5] = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "key")
        {
            Destroy(other.gameObject);
            SoloGameController.main.keys += 1;
        }
    }

}
