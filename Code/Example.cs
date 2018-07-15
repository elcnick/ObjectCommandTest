#if true
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ELCScript.Tween;
using System;

namespace ELCScript
{
    namespace Tween
    {
        /// <summary>
        /// 用來測試ELCScript.Tween與當作範例
        /// </summary>
        public class Example : MonoBehaviour
        {
            public Image img;
            public float Duration=30;
            public float Value ;
            public float To=1;
            public Vector2 Value2;
            public Vector2 To2 =new Vector2(1,1);
            public bool Signal=false;
            TweenParameter handle;

            public static bool XX;
            public static float XY;

            public static void TestRef(ref Vector2 v)
            {
                v = v + new Vector2(1,1);
            }
            public static void TestOut(out Vector2 v)
            {
                v = new Vector2(1, 1);
            }

            object getter()
            {
                return Value2;
            }
            void setter(object o)
            {
                Value2 = (Vector2)o;
            }

            public void Example1()
            {
                Value = 0;
                if (handle == null)
                    handle = this.Tween(Duration)
                        .BeforeStart(() => { Debug.Log("Started"); })
                        .AnchoredOffset(img.rectTransform, new Vector3(0, 100), V3Flag.Y).Ease(TweenFunctionType.easeInOutBounce)
                        .AnchoredOffset(img.rectTransform, new Vector3(100, 0), V3Flag.X)
                        .ColorFromTo(img, Color.white, Color.red)
                        .To(getter, setter, To2, V3Flag.X)
                        .OnFinal(() => Debug.Log("Finished"));
                handle.Play();
            }

            /// <summary>
            /// 這個Tween工具是參考DOTween但有不同程式碼撰寫風格的Tween工具
            /// </summary>
            public void Example3(GameObject go)
            {
                Image img = go.GetComponent<Image>();
                Transform tr = go.transform;
                tr
				    .Tween(10.0f)//一開始的tween將運作10秒
                        .MoveOffset(tr, new Vector3(0, 0, 10), true)//在該期間z軸位移10單位(localPosition)
                        .ColorTo(img, Color.red)//在上移同時將顏色轉變為紅色
                    .Tween(5.0f)//然後在接下來的5秒內
                        .MoveTo(tr, new Vector3(0, 0, 0), true)//回到位置0
                    .Play();//開始播放
            }


            /// <summary>
            /// 測試CoroutineVisitor(用來訪問IEnumerator在巢狀協程執行的工具)
            /// </summary>
            public void Example2()
            {
                StartCoroutine(new CoroutineVisitor(Co1()));
            }
            IEnumerator Co1()
            {
                Debug.Log("1-1");
                yield return new WaitForSeconds(1);
                yield return Co2(1.5f);
                yield return new WaitForSeconds(1);
                Debug.Log("1-2");
            }
            IEnumerator Co2(float v)
            {
                Debug.Log("2-1 "+v);
                v += 0.1f;
                yield return new WaitUntil(() => Signal);
                yield return new WaitForSeconds(1);
                Debug.Log("2-2 "+v);
            }
        }
    }
}
#endif
