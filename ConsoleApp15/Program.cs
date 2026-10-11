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
            List<Worker> workers = new List<Worker>();

            while (isProgramContinues)
            {
                AutoRemoverVacanciesIfTheyAreClosed(vacancies, workers);
                Console.WriteLine();
                Console.WriteLine("Click any button to continue");
                Console.ReadKey(true);

                PrintInfo();

                bool IsParseSuccess = int.TryParse(Console.ReadLine(), out int userInput);

                if (IsParseSuccess)
                {
                    if (userInput == 1)
                    {
                        Console.WriteLine("Please enter name of worker");
                        string nameOfWoker = Console.ReadLine();
                        Worker sameNamedWorker = GetWorkerByName(nameOfWoker, workers);

                        if (sameNamedWorker == null)
                        {
                            if (!string.IsNullOrEmpty(nameOfWoker))
                            {
                                Worker worker = new Worker(nameOfWoker);
                                workers.Add(worker);
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
                        if (workers.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                Console.WriteLine($"Name is: \"{findedWorker.Name}\"");

                                if (findedWorker.Experience != null)
                                {
                                    Console.WriteLine($"Experience: {findedWorker.Experience}");
                                }
                                else
                                {
                                    Console.WriteLine($"Experience: none");

                                }

                                Console.Write("Abilities: ");

                                if (findedWorker.Abilities.Count > 0)
                                {
                                    for (int i = 0; i < findedWorker.Abilities.Count; i++)
                                    {
                                        if (i > 0 && i < findedWorker.Abilities.Count)
                                        {
                                            Console.Write(", ");
                                        }
                                        Console.Write($"{findedWorker.Abilities[i]}");
                                    }

                                }
                                else
                                {
                                    Console.Write("none");
                                }

                                Console.WriteLine();
                                Console.Write("Wanted pay: ");

                                if (findedWorker.WantedPayments > 0)
                                {
                                    Console.WriteLine($"{findedWorker.WantedPayments}");
                                }
                                else if (findedWorker.WantedPayments == 0)
                                {
                                    Console.WriteLine("none");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\"{nameOfWorker}\" is not exists");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                    }
                    else if (userInput == 3)
                    {
                        if (workers.Count > 0)
                        {
                            Console.WriteLine("Please enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null && findedWorker.Experience == null && findedWorker.Abilities.Count == 0)
                            {
                                Console.WriteLine("Input some decription and experience of worker");
                                string experienceOfWorker = Console.ReadLine();

                                if (!string.IsNullOrEmpty(experienceOfWorker))
                                {
                                    List<string> abilities = new List<string>();

                                    Console.WriteLine("Enter abilities of worker. Enter \"0\" to stop");
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

                                    findedWorker.Abilities = abilities;
                                    findedWorker.Experience = experienceOfWorker;
                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("Empty input");
                                }
                            }
                            else if (findedWorker != null)
                            {
                                if (findedWorker.Experience != null || findedWorker.Abilities.Count == 0)
                                {
                                    Console.WriteLine($"\"{findedWorker.Name}\" already has experience or abilities (or both) but you can change them with \"4\" ");
                                }
                            }
                            else if (findedWorker == null)
                            {
                                Console.WriteLine("This worker is not exists yet");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                    }
                    else if (userInput == 4)
                    {
                        if (workers.Count > 0)
                        {
                            Console.WriteLine("Please enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null && findedWorker.Experience != null)
                            {
                                Console.WriteLine("What you want to change? 1 - Name, 2 - expirience, 3 - abilities");
                                bool isParseSuccess = int.TryParse(Console.ReadLine(), out int userChoiceToChange);

                                if (isParseSuccess)
                                {
                                    if (userChoiceToChange == 1)
                                    {
                                        Console.WriteLine($"Input new name. Current name is \"{nameOfWorker}\"");
                                        string newName = Console.ReadLine();
                                        string savedName = findedWorker.Name;
                                        findedWorker.Name = null;
                                        Worker findedWorkerWithSameName = GetWorkerByName(newName, workers);

                                        if (findedWorkerWithSameName == null)
                                        {
                                            findedWorker.Name = newName;
                                            Console.WriteLine("Success");
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is worker with same name already");
                                            findedWorker.Name = savedName;
                                        }
                                    }
                                    else if (userChoiceToChange == 2)
                                    {
                                        Console.WriteLine("Please enter expirience of worker");
                                        string expirience = Console.ReadLine();

                                        if (!string.IsNullOrEmpty(expirience))
                                        {
                                            findedWorker.Experience = expirience;
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
                                                    for (int i = 0; i < findedWorker.Abilities.Count; i++)
                                                    {
                                                        Console.WriteLine($"{i + 1}) {findedWorker.Abilities[i]}");
                                                    }

                                                    Console.WriteLine("Enter number of ability to remove");
                                                    bool isNumberOfAbilityParseSuccess = int.TryParse(Console.ReadLine(), out int numberOfAbility);
                                                    int indexOfAbility = numberOfAbility - 1;

                                                    if (isNumberOfAbilityParseSuccess && findedWorker.Abilities.Count > 0)
                                                    {
                                                        if (indexOfAbility >= 0 && indexOfAbility < findedWorker.Abilities.Count)
                                                        {
                                                            findedWorker.Abilities.Remove(findedWorker.Abilities[indexOfAbility]);
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
                                                    else if (findedWorker.Abilities.Count == 0)
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
                                                        findedWorker.Abilities.Add(newAbility);
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
                            else if (findedWorker == null)
                            {
                                Console.WriteLine($"\"{nameOfWorker}\" is not exists");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                    }
                    else if (userInput == 5)
                    {
                        if (workers.Count > 0)
                        {
                            Console.WriteLine("Please input name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                Console.WriteLine("Enter wanted pay for worker");
                                bool IsPayParseSuccess = float.TryParse(Console.ReadLine(), out float wantedPay);

                                if (IsPayParseSuccess && wantedPay > 0 && wantedPay < 10000000)
                                {
                                    findedWorker.WantedPayments = wantedPay;
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
                            Console.WriteLine("There is no workers yet");
                        }
                    }
                    else if (userInput == 6)
                    {
                        if (workers.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                workers.Remove(findedWorker);

                                foreach (Vacancy vacancy in vacancies)
                                {
                                    if (vacancy.JobApplications.Contains(findedWorker))
                                    {
                                        vacancy.JobApplications.Remove(findedWorker);
                                    }
                                }

                                Console.WriteLine("Success");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no workers yet");
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

                                            if (isPaymentParseSuccess)
                                            {
                                                findedVacancy.Payments = newPayment;
                                                Console.WriteLine("Success");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Input is not number");
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

                                    foreach (Worker worker in workers)
                                    {
                                        if (worker.SavedVacancys.Contains(findedVacancy))
                                        {
                                            worker.SavedVacancys.Remove(findedVacancy);
                                        }

                                        if (worker.JobApplicationsOfWoker.Contains(findedVacancy))
                                        {
                                            worker.JobApplicationsOfWoker.Remove(findedVacancy);
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
                                    if (vacancy.NameOfRole == nameOfRole && vacancy.IsVacancyAvaivable == true)
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
                                    if (vacancy.Company == nameOfCompany && vacancy.IsVacancyAvaivable == true)
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
                                    if (vacancy.Requirements == requirements && vacancy.IsVacancyAvaivable == true)
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
                        }
                    }
                    else if (userInput == 16)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Input id of vacancy");
                            bool isParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    Console.WriteLine("Input name of worker");
                                    string nameOfWorker = Console.ReadLine();

                                    if (!string.IsNullOrEmpty(nameOfWorker))
                                    {
                                        Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                                        if (findedWorker != null && !findedVacancy.OperatedApplications.Contains(findedWorker) && !findedVacancy.JobApplications.Contains(findedWorker)&& findedVacancy.IsVacancyAvaivable == true)
                                        {
                                            findedVacancy.JobApplications.Add(findedWorker);
                                            findedWorker.JobApplicationsOfWoker.Add(findedVacancy);
                                            Console.WriteLine("Success");
                                        }
                                        else if (findedWorker == null)
                                        {
                                            Console.WriteLine("This worker is not exists");
                                        }
                                        else if (findedVacancy.OperatedApplications.Contains(findedWorker) || findedVacancy.JobApplications.Contains(findedWorker))
                                        {
                                            Console.WriteLine("This worker cant make a answer to same vacancy more than 1 time");
                                        }
                                        else if (findedVacancy.IsVacancyAvaivable == false)
                                        {
                                            Console.WriteLine("This vacancy is closed");
                                        }
                                    }
                                }
                            }
                        }
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 17)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();

                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                if (findedWorker.JobApplicationsOfWoker.Count > 0)
                                {
                                    for (int i = 0; i < findedWorker.JobApplicationsOfWoker.Count; i++)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Name of role: {findedWorker.JobApplicationsOfWoker[i].NameOfRole}");
                                        Console.WriteLine($"Name of company: {findedWorker.JobApplicationsOfWoker[i].Company}");
                                        Console.WriteLine($"Payment: {findedWorker.JobApplicationsOfWoker[i].Payments}");
                                        Console.WriteLine($"Requirements: {findedWorker.JobApplicationsOfWoker[i].Requirements}");
                                        Console.WriteLine($"Id: {findedWorker.JobApplicationsOfWoker[i].Id}");
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if ( userInput == 18)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
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
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 19)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter id of vacancy");
                            bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int idOfVacancy);

                            if (isIdParseSuccess)
                            {
                                Vacancy findedVacancy = GetVacancyById(idOfVacancy, vacancies);

                                if (findedVacancy != null)
                                {
                                    Console.WriteLine("Enter name of worker");
                                    string nameOfWorker = Console.ReadLine();
                                    Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                                    if (findedWorker != null && findedVacancy.JobApplications.Contains(findedWorker))
                                    {
                                        Console.WriteLine("Enter 1 to accept, 2 to reject");
                                        bool isChoiceParsed = int.TryParse(Console.ReadLine(), out int choice);

                                        if (isChoiceParsed)
                                        {
                                            if (choice == 1 && findedWorker.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                findedVacancy.OperatedApplications.Add(findedWorker);
                                                int indexOfVacancy = findedWorker.JobApplicationsOfWoker.IndexOf(findedVacancy);
                                                findedVacancy.AcceptedApplications.Add(findedWorker);
                                                findedWorker.JobApplicationsOfWoker.Remove(findedVacancy);
                                                findedVacancy.JobApplications.Remove(findedWorker);
                                                Console.WriteLine("Success");

                                            }
                                            else if (choice == 2 && findedWorker.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                findedVacancy.OperatedApplications.Add(findedWorker);
                                                int indexOfVacancy = findedWorker.JobApplicationsOfWoker.IndexOf(findedVacancy);
                                                findedWorker.JobApplicationsOfWoker.Remove(findedVacancy);
                                                findedVacancy.CanceledApplications.Add(findedWorker);
                                                findedVacancy.JobApplications.Remove(findedWorker);
                                                Console.WriteLine("Success");
                                            }
                                            else if (!findedWorker.JobApplicationsOfWoker.Contains(findedVacancy))
                                            {
                                                Console.WriteLine("This worker didnt accept this application");
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
                                    else if (findedWorker == null)
                                    {
                                        Console.WriteLine("This worker is not exists");
                                    }
                                    else if (!findedVacancy.JobApplications.Contains(findedWorker))
                                    {
                                        Console.WriteLine("This worker didnt send an answer on this application");
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
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 20)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                Console.WriteLine("Enter id of vacancy");
                                bool isIdParseSuccess = int.TryParse(Console.ReadLine(), out int id);

                                if (isIdParseSuccess)
                                {
                                    Vacancy findedVacancy = GetVacancyById(id, vacancies);

                                    if (findedVacancy != null && !findedWorker.SavedVacancys.Contains(findedVacancy) && findedVacancy.IsVacancyAvaivable == true)
                                    {
                                        findedWorker.SavedVacancys.Add(findedVacancy);
                                        Console.WriteLine("Success");
                                    }
                                    else if (findedVacancy == null) 
                                    {
                                        Console.WriteLine("Vacancy is not exists");
                                    }
                                    else if (findedWorker.SavedVacancys.Contains(findedVacancy))
                                    {
                                        Console.WriteLine("Cant add same vacancy to favourite list for 1 worker");
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
                                Console.WriteLine("This worker is not exists");
                            }
                        }
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 21)
                    {
                        if (workers.Count > 0 && vacancies.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                if (findedWorker.SavedVacancys.Count > 0)
                                {
                                    for (int i = 0; i < findedWorker.SavedVacancys.Count; i++)
                                    {
                                        Console.WriteLine($"{findedWorker.SavedVacancys[i].NameOfRole}");
                                        Console.WriteLine($"{findedWorker.SavedVacancys[i].IsVacancyAvaivable}");
                                        Console.WriteLine($"{findedWorker.SavedVacancys[i].Company}");
                                        Console.WriteLine($"{findedWorker.SavedVacancys[i].Payments}");
                                        Console.WriteLine($"{findedWorker.SavedVacancys[i].Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("This worker doesnt has any saved vacancies");
                                }
                            }
                            else
                            {
                                Console.WriteLine("This worker is not exists");
                            }
                        }
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                    }
                    else if (userInput == 22)
                    {
                        if (vacancies.Count  > 0)
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
                        if (vacancies.Count > 0 && workers.Count > 0)
                        {
                            Console.WriteLine("Enter name of worker");
                            string nameOfWorker = Console.ReadLine();
                            Worker findedWorker = GetWorkerByName(nameOfWorker, workers);

                            if (findedWorker != null)
                            {
                                if (findedWorker.JobApplicationsOfWoker.Count > 0)
                                {
                                    Console.WriteLine($"Number of job applications of worker: {findedWorker.JobApplicationsOfWoker.Count}");
                                }
                                else
                                {
                                    Console.WriteLine("This worker is not made any job applications");
                                }
                            }
                            else
                            {
                                Console.WriteLine("This worker is not exists yet");
                            }
                        }
                        else if (vacancies.Count == 0)
                        {
                            Console.WriteLine("There is no vacancies yet");
                        }
                        else if (workers.Count == 0)
                        {
                            Console.WriteLine("There is no workers yet");
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
                        if (workers.Count > 0)
                        {
                            foreach (Worker worker in workers)
                            {
                                Console.WriteLine($"Name of worker: {worker.Name}");
                            }
                            Console.WriteLine($"Total amount is: {workers.Count}");
                        }
                        else
                        {
                            Console.WriteLine("There is no workers");
                        }

                        if (vacancies.Count > 0)
                        {
                            foreach (Vacancy vacancy in vacancies)
                            {
                                Console.WriteLine($"Id of vacancy: {vacancy.Id}");
                            }
                            Console.WriteLine($"Total amount is: {vacancies.Count}");
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
            Console.WriteLine("Enter 1 to add new worker");
            Console.WriteLine("Enter 2 to see info about worker");
            Console.WriteLine("Enter 3 to add decription and abilities");
            Console.WriteLine("Enter 4 to change info about worker");
            Console.WriteLine("Enter 5 to change wanted pay");
            Console.WriteLine("Enter 6 to remove worker");
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
            Console.WriteLine("Enter 17 to see all answers of worker");
            Console.WriteLine("Enter 18 to see answers of potential workers");
            Console.WriteLine("Enter 19 to change status of worker's answer");
            Console.WriteLine();
            Console.WriteLine("Enter 20 to add vacancy to favourite list");
            Console.WriteLine("Enter 21 to see saved vacancies favourite list");
            Console.WriteLine("Enter 22 to see amount of all active vacancies");
            Console.WriteLine("Enter 23 to see amount of applications of worker");
            Console.WriteLine("Enter 24 to see vacancies with same or higher payment");
            Console.WriteLine("Enter 25 to see al info about program");
            Console.WriteLine("Enter 0 to stop");
        }

        static Worker GetWorkerByName(string nameOfWorker, List<Worker> workers)
        {
            foreach (Worker worker in workers)
            {
                if (worker.Name == nameOfWorker)
                {
                    return worker;
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

        static void AutoRemoverVacanciesIfTheyAreClosed(List<Vacancy> vacancies, List<Worker> workers)
        {
            foreach (Vacancy vacancy in vacancies)
            {
                if (vacancy.IsVacancyAvaivable == false)
                {
                    foreach (Worker worker in workers)
                    {
                        if (worker.SavedVacancys.Contains(vacancy) || worker.JobApplicationsOfWoker.Contains(vacancy))
                        {
                            worker.SavedVacancys.Remove(vacancy);
                            worker.JobApplicationsOfWoker.Remove(vacancy);
                        }
                    }
                }
            }
        }
    }
}