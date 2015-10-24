namespace ConcordanceProject.Model
{
    public class Page : Collection<Sentence>
    {
        public int Number { get; set; }

        public Page(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return "Page №" + Number;
        }
    }
}
