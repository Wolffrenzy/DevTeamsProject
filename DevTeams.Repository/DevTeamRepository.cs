using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DevTeamRepository
{    
    private readonly DeveloperRepository _devRepo;
    
    public DevTeamRepository(DeveloperRepository devRepo)
    {
        _devRepo = devRepo;
        Seed();
    }
    
    private List<DeveloperTeam> _devTeamDb = new List<DeveloperTeam>();

    private int _count = 0;

    public bool AddDevTeam(DeveloperTeam devTeam)
    {
        if(devTeam is null)
        {
            return false;
        }
        else
        {
            _count++;
            devTeam.ID = _count;
            _devTeamDb.Add(devTeam);
            return true;
        }
        
    }

    public List<DeveloperTeam> GetDeveloperTeams()
    {
        return _devTeamDb;
    }

    public DeveloperTeam GetDeveloperTeam(int devTeamId)
    {
        foreach (DeveloperTeam team in _devTeamDb)
        {
            if (team.ID == devTeamId)
            {
                return team;
            }
        }
        return null;

        //* return _devTeamDb.SingleOrDefault(team => team.ID == devTeamId)!;
    }

    public bool UpdateDevTeam(int devTeamId, DeveloperTeam newDevTeamData)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);

        if(oldDevTeamData != null)
        {
            oldDevTeamData.TeamName = newDevTeamData.TeamName;
            
            if (newDevTeamData.Developers.Count() > 0)
            {
                oldDevTeamData.Developers = newDevTeamData.Developers;
            }
            
            return true;
        }
        return false;
    }

    public bool DeleteDevTeam(int devTeamId)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);

        if(oldDevTeamData != null)
        {
            return _devTeamDb.Remove(oldDevTeamData);
        }
        return false;
    }

    public bool AddMultipleDevelopers(int devTeamId, List<Developer> developersToAdd)
    {
        DeveloperTeam teamInDb = GetDeveloperTeam(devTeamId);

        if(teamInDb != null)
        {
            teamInDb.Developers.AddRange(developersToAdd);
            return true;
        }
        return false;
    }
    public void Seed()
    {
        DeveloperTeam js = new DeveloperTeam()
        {
            TeamName = "Java Script Developers"
        };
        js.Developers.Add(_devRepo.GetDeveloperById(3)); //Kyle

        DeveloperTeam cSharp = new DeveloperTeam()
        {
            TeamName = "C# Developers",
        };
        cSharp.Developers.Add(_devRepo.GetDeveloperById(1)); //Gabe
        cSharp.Developers.Add(_devRepo.GetDeveloperById(2)); //Mike
        
        DeveloperTeam java = new DeveloperTeam()
        {
            TeamName = "Java Developers"
        };
    }
}
