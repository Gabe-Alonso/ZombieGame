using UnityEngine;

public class EyeBlinks : MonoBehaviour
{
    public GameObject eyeLidsL;
    public GameObject eyeLidsR;
    private float _time = 0;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time > .025f)
        {
            eyeLidsL.SetActive(false);
            eyeLidsR.SetActive(false);
        }

        if(_time > 1f)
        {
            if(Random.Range(0,50) == 0)
            {
                eyeLidsL.SetActive(true);
                eyeLidsR.SetActive(true);
                _time = 0;
            }
        }
    }
}
