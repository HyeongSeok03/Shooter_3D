using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject hitParticle;
    private int maxHP;
    public int HP = 3;
    public Slider HPSlider;

    public float speed;
    Animator animator;
    bool isDead = false;

    private void Awake()
    {
        maxHP = HP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            SoundManager.instance.AudioStart(0);
            HP--;

            Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);

            HPSlider.value = (float)HP / (float)maxHP;

            if (HPSlider.gameObject.activeSelf == false)
            {
                HPSlider.gameObject.SetActive(true);
            }

            if (isDead == false)
            {
                Destroy(other.transform.parent.gameObject);

                if(HP <= 0)
                {
                    isDead = true;
                    animator.SetTrigger("DEATH");
                    GetComponent<CapsuleCollider>().enabled = false;
                    Destroy(GetComponent<Rigidbody>());

                    HPSlider.gameObject.SetActive (false);
                }
            }

        }
    }
}
