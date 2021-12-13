using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Memento
{
    internal class MementoController
    {
        private readonly List<IMemento> _mementos = new();

        public Originator _originator = null;

        public MementoController(Originator originator)
        {
            _originator = originator;
        }

        public void Backup()
        {
            _mementos.Add(_originator.Save());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
            {
                return;
            }

            IMemento memento = _mementos.Last();
            _mementos.Remove(memento);


            try
            {
                _originator.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }

        public void ShowHistory()
        {
            foreach (IMemento memento in _mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
}
