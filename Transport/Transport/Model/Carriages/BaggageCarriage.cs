using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    class BaggageCarriage : Carriage, IPrintable, ICollection<Baggage>
    {
        public int CellsCount { get; set; }
        public int CellCapacity { get; set; }

        private Dictionary<int, Baggage> dictionaryBaggages;

        public BaggageCarriage()
        {
            this.dictionaryBaggages = new Dictionary<int, Baggage>();
        }

        public BaggageCarriage(int number, DateTime startUpDate, uint axisNumber, int cellsCount, int cellCapacity)
            : base(number, startUpDate, axisNumber)
        {
            this.dictionaryBaggages = new Dictionary<int, Baggage>();
            this.CellCapacity = cellCapacity;
            this.CellsCount = cellsCount;
        }

        public Baggage this[int number]
        {
            get { return dictionaryBaggages.FirstOrDefault(x => x.Value.Number == number).Value; }
        }

        public Baggage this[string name]
        {
            get { return dictionaryBaggages.FirstOrDefault(x => x.Value.Name == name).Value; }
        }

        public int FirstFreeCell
        {
            get
            {
                for (int i = 1; i <= CellsCount; i++)
                    if (!dictionaryBaggages.ContainsKey(i))
                        return i;
                return -1;
            }
        }

        public void Add(Baggage item)
        {
            if (FirstFreeCell != -1 && item.Weight <= CellCapacity)
                dictionaryBaggages.Add(FirstFreeCell, item);
            else throw new InvalidOperationException("Невозможно добавить багаж! Нет свободного места!");
        }

        public bool Contains(Baggage item)
        {
            return dictionaryBaggages.ContainsValue(item);
        }

        public void CopyTo(Baggage[] array, int arrayIndex)
        {
            dictionaryBaggages.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return CellsCount; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Baggage item)
        {
             return dictionaryBaggages.Remove(GetCellNumber(item.Number));
        }

        public IEnumerator<Baggage> GetEnumerator()
        {
            return dictionaryBaggages.Values.GetEnumerator();
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

        public IEnumerable<int> GetBusyCells()
        {
            return dictionaryBaggages.Keys.AsEnumerable();
        }

        public int BusyCellsCount
        {
            get { return dictionaryBaggages.Keys.Count; }
        }

        public IEnumerable<int> GetFreeCells()
        {
            for (int i = 1; i <= CellsCount; i++)
                if (!dictionaryBaggages.ContainsKey(i))
                    yield return i;
        }

        public int FreeCellsCount
        {
            get { return CellsCount - BusyCellsCount; }
        }

        public void Clear()
        {
            dictionaryBaggages.Clear();
        }

        public Baggage GetBaggage(int cellNumber)
        {
            if (dictionaryBaggages.ContainsKey(cellNumber))
                return dictionaryBaggages[cellNumber];
            throw new ArgumentException("Ячейка с таким номером отсутсвует!");
        }

        public int GetCellNumber(int baggageNumber)
        {
            try
            {
                return dictionaryBaggages.First(baggage => baggage.Value.Number == baggageNumber).Key;
            }
            catch (Exception)
            {
                throw new ArgumentException("Данный багаж отсутсвует!");
            }
        }

        public double TotalWeight
        {
            get { return dictionaryBaggages.Sum(item => item.Value.Weight); }
        }

        public int TotalCapacity
        {
            get { return CellCapacity * CellsCount; }
        }

        public string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.ToString());
            for (int i = 1; i <= CellsCount; i++)
            {
                if (dictionaryBaggages.ContainsKey(i))
                    result.AppendLine("   - " + i + " ячейка - " + dictionaryBaggages[i]);
                else result.AppendLine("   - " + i + " ячека пуста");
            }
            return result.ToString();
        }
    }
}
