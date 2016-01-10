using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLifeCycleManagement {

    /// <summary>
    /// Context class used to dispose objects.
    /// </summary>
    public class DisposeContext : IDisposable {

        private static Stack<IDisposable> _DisposableItems = new Stack<IDisposable>();

        public static void AddDisposeObject(IDisposable disposableObject) {
            _DisposableItems.Push(disposableObject);
        }

        public void Dispose() {
            IDisposable current = null;
            while (_DisposableItems.Count > 0) {
                current = _DisposableItems.Pop();
                current.Dispose();
            }
        }
    }
}
