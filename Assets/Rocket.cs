
using UnityEngine;
using UnityEngine.SceneManagement;
using AudioSource = UnityEngine.AudioSource;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    State state = State.Alive;

	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	    rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        //todo somewhere stop sound on death
	    if (state == State.Alive)
	    {
	        Rotate();
	        Thrust();
        }
	   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return; //ignore collisions when dead
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); //parameterise time
                break;
            default:
                print("Hit something deadly");
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation

        

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
 
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
