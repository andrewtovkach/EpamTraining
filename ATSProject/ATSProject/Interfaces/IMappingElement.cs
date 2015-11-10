namespace ATSProject.Interfaces
{
    public interface IMappingElement<in T>
    {
        void AddMapping(T element);
        bool RemoveMapping(string elementNumber);
    }
}
