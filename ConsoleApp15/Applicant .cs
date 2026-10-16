namespace ConsoleApp15
{
    internal class Applicant
    {
        public Applicant(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Experience { get; set; }

        public List<string> Abilities { get; set; } = new List<string>();

        public List<Vacancy> SavedVacancys { get; set; } = new List<Vacancy>();

        public List<Vacancy> JobApplicationsOfWoker { get; set; } = new List<Vacancy>();

        public float WantedPayments {  get; set; } = 0;
    }
}
