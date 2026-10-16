using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isProgramContinues = true;
            List<Vacancy> vacancies = new List<Vacancy>();
            List<Applicant> applicants = new List<Applicant>();

            while (isProgramContinues)
            {
                Console.WriteLine();
                Console.WriteLine("Click any button to continue");
                Console.ReadKey(true);

                PrintInfo();

                bool IsParseSuccess = int.TryParse(Console.ReadLine(), out int userInput);

                if (IsParseSuccess)
                {
                    if (userInput == 1)
                    {
                        Console.WriteLine("Please enter name of applicant");
                        string nameOfWoker = Console.ReadLine();
                        Applicant sameNamedApplicant = GetApplicantByName(nameOfWoker, applicants);

                        if (sameNamedApplicant == null)
                        {
                            if (!string.IsNullOrEmpty(nameOfWoker))
                            {
                                Applicant applicant = new Applicant(nameOfWoker);
                                applicants.Add(applicant);
                                Console.WriteLine("Success");
                            }
                            else
                            {
                                Console.WriteLine("Empty input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This name is already taken");
                        }
                    }
                    else if (userInput == 2)
                    {
                        if (applicants.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                Console.WriteLine($"Name is: \"{findedApplicant.Name}\"");

                                if (findedApplicant.Experience != null)
                                {
                                    Console.WriteLine($"Experience: {findedApplicant.Experience}");
                                }
                                else
                                {
                                    Console.WriteLine($"Experience: none");

                                }

                                Console.Write("Abilities: ");

                                if (findedApplicant.Abilities.Count > 0)
                                {
                                    for (int i = 0; i < findedApplicant.Abilities.Count; i++)
                                    {
                                        if (i > 0 && i < findedApplicant.Abilities.Count)
                                        {
                                            Console.Write(", ");
                                        }
                                        Console.Write($"{findedApplicant.Abilities[i]}");
                                    }

                                }
                                else
                                {
                                    Console.Write("none");
                                }

                                Console.WriteLine();
                                Console.Write("Wanted pay: ");

                                if (findedApplicant.WantedPayments > 0)
                                {
                                    Console.WriteLine($"{findedApplicant.WantedPayments}");
                                }
                                else if (findedApplicant.WantedPayments == 0)
                                {
                                    Console.WriteLine("none");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\"{nameOfApplicant}\" is not exists");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 3)
                    {
                        if (applicants.Count > 0)
                        {
                            Console.WriteLine("Please enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null && findedApplicant.Experience == null && findedApplicant.Abilities.Count == 0)
                            {
                                Console.WriteLine("Input some decription and experience of applicant");
                                string experienceOfApplicant = Console.ReadLine();

                                if (!string.IsNullOrEmpty(experienceOfApplicant))
                                {
                                    List<string> abilities = new List<string>();

                                    Console.WriteLine("Enter abilities of applicant. Enter \"0\" to stop");
                                    bool isUserWantsToContinue = true;

                                    while (isUserWantsToContinue)
                                    {
                                        string ability = Console.ReadLine();

                                        if (ability != "0" && !string.IsNullOrEmpty(ability))
                                        {
                                            abilities.Add(ability);
                                            Console.WriteLine("Success. Enter \"0\" to stop or continue adding new abilities");
                                        }
                                        else if (ability == "0")
                                        {
                                            isUserWantsToContinue = false;
                                        }
                                        else if (string.IsNullOrEmpty(ability))
                                        {
                                            Console.WriteLine("Empty input (this ability cant be added)");
                                        }
                                    }

                                    findedApplicant.Abilities = abilities;
                                    findedApplicant.Experience = experienceOfApplicant;
                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("Empty input");
                                }
                            }
                            else if (findedApplicant != null)
                            {
                                if (findedApplicant.Experience != null || findedApplicant.Abilities.Count == 0)
                                {
                                    Console.WriteLine($"\"{findedApplicant.Name}\" already has experience or abilities (or both) but you can change them with \"4\" ");
                                }
                            }
                            else if (findedApplicant == null)
                            {
                                Console.WriteLine("This applicant is not exists yet");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 4)
                    {
                        if (applicants.Count > 0)
                        {
                            Console.WriteLine("Please enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                Console.WriteLine("What you want to change? 1 - Name, 2 - expirience, 3 - abilities");
                                bool isParseSuccess = int.TryParse(Console.ReadLine(), out int userChoiceToChange);

                                if (isParseSuccess)
                                {
                                    if (userChoiceToChange == 1)
                                    {
                                        Console.WriteLine($"Input new name. Current name is \"{nameOfApplicant}\"");
                                        string newName = Console.ReadLine();
                                        string savedName = findedApplicant.Name;
                                        findedApplicant.Name = null;
                                        Applicant findedApplicantWithSameName = GetApplicantByName(newName, applicants);

                                        if (findedApplicantWithSameName == null)
                                        {
                                            findedApplicant.Name = newName;
                                            Console.WriteLine("Success");
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is applicant with same name already");
                                            findedApplicant.Name = savedName;
                                        }
                                    }
                                    else if (userChoiceToChange == 2)
                                    {
                                        Console.WriteLine("Please enter expirience of applicant");
                                        string expirience = Console.ReadLine();

                                        if (!string.IsNullOrEmpty(expirience))
                                        {
                                            findedApplicant.Experience = expirience;
                                            Console.WriteLine("Success");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Empty input");
                                        }
                                    }
                                    else if (userChoiceToChange == 3)
                                    {
                                        bool isTaskNotCompleted = true;

                                        while (isTaskNotCompleted)
                                        {
                                            Console.WriteLine("Enter 0 to stop, enter 1 to remove ability, enter 2 to add ability");
                                            bool isNumberParseSuccess = int.TryParse(Console.ReadLine(), out int userChoice);

                                            if (isNumberParseSuccess)
                                            {
                                                if (userChoice == 0)
                                                {
                                                    isTaskNotCompleted = false;
                                                }
                                                else if (userChoice == 1)
                                                {
                                                    for (int i = 0; i < findedApplicant.Abilities.Count; i++)
                                                    {
                                                        Console.WriteLine($"{i + 1}) {findedApplicant.Abilities[i]}");
                                                    }

                                                    Console.WriteLine("Enter number of ability to remove");
                                                    bool isNumberOfAbilityParseSuccess = int.TryParse(Console.ReadLine(), out int numberOfAbility);
                                                    int indexOfAbility = numberOfAbility - 1;

                                                    if (isNumberOfAbilityParseSuccess && findedApplicant.Abilities.Count > 0)
                                                    {
                                                        if (indexOfAbility >= 0 && indexOfAbility < findedApplicant.Abilities.Count)
                                                        {
                                                            findedApplicant.Abilities.Remove(findedApplicant.Abilities[indexOfAbility]);
                                                            Console.WriteLine("Success");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Wrong number");
                                                        }
                                                    }
                                                    else if (!isNumberOfAbilityParseSuccess)
                                                    {
                                                        Console.WriteLine("Wrong input");
                                                    }
                                                    else if (findedApplicant.Abilities.Count == 0)
                                                    {
                                                        Console.WriteLine("List of abilities is empty");
                                                    }
                                                }
                                                else if (userChoice == 2)
                                                {
                                                    Console.WriteLine("Enter new ability");
                                                    string newAbility = Console.ReadLine();

                                                    if (!string.IsNullOrEmpty(newAbility))
                                                    {
                                                        findedApplicant.Abilities.Add(newAbility);
                                                        Console.WriteLine("Success");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Wrong input");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Wrong input");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Wrong input");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input");
                                }
                            }
                            else if (findedApplicant == null)
                            {
                                Console.WriteLine($"\"{nameOfApplicant}\" is not exists");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 5)
                    {
                        if (applicants.Count > 0)
                        {
                            Console.WriteLine("Please input name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                Console.WriteLine("Enter wanted pay for applicant");
                                bool IsPayParseSuccess = float.TryParse(Console.ReadLine(), out float wantedPay);

                                if (IsPayParseSuccess && wantedPay > 0 && wantedPay < 10000000)
                                {
                                    findedApplicant.WantedPayments = wantedPay;
                                    Console.WriteLine("Success");
                                }
                                else if (!IsPayParseSuccess)
                                {
                                    Console.WriteLine("Not number");
                                }
                                else if (wantedPay <= 0 || wantedPay >= 10000000)
                                {
                                    Console.WriteLine("Wanted pay cant be less than 0 or more than 10000000");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 6)
                    {
                        if (applicants.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                applicants.Remove(findedApplicant);

                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.JobApplications.Contains(findedApplicant))
                                    {
                                        vacancy.JobApplications.Remove(findedApplicant);
                                    }
                                }

                                Console.WriteLine("Success");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 7)
                    {
                        Console.WriteLine("Enter name of role");
                        string nameOfRole = Console.ReadLine();

                        if (nameOfRole != null && !string.IsNullOrWhiteSpace(nameOfRole))
                        {
                            Console.WriteLine("Enter name of company");
                            string nameOfCompany = Console.ReadLine();

                            if (nameOfCompany != null && !string.IsNullOrWhiteSpace(nameOfCompany))
                            {
                                Console.WriteLine("Enter amount of payment");
                                bool isPaymentParseSuccess = float.TryParse(Console.ReadLine(), out float payment);

                                if (isPaymentParseSuccess && payment > 0 && payment < 10000000)
                                {
                                    Console.WriteLine("Enter requirements");
                                    string requirements = Console.ReadLine();

                                    if (requirements != null && !string.IsNullOrWhiteSpace(requirements))
                                    {
                                        Vacancy vacancy = new Vacancy(nameOfRole, nameOfCompany, payment, requirements);
                                        vacancies.Add(vacancy);
                                        int indexOfPreviousVacancy = vacancies.IndexOf(vacancy) - 1;

                                        if (vacancies.Count <= 1)
                                        {
                                            vacancy.Id = 0;
                                        }
                                        else
                                        {
                                            vacancy.Id = vacancies[indexOfPreviousVacancy].Id + 1;
                                        }

                                        Console.WriteLine("Success");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty input");
                                    }
                                }
                                else if (!isPaymentParseSuccess)
                                {
                                    Console.WriteLine("Wrong input");
                                }
                                else if (payment <= 0 || payment >= 10000000)
                                {
                                    Console.WriteLine("Payment cant be less than 0 or more than 10000000");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }
                    }
                    else if (userInput == 8)
                    {
                        if (vacancies.Count > 0)
                        {
                            foreach (Vacancy vacancy in vacancies)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Name of role: {vacancy.NameOfRole}");
                                Console.WriteLine($"Name of company {vacancy.Company}");
                                Console.WriteLine($"Payment: {vacancy.Payments}");
                                Console.WriteLine($"Requirements: {vacancy.Requirements}");
                                Console.WriteLine($"Id: {vacancy.Id}");
                                Console.WriteLine($"Is avaivable: {vacancy.IsVacancyAvaivable}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 9)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    Console.WriteLine("Enter 1 to change name of role, 2 to change company, 3 to change payments, 4 to change requirements");
                                    bool isParseSuccess = int.TryParse(Console.ReadLine(), out int userchoice);

                                    if (isParseSuccess)
                                    {
                                        if (userchoice == 1)
                                        {
                                            Console.WriteLine("Please enter new name of role");
                                            string newNameOfRole = Console.ReadLine();

                                            if (!string.IsNullOrEmpty(newNameOfRole))
                                            {
                                                findedVacancy.NameOfRole = newNameOfRole;
                                                Console.WriteLine("Success");
                                            }
                                        }
                                        else if (userchoice == 2)
                                        {
                                            Console.WriteLine("Please enter new name of company");
                                            string newNameOfCompany = Console.ReadLine();

                                            if (!string.IsNullOrEmpty(newNameOfCompany))
                                            {
                                                findedVacancy.Company = newNameOfCompany;
                                                Console.WriteLine("Success");
                                            }
                                        }
                                        else if (userchoice == 3)
                                        {
                                            Console.WriteLine("Please enter new payment");
                                            bool isPaymentParseSuccess = float.TryParse(Console.ReadLine(), out float newPayment);

                                            if (isPaymentParseSuccess && newPayment > 0)
                                            {
                                                findedVacancy.Payments = newPayment;
                                                Console.WriteLine("Success");
                                            }
                                            else if (!isPaymentParseSuccess)
                                            {
                                                Console.WriteLine("Input is not number");
                                            }
                                            else if (newPayment <= 0)
                                            {
                                                Console.WriteLine("Payment cant be less than 0");
                                            }
                                        }
                                        else if (userchoice == 4)
                                        {
                                            Console.WriteLine("Please enter new requirements");
                                            string newRequirements = Console.ReadLine();

                                            if (!string.IsNullOrEmpty(newRequirements))
                                            {
                                                findedVacancy.Requirements = newRequirements;
                                                Console.WriteLine("Success");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong input");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Input is not number");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("There is no vacancies with this id");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 10)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null && findedVacancy.IsVacancyAvaivable == true)
                                {
                                    findedVacancy.IsVacancyAvaivable = false;
                                    Console.WriteLine("Success");
                                }
                                else if (findedVacancy == null)
                                {
                                    Console.WriteLine("Couldnt find vacancy");
                                }
                                else if (findedVacancy.IsVacancyAvaivable == false)
                                {
                                    findedVacancy.IsVacancyAvaivable = true;
                                    Console.WriteLine("Vacansy was closed so it was opened instead");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no vacancies yet");
                        }
                    }
                    else if (userInput == 11)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    vacancies.Remove(findedVacancy);

                                    foreach (Applicant applicant in applicants)
                                    {
                                        if (applicant.SavedVacancys.Contains(findedVacancy))
                                        {
                                            applicant.SavedVacancys.Remove(findedVacancy);
                                        }

                                        if (applicant.JobApplicationsOfWoker.Contains(findedVacancy))
                                        {
                                            applicant.JobApplicationsOfWoker.Remove(findedVacancy);
                                        }
                                    }

                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("Couldnt find vacancy");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not number");
                            }
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 12)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of role");
                            string nameOfRole = Console.ReadLine();
                            List<Vacancy> findedVacancies = new List<Vacancy>();

                            if (!string.IsNullOrEmpty(nameOfRole))
                            {
                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.NameOfRole.Contains(nameOfRole, StringComparison.OrdinalIgnoreCase) && vacancy.IsVacancyAvaivable == true)
                                    {
                                        findedVacancies.Add(vacancy);
                                    }
                                }

                                if (findedVacancies.Count > 0)
                                {
                                    for (int i = 0; i < findedVacancies.Count; i++)
                                    {
                                        Console.WriteLine($"Name of role:{i + 1}) {findedVacancies[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedVacancies[i].Company}");
                                        Console.WriteLine($"Payment: {findedVacancies[i].Payments}");
                                        Console.WriteLine($"Id: {findedVacancies[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("List is empty");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empty input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 13)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter lowest payment");
                            bool isPaymentParseSuccess = float.TryParse(Console.ReadLine(), out float lowestPayment);
                            List<Vacancy> findedVacancies = new List<Vacancy>();

                            if (isPaymentParseSuccess)
                            {
                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.Payments >= lowestPayment && vacancy.IsVacancyAvaivable == true)
                                    {
                                        findedVacancies.Add(vacancy);
                                    }
                                }

                                if (findedVacancies.Count > 0)
                                {
                                    for (int i = 0; i < findedVacancies.Count; i++)
                                    {
                                        Console.WriteLine($"Name of role: {findedVacancies[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedVacancies[i].Company}");
                                        Console.WriteLine($"Payment: {findedVacancies[i].Payments}");
                                        Console.WriteLine($"Id: {findedVacancies[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("List is empty");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not a number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 14)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of company");
                            string nameOfCompany = Console.ReadLine();
                            List<Vacancy> findedVacancies = new List<Vacancy>();

                            if (!string.IsNullOrEmpty(nameOfCompany))
                            {
                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.Company.Contains(nameOfCompany, StringComparison.OrdinalIgnoreCase) && vacancy.IsVacancyAvaivable == true)
                                    {
                                        findedVacancies.Add(vacancy);
                                    }
                                }

                                if (findedVacancies.Count > 0)
                                {
                                    for (int i = 0; i < findedVacancies.Count; i++)
                                    {
                                        Console.WriteLine($"Name of role:{i + 1}) {findedVacancies[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedVacancies[i].Company}");
                                        Console.WriteLine($"Payment: {findedVacancies[i].Payments}");
                                        Console.WriteLine($"Id: {findedVacancies[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("List is empty");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empty input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 15)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter requirements");
                            string requirements = Console.ReadLine();
                            List<Vacancy> findedVacancies = new List<Vacancy>();

                            if (!string.IsNullOrEmpty(requirements))
                            {
                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.Requirements.Contains(requirements, StringComparison.OrdinalIgnoreCase) && vacancy.IsVacancyAvaivable == true)
                                    {
                                        findedVacancies.Add(vacancy);
                                    }
                                }

                                if (findedVacancies.Count > 0)
                                {
                                    for (int i = 0; i < findedVacancies.Count; i++)
                                    {
                                        Console.WriteLine($"Name of role:{i + 1}) {findedVacancies[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedVacancies[i].Company}");
                                        Console.WriteLine($"Payment: {findedVacancies[i].Payments}");
                                        Console.WriteLine($"Id: {findedVacancies[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("List is empty");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empty input");
                            }
                        }
                    }
                    else if (userInput == 16)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Input id of vacancy");
                            bool isParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    Console.WriteLine("Input name of applicant");
                                    string nameOfApplicant = Console.ReadLine();

                                    if (!string.IsNullOrEmpty(nameOfApplicant))
                                    {
                                        Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                                        if (findedApplicant != null && !findedVacancy.OperatedApplications.Contains(findedApplicant) && !findedVacancy.JobApplications.Contains(findedApplicant) && findedVacancy.IsVacancyAvaivable == true)
                                        {
                                            findedVacancy.JobApplications.Add(findedApplicant);
                                            findedApplicant.JobApplicationsOfWoker.Add(findedVacancy);
                                            Console.WriteLine("Success");
                                        }
                                        else if (findedApplicant == null)
                                        {
                                            Console.WriteLine("This applicant is not exists");
                                        }
                                        else if (findedVacancy.OperatedApplications.Contains(findedApplicant) || findedVacancy.JobApplications.Contains(findedApplicant))
                                        {
                                            Console.WriteLine("This applicant cant make a answer to same vacancy more than 1 time");
                                        }
                                        else if (findedVacancy.IsVacancyAvaivable == false)
                                        {
                                            Console.WriteLine("This vacancy is closed");
                                        }
                                    }
                                }
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 17)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();

                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                if (findedApplicant.JobApplicationsOfWoker.Count > 0)
                                {
                                    for (int i = 0; i < findedApplicant.JobApplicationsOfWoker.Count; i++)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Name of role: {findedApplicant.JobApplicationsOfWoker[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedApplicant.JobApplicationsOfWoker[i].Company}");
                                        Console.WriteLine($"Payment: {findedApplicant.JobApplicationsOfWoker[i].Payments}");
                                        Console.WriteLine($"Requirements: {findedApplicant.JobApplicationsOfWoker[i].Requirements}");
                                        Console.WriteLine($"Id: {findedApplicant.JobApplicationsOfWoker[i].Id}");
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 18)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    if (findedVacancy.JobApplications.Count > 0)
                                    {
                                        for (int i = 0; i < findedVacancy.JobApplications.Count; i++)
                                        {
                                            Console.WriteLine($"{i + 1}) {findedVacancy.JobApplications[i].Name}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is no applications yet");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("This vacancy is not exists yet");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not a number");
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 19)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    Console.WriteLine("Enter name of applicant");
                                    string nameOfApplicant = Console.ReadLine();
                                    Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                                    if (findedApplicant != null && findedVacancy.JobApplications.Contains(findedApplicant))
                                    {
                                        Console.WriteLine("Enter 1 to accept, 2 to reject");
                                        bool isChoiceParsed = int.TryParse(Console.ReadLine(), out int choice);

                                        if (isChoiceParsed)
                                        {
                                            if (choice == 1 && findedApplicant.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                findedVacancy.OperatedApplications.Add(findedApplicant);
                                                int indexOfVacancy = findedApplicant.JobApplicationsOfWoker.IndexOf(findedVacancy);
                                                findedVacancy.AcceptedApplications.Add(findedApplicant);
                                                findedApplicant.JobApplicationsOfWoker.Remove(findedVacancy);
                                                findedVacancy.JobApplications.Remove(findedApplicant);
                                                Console.WriteLine("Success");

                                            }
                                            else if (choice == 2 && findedApplicant.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                findedVacancy.OperatedApplications.Add(findedApplicant);
                                                int indexOfVacancy = findedApplicant.JobApplicationsOfWoker.IndexOf(findedVacancy);
                                                findedApplicant.JobApplicationsOfWoker.Remove(findedVacancy);
                                                findedVacancy.CanceledApplications.Add(findedApplicant);
                                                findedVacancy.JobApplications.Remove(findedApplicant);
                                                Console.WriteLine("Success");
                                            }
                                            else if (!findedApplicant.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                Console.WriteLine("This applicant didnt accept this application");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Wrong input");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input is not a number");
                                        }
                                    }
                                    else if (findedApplicant == null)
                                    {
                                        Console.WriteLine("This applicant is not exists");
                                    }
                                    else if (!findedVacancy.JobApplications.Contains(findedApplicant))
                                    {
                                        Console.WriteLine("This applicant didnt send an answer on this application");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("This vacancy is not exists yet");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not a number");
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 20)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                Console.WriteLine("Enter id of vacancy");
                                bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int id);

                                if (isIdParseSuccess)
                                {
                                    Vacancy findedVacancy = GetVacancyById(id, vacancies);

                                    if (findedVacancy != null && !findedApplicant.SavedVacancys.Contains(findedVacancy) && findedVacancy.IsVacancyAvaivable == true)
                                    {
                                        findedApplicant.SavedVacancys.Add(findedVacancy);
                                        Console.WriteLine("Success");
                                    }
                                    else if (findedVacancy == null)
                                    {
                                        Console.WriteLine("Vacancy is not exists");
                                    }
                                    else if (findedApplicant.SavedVacancys.Contains(findedVacancy))
                                    {
                                        Console.WriteLine("Cant add same vacancy to favourite list for 1 applicant");
                                    }
                                    else if (findedVacancy.IsVacancyAvaivable == false)
                                    {
                                        Console.WriteLine("Vacancy is not avaivable anymore");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Input is not a number");
                                }
                            }
                            else
                            {
                                Console.WriteLine("This applicant is not exists");
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 21)
                    {
                        if (applicants.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                if (findedApplicant.SavedVacancys.Count > 0)
                                {
                                    for (int i = 0; i < findedApplicant.SavedVacancys.Count; i++)
                                    {
                                        Console.WriteLine($"{findedApplicant.SavedVacancys[i].NameOfRole}");
                                        Console.WriteLine($"{findedApplicant.SavedVacancys[i].IsVacancyAvaivable}");
                                        Console.WriteLine($"{findedApplicant.SavedVacancys[i].Company}");
                                        Console.WriteLine($"{findedApplicant.SavedVacancys[i].Payments}");
                                        Console.WriteLine($"{findedApplicant.SavedVacancys[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("This applicant doesnt has any saved vacancies");
                                }
                            }
                            else
                            {
                                Console.WriteLine("This applicant is not exists");
                            }
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 22)
                    {
                        if (vacancies.Count > 0)
                        {
                            int totalAmountOffreeVacancies = 0;

                            foreach (Vacancy vacancy in vacancies)
                            {
                                if (vacancy.IsVacancyAvaivable)
                                {
                                    Console.WriteLine($"Id of vacancy free: {vacancy.Id}");
                                    totalAmountOffreeVacancies++;
                                }
                            }

                            if (totalAmountOffreeVacancies > 0)
                            {
                                Console.WriteLine($"Total amount of free vacancies: {totalAmountOffreeVacancies}");
                            }
                            else
                            {
                                Console.WriteLine("There is no free vacancyes");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 23)
                    {
                        if (vacancies.Count > 0 && applicants.Count > 0)
                        {
                            Console.WriteLine("Enter name of applicant");
                            string nameOfApplicant = Console.ReadLine();
                            Applicant findedApplicant = GetApplicantByName(nameOfApplicant, applicants);

                            if (findedApplicant != null)
                            {
                                if (findedApplicant.JobApplicationsOfWoker.Count > 0)
                                {
                                    Console.WriteLine($"Number of job applications of applicant: {findedApplicant.JobApplicationsOfWoker.Count}");
                                }
                                else
                                {
                                    Console.WriteLine("This applicant is not made any job applications");
                                }
                            }
                            else
                            {
                                Console.WriteLine("This applicant is not exists yet");
                            }
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                        else if (applicants.Count == 0)
                        {
                            Console.WriteLine("There is no applicants yet");
                        }
                    }
                    else if (userInput == 24)
                    {
                        if (vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter prefered payment");
                            bool isPaymentParseSuccess = float.TryParse(Console.ReadLine(), out float preferedPayment);
                            bool isThereAnyFindedVacancies = false;

                            if (isPaymentParseSuccess)
                            {
                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.Payments >= preferedPayment && vacancy.IsVacancyAvaivable == true)
                                    {
                                        Console.WriteLine($"Payment: {vacancy.Payments}");
                                        Console.WriteLine($"Id: {vacancy.Id}");
                                        isThereAnyFindedVacancies = true;
                                    }
                                }

                                if (isThereAnyFindedVacancies != true)
                                {
                                    Console.WriteLine("There is no finded vacancies");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not a number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 25)
                    {
                        int freeVacancies = 0;
                        int totalJobApplications = 0;
                        int totalCanceledApplications = 0;
                        int totalAcceptedApplications = 0;


                        if (applicants.Count > 0)
                        {
                            foreach (Applicant applicant in applicants)
                            {
                                Console.WriteLine($"Name of applicant: {applicant.Name}");
                            }
                            Console.WriteLine($"Total amount is: {applicants.Count}");
                        }
                        else
                        {
                            Console.WriteLine("There is no applicants");
                        }

                        if (vacancies.Count > 0)
                        {
                            foreach (Vacancy vacancy in vacancies)
                            {
                                Console.WriteLine($"Id of vacancy: {vacancy.Id}");
                            }

                            Console.WriteLine($"Total amount is: {vacancies.Count}");

                            foreach (Vacancy vacancy in vacancies)
                            {
                                if (vacancy.IsVacancyAvaivable == true)
                                {
                                    Console.WriteLine($"Id of free vacancy: {vacancy.Id}");
                                    freeVacancies++;
                                }

                                Console.WriteLine($"Total amount of free vacancies is: {freeVacancies}");
                            }

                            foreach (Vacancy vacancy in vacancies)
                            {
                                if (vacancy.JobApplications.Count > 0)
                                {
                                    totalJobApplications += vacancy.JobApplications.Count;
                                }
                            }

                            Console.WriteLine($"Total amount of job applications: {totalJobApplications}");

                            foreach (Vacancy vacancy in vacancies)
                            {
                                if (vacancy.CanceledApplications.Count > 0)
                                {
                                    totalCanceledApplications += vacancy.CanceledApplications.Count;
                                }
                            }

                            Console.WriteLine($"Total amount of canceled applications: {totalCanceledApplications}");

                            foreach (Vacancy vacancy in vacancies)
                            {
                                if (vacancy.AcceptedApplications.Count > 0)
                                {
                                    totalAcceptedApplications += vacancy.AcceptedApplications.Count;
                                }
                            }

                            Console.WriteLine($"Total amount of accepted applications: {totalAcceptedApplications}");
                        }
                        else
                        {
                            Console.WriteLine("There is no vacancies");
                        }
                    }
                    else if (userInput == 0)
                    {
                        Console.WriteLine("Goodbye");
                        isProgramContinues = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
            }
        }

        static void PrintInfo()
        {
            Console.WriteLine("Enter 1 to add new applicant");
            Console.WriteLine("Enter 2 to see info about applicant");
            Console.WriteLine("Enter 3 to add decription and abilities");
            Console.WriteLine("Enter 4 to change info about applicant");
            Console.WriteLine("Enter 5 to change wanted pay");
            Console.WriteLine("Enter 6 to remove applicant");
            Console.WriteLine();
            Console.WriteLine("Enter 7 to make a new vacancy");
            Console.WriteLine("Enter 8 to see all vacancies");
            Console.WriteLine("Enter 9 to change info about vacancy");
            Console.WriteLine("Enter 10 to cancel vacancy");
            Console.WriteLine("Enter 11 to delete vacancy");
            Console.WriteLine();
            Console.WriteLine("Enter 12 to find vacancy by name of role");
            Console.WriteLine("Enter 13 to find vacancy by same or higher payment");
            Console.WriteLine("Enter 14 to find vacancy by name of company");
            Console.WriteLine("Enter 15 to find vacancy by abilties");
            Console.WriteLine();
            Console.WriteLine("Enter 16 to send an answer to vacancy");
            Console.WriteLine("Enter 17 to see all answers of applicant");
            Console.WriteLine("Enter 18 to see answers of potential applicants");
            Console.WriteLine("Enter 19 to change status of applicant's answer");
            Console.WriteLine();
            Console.WriteLine("Enter 20 to add vacancy to favourite list");
            Console.WriteLine("Enter 21 to see saved vacancies favourite list");
            Console.WriteLine("Enter 22 to see amount of all active vacancies");
            Console.WriteLine("Enter 23 to see amount of applications of applicant");
            Console.WriteLine("Enter 24 to see vacancies with same or higher payment");
            Console.WriteLine("Enter 25 to see all info about program");
            Console.WriteLine("Enter 0 to stop");
        }

        static Applicant GetApplicantByName(string nameOfApplicant, List<Applicant> applicants)
        {
            foreach (Applicant applicant in applicants)
            {
                if (applicant.Name == nameOfApplicant)
                {
                    return applicant;
                }
            }
            return null;
        }

        static Vacancy GetVacancyById(int id, List<Vacancy> vacancies)
        {
            foreach (Vacancy vacancy in vacancies)
            {
                if (vacancy.Id == id)
                {
                    return vacancy;
                }
            }
            return null;
        }
    }
}