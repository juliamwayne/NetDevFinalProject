using Unity.Netcode;
using UnityEngine;


public class Player : NetworkBehaviour
{


    public NetworkVariable<Vector3> PositionChange = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> RotationChange = new NetworkVariable<Vector3>();
    public NetworkVariable<Color> PlayerColor = new NetworkVariable<Color>(Color.red);
    public NetworkVariable<int> Score = new NetworkVariable<int>(50);

    private GameManager _gameMgr;

    private Camera _camera;

    public float movementSpeed = .5f;
    private float rotationSpeed = 4f;

    private BulletSpawner _bulletSpawner;

    public TMPro.TMP_Text txtScoreDisplay;

    private void Start()
    {
        ApplyPlayerColor();
        PlayerColor.OnValueChanged += OnPlayerColorChanged;
        //_bulletSpawner = transform.Find("RArm").transform.Find("BulletSpawner").GetComponent<BulletSpawner>();
    }

    public override void OnNetworkSpawn()
    {
        _camera = transform.Find("Camera").GetComponent<Camera>();
        _camera.enabled = IsOwner;

        //txtScoreDisplay.text = $"P: {NetworkManager.Singleton.LocalClientId}";

        _bulletSpawner = transform.Find("RArm").transform.Find("BulletSpawner").GetComponent<BulletSpawner>();

        Score.OnValueChanged += ClientOnScoreChanged;
        //RequestScoreServerRpc(50);

        if (IsHost)
        {
            _bulletSpawner.netBulletDamage.Value = 1;
        }

        //DisplayScore();
    }

    void ClientOnScoreChanged(int previous, int current)
    {
        //txtScoreDisplay.text = current.ToString();
        //DisplayScore();
    }

    public void DisplayScore()
    {
        txtScoreDisplay.text = Score.Value.ToString();
    }

    [ServerRpc]
    public void RequestScoreServerRpc(int score)
    {
        Score.Value = score;
    }

    [ServerRpc]
    void RequestPositionForMovementServerRpc(Vector3 posChange, Vector3 rotChange)
    {
        if (!IsServer && !IsHost) return;

        PositionChange.Value = posChange;
        RotationChange.Value = rotChange;
    }


    public void OnPlayerColorChanged(Color previous, Color current)
    {
        ApplyPlayerColor();
    }

    public void ApplyPlayerColor()
    {
        GetComponent<MeshRenderer>().material.color = PlayerColor.Value;
        transform.Find("LArm").GetComponent<MeshRenderer>().material.color = PlayerColor.Value;
        transform.Find("RArm").GetComponent<MeshRenderer>().material.color = PlayerColor.Value;
    }


    // horiz changes y rotation or x movement if shift down, vertical moves forward and back.
    private Vector3[] CalcMovement()
    {
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float x_move = 0.0f;
        float z_move = Input.GetAxis("Vertical");
        float y_rot = 0.0f;

        if (isShiftKeyDown)
        {
            x_move = Input.GetAxis("Horizontal");
        }
        else
        {
            y_rot = Input.GetAxis("Horizontal");
        }

        Vector3 moveVect = new Vector3(x_move, 0, z_move);
        moveVect *= movementSpeed;

        Vector3 rotVect = new Vector3(0, y_rot, 0);
        rotVect *= rotationSpeed;

        return new[] { moveVect, rotVect };
    }


    void Update()
    {
        if (IsOwner)
        {
            Vector3[] results = CalcMovement();
            RequestPositionForMovementServerRpc(results[0], results[1]);

            /*
            if (Input.GetButtonDown("Fire1"))
            {
                _bulletSpawner.FireServerRpc();
            }
            */
        }

        if (!IsOwner || IsHost)
        {
            transform.Translate(PositionChange.Value);
            transform.Rotate(RotationChange.Value);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        /*
        if (IsHost)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                HostHandleBulletCollision(collision.gameObject);
            }
        }
        */

        if (IsOwner)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                Debug.Log("NPC says hello!");
            }
        }

        if (IsOwner)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Oh no, an enemy!");
            }
        }
    }

    void HostHandleBulletCollision(GameObject bullet)
    {
        ulong ownerClientId = bullet.GetComponent<NetworkObject>().OwnerClientId;
        Player otherPlayer = NetworkManager.Singleton.ConnectedClients[ownerClientId].PlayerObject.GetComponent<Player>();
        BulletClass bulletClass = bullet.GetComponent<BulletClass>();

        Score.Value -= bulletClass.netDamage.Value;
        otherPlayer.Score.Value += bulletClass.netDamage.Value;

        Destroy(bullet);
    }


    void HostHandleDamageBoostPickup(Collider other)
    {
        if(!_bulletSpawner.IsAtMaxDmg())
        {
            other.GetComponent<NetworkObject>().Despawn();
            _bulletSpawner.IncreaseDamage();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(IsHost)
        {
            if(other.gameObject.CompareTag("DamageBoost"))
            {
                HostHandleDamageBoostPickup(other);
            }
        }
    }
}