using JetBrains.ProjectModel;

namespace ReSharperPlugin.Summoner.Finder
{
    public interface ITestFinder
    {
        IProjectFile GetTestFor(IProjectFile file);
    }
}