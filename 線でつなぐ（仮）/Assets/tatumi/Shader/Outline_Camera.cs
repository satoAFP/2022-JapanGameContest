using UnityEngine;
using System.Collections;

public class Outline_Camera : MonoBehaviour
{

	public Material Outline_PostEff;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, Outline_PostEff);
	}
}