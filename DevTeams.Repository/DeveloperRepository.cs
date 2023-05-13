using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DeveloperRepository
{

    public DeveloperRepository()
    {
        Seed();
    }
    //We need a variable vontainer that will hold the collection of Developers
    private List<Developer> _developerDb = new List<Developer>();
    //we need to auto-increment the developer ID
    private int _count = 0;

    //C.R.U.D

    //Create
    public bool AddDeveloper(Developer developer)
    {
        if(developer is null)
        {
            return false;
        }
        else
        {
            //increment the _count;
            _count++;
            //assign the developerID to _count
            developer.ID = _count;
            //save to the database
            _developerDb.Add(developer);
            
            return true;
        }
    }

    //Read All
    public List<Developer> GetDevelopers()
    {
        return _developerDb;
    }

    //Read by ID
    public Developer GetDeveloperById(int developerId)
    {
        //look through the whole database
        foreach (var developer in _developerDb)
        {
            //Check to see if the developer has a matching ID
            if(developer.ID == developerId)
            {
                return developer;
            }
        }

        return null!;
    }

    //Update
    public bool UpdateDeveloper(int developerId, Developer newDevData)
    {
        //Let's check to see if the developer exists
        Developer oldDevData = GetDeveloperById(developerId);

        //If we have the data
        if(oldDevData != null)
        {
            oldDevData.FirstName = newDevData.FirstName;
            oldDevData.LastName = newDevData.LastName;
            oldDevData.HasPluralsight = newDevData.HasPluralsight;
            //after values have been updated
            return true;

        }
        //If old dev data returns null
        return false;
    }
    
    //Delete
    public bool DeleteDeveloper (int developerId)
    {
        Developer oldDevData = GetDeveloperById(developerId);
        
        if(oldDevData != null)
        {
            return _developerDb.Remove(oldDevData);
        }

        return false;
    }

    //Developers without PluralSight
    public List<Developer> GetDevelopersWithoutPluralsight()
    {
        List<Developer> devsWithOutPS = new List<Developer>();

        foreach (Developer developer in _developerDb)
        {
            if(developer.HasPluralsight == false)
            {
                devsWithOutPS.Add(developer);
            }
        }
        
        return devsWithOutPS;
    }
    
    //Seed Developers
    private void Seed()
    {
        Developer gabe = new Developer
        {
            FirstName = "Gabe",
            LastName = "Wolf",
            HasPluralsight = true
        };

        Developer mike = new Developer
        {
            FirstName = "Mike",
            LastName = "Aspatore",
            HasPluralsight = false
        };

        Developer kyle = new Developer
        {
            FirstName = "Kyle",
            LastName = "Dieruf",
            HasPluralsight = true
        };

        AddDeveloper(gabe);
        AddDeveloper(mike);
        AddDeveloper(kyle);
    }
}