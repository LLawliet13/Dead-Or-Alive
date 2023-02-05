using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class ScreenHelper
    {
        public static Bounds OrthographicBounds(Camera camera)

        {

            float screenAspect = (float)Screen.width / (float)Screen.height;

            float cameraHeight = camera.orthographicSize * 2;

            Bounds bounds = new Bounds(

                camera.transform.position,

                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

            return bounds;

        }


    }
}
