using System.Collections;
using System.Collections.Generic;

namespace ELCScript
{
    public class CoroutineVisitor : IEnumerator
    {
        Stack<IEnumerator> stack = new Stack<IEnumerator>();

        public CoroutineVisitor()
        {

        }
        public CoroutineVisitor(IEnumerator co)
        {
            Start(co);
        }

        public void Start(IEnumerator co)
        {
            stack.Clear();
            stack.Push(co);
        }

        public object Current { get { return stack.Peek().Current; } }

        public void Reset()
        {
            throw new System.NotSupportedException("ELCScript.ProgramTool.CoroutineVisitor.Reset()");
        }

        public bool MoveNext()
        {
            Again:
            bool b = stack.Peek().MoveNext();
            if (!b)
            {
                stack.Pop();
                if (stack.Count == 0)
                    return false;
                else
                    goto Again;
            }
            object o = stack.Peek().Current;

#if UNITY_5_3_OR_NEWER
            if (!(o is UnityEngine.CustomYieldInstruction))
#endif
            {
                IEnumerator x = o as IEnumerator;
                if (x != null)
                {
                    stack.Push(x);
                    goto Again;
                }
            }

            return true;
        }
    }
}
