using Photon.Pun;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class Movement : MonoBehaviour
{
	public float acceleration = 50; 
	public float accSprintMultiplier = 4; 
	public float lookSensitivity = 1; 
	public float dampingCoefficient = 5; 
	public bool focusOnEnable = true;  

	Vector3 velocity;

	[Header("References")]
	public static Movement instance;

	public PhotonView view;

	public TextMeshProUGUI statsText;

	TowerDefenseGameManager manager;

	public int kills, waves;

	void Awake()
	{
		view = GetComponent<PhotonView>();

		manager = FindObjectOfType<TowerDefenseGameManager>();

		instance = this;

		if (view.IsMine) FindObjectOfType<TowerDefenseGameManager>().player = this;
	}

	void Update()
	{
		if (!view.IsMine) return;

		if (FindObjectOfType<PhotonRoom>() != null) view.RPC("UpdateStats", RpcTarget.All);

        // Input
        if (!Cursor.visible) UpdateInput();

		if (Input.GetMouseButtonDown(0) && MenuManager.instance.isOpen) lockCursor();

        // Physics
        velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}

    private void Start()
    {
		lockCursor();
    }

    private static void lockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

	[PunRPC]
	public void UpdateStats()
	{
		if (manager != null)
		{
			waves = manager.currentWave;
			kills = 0;

			statsText.text = "Total Kills: " + kills + "\n" + "Waves Passed: " + waves;
		}
	}

	void UpdateInput()
	{
		// Position
		velocity += GetAccelerationVector() * Time.deltaTime;

		// Rotation
		Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		Quaternion rotation = transform.rotation;
		Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
		Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
		transform.rotation = horiz * rotation * vert;

	}

	Vector3 GetAccelerationVector()
	{
		Vector3 moveInput = default;

		void AddMovement(KeyCode key, Vector3 dir)
		{
			if (Input.GetKey(key))
				moveInput += dir;
		}

		AddMovement(KeyCode.W, Vector3.forward);
		AddMovement(KeyCode.S, Vector3.back);
		AddMovement(KeyCode.D, Vector3.right);
		AddMovement(KeyCode.A, Vector3.left);
		AddMovement(KeyCode.Space, transform.InverseTransformDirection(Vector3.up));
		AddMovement(KeyCode.LeftShift, transform.InverseTransformDirection(Vector3.down));
		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if (Input.GetKey(KeyCode.LeftControl))
			return direction * (acceleration * accSprintMultiplier); // "sprinting"
		return direction * acceleration; // "walking"
	}
}