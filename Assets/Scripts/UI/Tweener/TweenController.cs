using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TweenController : MonoBehaviour 
{
	public List<Tween> tweenDummy;

	public void TweenPlay(bool inverse)
	{
        foreach(Tween tween in tweenDummy)
		{
			tween.Play(inverse);
		}
	}
}
