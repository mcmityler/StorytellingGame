using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceSplatScript : MonoBehaviour
{
    [SerializeField] List<Sprite> _splatSprites;
    // Start is called before the first frame update
    void Awake()
    {
        RandomSplat(); //randomly select a splat spite, rotation, and then plat its splat animation (sound is played automaticall on awake in inspector)
        Destroy(this.gameObject, 1.5f);
    }
    void RandomSplat()
    {
        
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); //give the splat a random rotation
        this.gameObject.GetComponent<Image>().sprite = _splatSprites[Random.Range(0, _splatSprites.Count)];
        this.gameObject.GetComponent<Animator>().SetTrigger("Splat");
    }
}
