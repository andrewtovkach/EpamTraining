namespace ConcordanceProject.Model.IO
{
    public class Separators: Collection<char>
    {
        public Separators(params char [] collection) 
        {
            foreach (var item in collection)
                List.Add(item);
        }
    }
}
