using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Slider HPSlider;

    const int maxHP = 100;
    int HP = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HP -= 5;
            HPSlider.value = (float)HP / (float)maxHP;
            print("hp--");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ATK_Speed" ||  other.gameObject.tag == "ATK_Count")
        {
            Destroy(other.gameObject);
        }
    }
}
