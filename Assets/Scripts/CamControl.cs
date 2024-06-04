using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour
{
    public float moveSensitivityX = 10.0f;
    public float moveSensitivityY = 10.0f;
    public bool invertMoveX = false;
    public bool invertMoveY = false;

    private Camera m_camera;

    // Use this for initialization
    void Start()
    {
        m_camera = Camera.main;
		//Loading options
		GetComponent<GrayscaleEffect>().enabled = (PlayerPrefs.GetString("NightVisionColor") == "True");
		invertMoveX = (PlayerPrefs.GetString("InvertX") == "True");
		invertMoveY = (PlayerPrefs.GetString("InvertY") == "True");
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;

        if (touches.Length > 0)
        {
            if (touches.Length >= 1)
            {
                if (touches[0].phase == TouchPhase.Moved)
                {
                    Vector2 delta = touches[0].deltaPosition;

                    float positionX = delta.x * moveSensitivityX * Time.deltaTime;

                    float positionZ = delta.y * moveSensitivityY * Time.deltaTime;

                    m_camera.transform.localEulerAngles += new Vector3(-positionZ, -positionX, 0);
                }
            }
        }
      }
}
