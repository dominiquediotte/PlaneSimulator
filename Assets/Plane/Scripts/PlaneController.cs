using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float m_HorizontalTurnSpeed = 5.0f;
    public float m_VerticalTurnSpeed = 5.0f;
    public float m_PropellerSpeed = 5.0f;
    public float m_VerticalBoostCompensation = 2.0f;
    public AudioClip m_EngineSound;

    private Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;
    [SerializeField]
    private ParticleSystem m_Smoke;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            TurnHorizontaly(horizontalInput);
        }

        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            TurnVerticaly(verticalInput);
        }

        float boostInput = Input.GetAxis("Jump");
        Debug.Log(boostInput);
        if (boostInput > 0.0000000001)
        {
            Propel(boostInput);
        }
        else
        {
            if (!m_Smoke.isStopped && m_Smoke.isPlaying)
            {
                m_Smoke.Stop();
            }
        }
    }

    private void Propel(float input)
    {
        if (!m_AudioSource.isPlaying)
        {
            PlaySound(m_EngineSound);
        }
        if (m_Smoke.isStopped && !m_Smoke.isPlaying)
        {
            m_Smoke.Play();
        }
        
        m_Rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * input * m_PropellerSpeed * Time.deltaTime + new Vector3(0, m_VerticalBoostCompensation * Time.deltaTime, 0));
    }

    private void TurnHorizontaly(float input)
    {
        transform.Rotate(new Vector3(0, 0, 1), -input * m_HorizontalTurnSpeed * Time.deltaTime);
    }

    private void TurnVerticaly(float input)
    {
        transform.Rotate(new Vector3(1, 0, 0), input * m_VerticalTurnSpeed * Time.deltaTime);
    }

    private void PlaySound(AudioClip sound)
    {
        m_AudioSource.clip = sound;
        m_AudioSource.Play();
    }
}
