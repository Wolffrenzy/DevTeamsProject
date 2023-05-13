using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProgramUI
{
    private DeveloperRepository _dRepo = new DeveloperRepository();
    private DevTeamRepository _dTRepo;

    public ProgramUI()
    {
        _dTRepo = new DevTeamRepository(_dRepo);
    }
    public void Run()
    {
        RunApplication();
    }
    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("Welcome to Komodo DevTeams\n" +
                "============ Developer Management =============\n" +
                "1. View All Developers\n" +
                "2. View Developer By ID\n" +
                "3. Add Developer\n" +
                "4. Update Existing Developer\n" +
                "5. Delete Existing Developer\n" +
                "============ Developer Team Management ============\n" +
                "6. View All Developer Teams\n" +
                "7. View Developer Team By ID\n" +
                "8. Add Developer Team\n" +
                "9. Update Existing Developer Team\n" +
                "10. Delete Existing Developer Team\n" +
                "============ Additional Things To Do ============\n" +
                "11. Check Developers With PluralSight\n" +
                "12. Add Multiple Developers To A Team\n" +
                "============ Exit The Application ============\n" +
                "00. Exit Application");

            string userInput = Console.ReadLine()!;

            switch (userInput)
            {
                case "1":
                    ViewAllDevelopers();
                    break;
                case "2":
                    ViewDeveloperById();
                    break;
                case "3":
                    AddDeveloper();
                    break;
                case "4":
                    UpdateExistingDeveloper();
                    break;
                case "5":
                    DeleteExistingDeveloper();
                    break;
                case "6":
                    ViewAllDevTeams();
                    break;
                case "7":
                    ViewDevTeamById();
                    break;
                case "8":
                    AddDevTeam();
                    break;
                case "9":
                    UpdateExistingDevTeam();
                    break;
                case "10":
                    DeleteExistingDevTeam();
                    break;
                case "11":
                    DeveloperWithPluralSight();
                    break;
                case "12":
                    AddMultipleDevelopersToATeam();
                    break;
                case "00":
                    isRunning = ExitApplication();
                    break;
            }
        }
    }

    private bool ExitApplication()
    {
        Console.Clear();
        System.Console.WriteLine("Thank you for using Komodo Dev Teams!");
        PressAnyKey();
        Console.Clear();
        return false;
    }

    private void PressAnyKey()
    {
        System.Console.WriteLine("Press Any Key To Continue!");
        Console.ReadKey();
    }

    private void AddMultipleDevelopersToATeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();

            if (dTeam.Count > 0)
            {
                System.Console.WriteLine("Select a Developer Team by ID");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                List<Developer> auxDevsInDb = _dRepo.GetDevelopers();

                List<Developer> devsToAdd = new List<Developer>();

                if (team != null)
                {
                    bool hasFilledPositions = false;

                    while (!hasFilledPositions)
                    {
                        if (auxDevsInDb.Count() > 0)
                        {
                            DisplayDevelopersInDb(auxDevsInDb);
                            System.Console.WriteLine("Would you like to add a developer y/n ?");
                            string userInputAnyDevs = Console.ReadLine()!.ToLower()!;

                            if (userInputAnyDevs == "y")
                            {
                                System.Console.WriteLine("Input Developer ID");
                                int userInputDevId = int.Parse(Console.ReadLine()!);
                                Developer dev = _dRepo.GetDeveloperById(userInputDevId);
                                if (dev != null)
                                {
                                    devsToAdd.Add(dev);
                                    auxDevsInDb.Remove(dev);
                                }
                                else
                                {
                                    System.Console.WriteLine("That Developer Doesn't Exist");
                                    PressAnyKey();
                                }
                            }
                            else
                            {
                                hasFilledPositions = true;
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("There aren't any Developers in the Database..");
                            PressAnyKey();
                            break;
                        }
                    }

                    if (_dTRepo.AddMultipleDevelopers(team.ID, devsToAdd))
                    {
                        System.Console.WriteLine("Success.");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry...Invalid Developer Team ID.");
                }
            }
            PressAnyKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private void DeveloperWithPluralSight()
    {
        Console.Clear();
        List<Developer> devsWoPS = _dRepo.GetDevelopersWithoutPluralsight();
        if (devsWoPS.Count > 0)
        {
            foreach (Developer dev in devsWoPS)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            System.Console.WriteLine("Every Developer Has PluralSight!");
        }

        PressAnyKey();
    }

    private void DeleteExistingDevTeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();

            if (dTeam.Count > 0)
            {
                System.Console.WriteLine("Please select a Developer Team by ID for deletion.");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                if (team != null)
                {
                    if (_dTRepo.DeleteDevTeam(team.ID))
                    {
                        System.Console.WriteLine("Great Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Failure");
                    }
                }
                else
                {
                    System.Console.WriteLine("There aren't any Developer Teams to delete.");
                }
            }
            else
            {
                System.Console.WriteLine("There aren't any Developer Teams available.");
            }


            PressAnyKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private void UpdateExistingDevTeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();
            if (dTeam.Count > 0)
            {
                System.Console.WriteLine("Please Select a Developer Team ID To Update.");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                if (team != null)
                {
                    DeveloperTeam updatedTeamData = InitializeDTeamCreation();
                    if (_dTRepo.UpdateDevTeam(team.ID, updatedTeamData))
                    {
                        System.Console.WriteLine("Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Failure.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry, You used in Invalid ID");
                }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private void AddDevTeam()
    {
        Console.Clear();
        DeveloperTeam dTeam = InitializeDTeamCreation();
        if (_dTRepo.AddDevTeam(dTeam))
        {
            System.Console.WriteLine("Succesfully Added!");
        }
        else
        {
            System.Console.WriteLine("Failure to Add.");
        }
    }

    private DeveloperTeam InitializeDTeamCreation()
    {
        try
        {
            DeveloperTeam team = new DeveloperTeam();

            System.Console.WriteLine("What is the Team Name?");
            team.TeamName = Console.ReadLine()!;

            bool hasFilledPositions = false;

            List<Developer> auxDevelopers = _dRepo.GetDevelopers();

            while (hasFilledPositions == false)
            {
                System.Console.WriteLine("Are there any Developers on this team y/n ?");
                string userInputAnyDevs = Console.ReadLine()!.ToLower();
                if (userInputAnyDevs == "y")
                {
                    if (auxDevelopers.Count() > 0)
                    {
                        DisplayDevelopersInDb(auxDevelopers);
                        System.Console.WriteLine("Select a Developer by ID");
                        int userInputDevId = int.Parse(Console.ReadLine()!);

                        Developer selectedDeveloper = _dRepo.GetDeveloperById(userInputDevId);

                        if (selectedDeveloper != null)
                        {
                            team.Developers.Add(selectedDeveloper);
                            auxDevelopers.Remove(selectedDeveloper);
                        }
                        else
                        {
                            System.Console.WriteLine("Sorry...That Developer Doesn't Exist.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("There are no Developers in the Database.");
                        PressAnyKey();
                        break;
                    }
                }
                else
                {
                    hasFilledPositions = true;
                }
            }
            return team;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        return null;
    }

    private void DisplayDevelopersInDb(List<Developer> auxDevelopers)
    {
        if (auxDevelopers.Count > 0)
        {
            foreach (Developer dev in auxDevelopers)
            {
                System.Console.WriteLine(dev);
            }
        }
    }

    private void ViewDevTeamById()
    {
        Console.Clear();
        System.Console.WriteLine("== Developer Team List ==");
        GetDevTeamData();
        List<DeveloperTeam> devTeam = _dTRepo.GetDeveloperTeams();
        if (devTeam.Count() > 0)
        {
            System.Console.WriteLine("Select the Development Team by ID");
            int userInputDevTeamId = int.Parse(Console.ReadLine()!);
            ValidateDevTeamData(userInputDevTeamId);
        }
        PressAnyKey();
    }

    private void ValidateDevTeamData(int userInputDevTeamId)
    {
        DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);
        if (team != null)
        {
            DisplayDeveloperTeamData(team);
        }
        else
        {
            System.Console.WriteLine("Sorry...This Developer Team does not exist.");
        }
    }

    private void ViewAllDevTeams()
    {
        Console.Clear();
        System.Console.WriteLine("== Developer Team Listing ==");
        GetDevTeamData();

        PressAnyKey();
    }

    private void GetDevTeamData()
    {
        List<DeveloperTeam> dTeams = _dTRepo.GetDeveloperTeams();
        if (dTeams.Count() > 0)
        {
            foreach (DeveloperTeam team in dTeams)
            {
                DisplayDeveloperTeamData(team);
            }
        }
        else
        {
            System.Console.WriteLine("There are no available developer teams.");
        }
    }

    private void DisplayDeveloperTeamData(DeveloperTeam team)
    {
        System.Console.WriteLine(team);
    }

    private void DeleteExistingDeveloper()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("================\n");
        try
        {
            System.Console.WriteLine("Select Developer By ID.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            var isValidated = ValidateDeveloperInDatabase(userInputDevId);

            if (isValidated)
            {
                System.Console.WriteLine("Do you want to delete this Developer y/n");
                string userInputDeleteDev = Console.ReadLine()!.ToLower();
                if (userInputDeleteDev == "y")
                {
                    if (_dRepo.DeleteDeveloper(userInputDevId))
                    {
                        System.Console.WriteLine($"The Developer was Deleted!");
                    }
                    else
                    {
                        System.Console.WriteLine($"The Developer was NOT Deleted!");
                    }
                }
            }
            else
            {
                System.Console.WriteLine($"The Developer with the ID: {userInputDevId} Does NOT Exist.");
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }


        PressAnyKey();
    }

    private void UpdateExistingDeveloper()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("-----------------\n");
        try
        {
            Console.WriteLine("Select Developer by ID.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            Developer devInDb = GetDeveloperDataFromDb(userInputDevId);
            bool isValidated = ValidateDeveloperInDatabase(devInDb.ID);

            if (isValidated)
            {
                Console.WriteLine("Do you want to update this Developer y/n");
                string userInputUpdateDev = Console.ReadLine()!.ToLower();
                if (userInputUpdateDev == "y")
                {
                    Developer updateDevData = InitializeDevCreationSetup();

                    if (_dRepo.UpdateDeveloper(devInDb.ID, updateDevData))
                    {
                        Console.WriteLine($"The Developer {updateDevData.FullName} has been updated!");
                    }
                    else
                    {
                        Console.WriteLine($"The Developer {updateDevData.FullName} WAS NOT updated!");
                    }
                }
            }

            else
            {
                System.Console.WriteLine("Developer doesn't Exist!");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        PressAnyKey();
    }

    private void AddDeveloper()
    {
        Console.Clear();

        try
        {
            Developer dev = InitializeDevCreationSetup();
            if (_dRepo.AddDeveloper(dev))
            {
                System.Console.WriteLine($"Successfully Added {dev.FullName} to the Database!");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private Developer InitializeDevCreationSetup()
    {
        Developer dev = new Developer();
        Console.WriteLine("== Add Developer Menu ==");

        Console.WriteLine("What is the developer's First Name?");
        dev.FirstName = Console.ReadLine()!;

        Console.WriteLine("What is the developer's Last Name?");
        dev.LastName = Console.ReadLine()!;

        Console.WriteLine("Does this Developer have a PluralSight Account?\n" +
                            "1. Yes\n" +
                            "2. No\n");

        string userInputPsAcct = Console.ReadLine()!;

        switch (userInputPsAcct)
        {
            case "1":
                dev.HasPluralsight = true;
                break;

            default:

                dev.HasPluralsight = false;
                break;

        }
        return dev;
    }

    private void ViewDeveloperById()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("----------\n");

        try
        {
            Console.WriteLine("Select Developer by ID.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            ValidateDeveloperInDatabase(userInputDevId);
        }
        catch (Exception ex)
        {
            SomethingWentWrong();
            System.Console.WriteLine(ex.Message);
        }
        PressAnyKey();
    }

    private bool ValidateDeveloperInDatabase(int userInputDevId)
    {
        Developer dev = GetDeveloperDataFromDb(userInputDevId);
        if (dev != null)
        {
            Console.Clear();
            DisplayDevData(dev);
            return true;
        }
        else
        {
            Console.WriteLine($"The Developer with the ID: {userInputDevId} doesn't exist!");
            return false;
        }
    }

    private Developer GetDeveloperDataFromDb(int userInputDevId)
    {
        return _dRepo.GetDeveloperById(userInputDevId);
    }

    private void SomethingWentWrong()
    {
        Console.WriteLine("Something Went Wrong.\n" +
                            "Please Try Again.\n" +
                            "Returning to Developer Menu");
    }

    private void ViewAllDevelopers()
    {
        Console.Clear();
        //Helper Method
        ShowEnlistedDevs();
        PressAnyKey();
    }

    private void ShowEnlistedDevs()
    {
        Console.Clear();
        Console.WriteLine("== Developer Listing ==");
        List<Developer> devsInDb = _dRepo.GetDevelopers();
        ValidateDeveloperDatabaseData(devsInDb);
    }

    private void ValidateDeveloperDatabaseData(List<Developer> devsInDb)
    {
        if (devsInDb.Count > 0)
        {
            Console.Clear();
            foreach (Developer dev in devsInDb)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            Console.WriteLine("There are no developers in the Database..");
        }
    }

    private void DisplayDevData(Developer dev)
    {
        Console.WriteLine(dev);
    }
}


