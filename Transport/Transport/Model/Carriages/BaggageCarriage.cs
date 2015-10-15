using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    class BaggageCarriage : Carriage, ICollection<Baggage>, IBaggageElement
    {
        public uint CellsCount { get; set; }
        public uint CellCapacity { get; set; }

        private readonly Dictionary<int, Baggage> _dictionaryBaggages;

        public BaggageCarriage(int number, DateTime startUpDate, uint axisNumber, uint cellsCount, uint cellCapacity)
            : base(number, startUpDate, axisNumber)
        {
            this._dictionaryBaggages = new Dictionary<int, Baggage>();
            this.CellCapacity = cellCapacity;
            this.CellsCount = cellsCount;
        }

        public Baggage this[int number]
        {
            get { return _dictionaryBaggages.FirstOrDefault(x => x.Value.Number == number).Value; }
        }

        public Baggage this[string name]
        {
            get { return _dictionaryBaggages.FirstOrDefault(x => x.Value.Name == name).Value; }
        }

        public void Add(Baggage item)
        {
            if (FirstFreeCell() != -1 && item.Weight <= CellCapacity)
                _dictionaryBaggages.Add(FirstFreeCell(), item);
            else throw new InvalidOperationException("Невозможно добавить багаж! Нет свободного места!");
        }

        public bool Contains(Baggage item)
        {
            return _dictionaryBaggages.ContainsValue(item);
        }

        public void CopyTo(Baggage[] array, int arrayIndex)
        {
            _dictionaryBaggages.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return (int)CellsCount; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Baggage item)
        {
            return _dictionaryBaggages.Remove(GetCellNumber(item.Number));
        }

        public void Clear()
        {
            _dictionaryBaggages.Clear();
        }

        public IEnumerator<Baggage> GetEnumerator()
        {
            return _dictionaryBaggages.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return String.Format("Багажное отделение {0}, Кол-во ячеек: {1}, Вместимость ячейки: {2}", base.ToString(),
                CellsCount, CellCapacity);
        }

        private int FirstFreeCell()
        {
            for (int i = 1; i <= CellsCount; i++)
                if (!_dictionaryBaggages.ContainsKey(i))
                    return i;
            return -1;
        }

        public IEnumerable<int> GetBusyCells()
        {
            return _dictionaryBaggages.Keys.AsEnumerable();
        }

        public int BusyCellsCount
        {
            get { return _dictionaryBaggages.Keys.Count; }
        }

        public IEnumerable<int> GetFreeCells()
        {
            for (int i = 1; i <= CellsCount; i++)
                if (!_dictionaryBaggages.ContainsKey(i))
                    yield return i;
        }

        public long FreeCellsCount
        {
            get { return CellsCount - BusyCellsCount; }
        }

        public Baggage GetBaggage(int cellNumber)
        {
            if (_dictionaryBaggages.ContainsKey(cellNumber))
                return _dictionaryBaggages[cellNumber];
            throw new ArgumentException("Ячейка с таким номером отсутсвует!");
        }

        public int GetCellNumber(int baggageNumber)
        {
            try
            {
                return _dictionaryBaggages.First(baggage => baggage.Value.Number == baggageNumber).Key;
            }
            catch (Exception)
            {
                throw new ArgumentException("Данный багаж отсутсвует!");
            }
        }

        public double Weight
        {
            get { return _dictionaryBaggages.Sum(item => item.Value.Weight); }
        }

        public long Capacity
        {
            get { return CellCapacity * CellsCount; }
        }

        public override string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.ToString());
            for (int i = 1; i <= CellsCount; i++)
            {
                if (_dictionaryBaggages.ContainsKey(i))
                    result.AppendLine("   - " + i + " ячейка - " + _dictionaryBaggages[i]);
                else result.AppendLine("   - " + i + " ячека пуста");
            }
            return result.ToString();
        }
    }
}
