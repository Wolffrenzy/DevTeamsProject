using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//ctrl + b -> hide explorer
public class DeveloperTeam
{
    //Empty Constructor
    public DeveloperTeam() { }

    //Partial Constructor
    public DeveloperTeam(string teamName)
    {
        TeamName = teamName;
    }

    //Full Constructor
    public DeveloperTeam(string teamName, List<Developer> developers)
    {
        TeamName = teamName;
        Developers = developers;
    }

    //[KEY]
    public int ID { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public List<Developer> Developers { get; set; } = new List<Developer>();

    public override string ToString()
    {
        var str = $"TeamID: {ID}\n" +
                    $"TeamName: {TeamName}\n" +
                    $"----Team Members----\n";
        foreach (Developer dev in Developers)
        {
            str += dev + "/n";
        }

        return str;
    }
}