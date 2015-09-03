// I got a problem to know if a renderer was visible from a camera
// I have found this on this unity3d wiki
// http://wiki.unity3d.com/index.php?title=IsVisibleFrom

using UnityEngine;
 
public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}