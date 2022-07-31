using UnityEngine;

namespace Extentions
{
    public class Timer
    {
        public void WaitForSec(float second)
        {
            /*if (countTimer > second)
            {
                countTimer = 0;
                return true;
            }
            else
            {
                countTimer += Time.deltaTime;
                return false;
            }*/
            var seconds = second;
            while (second > 0)
            {
                second -= Time.deltaTime;
            }
            second = seconds;

        }
    }

}