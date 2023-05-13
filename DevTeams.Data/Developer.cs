using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Developer
{
    public Developer()
    {

    }

    public Developer(string firstName, string lastName, bool hasPluralsight)
    {
        FirstName = firstName;
        LastName = LastName;
        HasPluralsight = hasPluralsight;
    }

    //We need a Primary Key
    public int ID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }

    }
    public bool HasPluralsight { get; set; } //Defaults to false

    //Everytime I do: Developer.ToString()
    public override string ToString()
    {
        var str = $"ID: {ID}\n" +
                $"Full Name: {FullName}\n" +
                $"Has Pluralsight Access: {HasPluralsight}\n" +
                "===========================================\n";

        return str;
    }

}

//! THIS IS THE DEVELOPER OBJECT P.O.C.O -> Plain Old cSharp Object
//! Domain Object (Domain Driven Design)