using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class ThreadQueue
    {
        private object _mutex = new object();
        private bool _executing = false;
        private Queue<Action> queue = new Queue<Action>();
        public void EnqueueAction(Action action)
        {
            lock (_mutex)
            {
                queue.Enqueue(action);
                ContinueExecuting();
            }
        }

        private void ContinueExecuting()
        {
            lock (_mutex)
            {
                if (!_executing)
                {
                    if (queue.Count > 0)
                    {
                        Action top = queue.Dequeue();
                        _executing = true;
                        Task.Run(() =>
                        {
#if !DEBUG
                            try
                            {
#endif
                                top();
#if !DEBUG
                            }
                            catch
                            {
                                 // Suppress all in release mode.
                            }
                            finally
                            {
#endif
                                lock (_mutex)
                                {
                                    _executing = false;
                                    ContinueExecuting();
                                }
#if !DEBUG
                            }
#endif
                        });
                    }
                }
            }
        }
    }
}
