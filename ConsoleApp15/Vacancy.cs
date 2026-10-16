namespace ConsoleApp15
{
    internal class Vacancy
    {
        public Vacancy(string nameOfRole, string company, float payments, string requirements)
        {
            NameOfRole = nameOfRole;
            Company = company;
            Payments = payments;
            Requirements = requirements;
        }

        public string NameOfRole { get; set; }

        public bool IsVacancyAvaivable { get; set; } = true;

        public List<Applicant> JobApplications { get; set; } = new List<Applicant>();

        public List<Applicant> OperatedApplications { get; set; } = new List<Applicant>();

        public string Company { get; set; }

        public float Payments { get; set; }

        public string Requirements { get; set; }

        public int Id { get; set; }

        public List<Applicant> AcceptedApplications { get; set; } = new List<Applicant>();

        public List<Applicant> CanceledApplications { get; set; } = new List<Applicant>();

    }
}
