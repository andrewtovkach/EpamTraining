using Transport.Model.Carriages;

namespace Transport.Interfaces
{
    interface ISortable
    {
        Carriage this[int number] { get; }
        void Sort();
        void SortByUserCondition();
    }
}
