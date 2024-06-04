using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum TransformType
{
	Translate,
	Rotate,
	Alpha
}

public class Tween : MonoBehaviour 
{
	public AnimationCurve timeScale;

	public TransformType type;

	public bool inverse = false;

	public Vector3 startPos;
	public Vector3 finishPos;

	public bool play = false;

	private float timeDump = 0;

	private RectTransform rectTransformDummy;
	private Image m_image;

	void Start()
	{
		if(GetComponent<RectTransform>() != null)
			rectTransformDummy = GetComponent<RectTransform>();
		if(GetComponent<Image>() != null)
			m_image = GetComponent<Image>();
	}


	public void Play(bool _inverse)
	{
		timeDump = 0;
		inverse = _inverse;
		play = true;
	}

	public void Stop()
	{
		play = false;
	}

	void FixedUpdate () 
	{
	    if(play)
		{

			DoTween();
		}

    }

	void DoTween()
	{
		timeDump += Time.fixedDeltaTime;

		if(type == TransformType.Translate)
		{
			if(!inverse)
				rectTransformDummy.anchoredPosition = Vector2.Lerp(startPos,finishPos,timeScale.Evaluate(timeDump));
			else
				rectTransformDummy.anchoredPosition = Vector2.Lerp(finishPos,startPos,timeScale.Evaluate(timeDump));
		}

		if(type == TransformType.Rotate)
		{
			if(!inverse)
				rectTransformDummy.localEulerAngles = Vector3.Lerp(startPos,finishPos,timeScale.Evaluate(timeDump));
			else
				rectTransformDummy.localEulerAngles  = Vector3.Lerp(finishPos,startPos,timeScale.Evaluate(timeDump));
		}
		if(type == TransformType.Alpha)
		{
			if(!inverse)
				m_image.color = Color.Lerp(new Color(0,0,0,startPos.x),new Color(0,0,0,finishPos.x),timeScale.Evaluate(timeDump));
			else
				m_image.color = Color.Lerp(new Color(0,0,0,finishPos.x),new Color(0,0,0,startPos.x),timeScale.Evaluate(timeDump));
		}
	}
}
