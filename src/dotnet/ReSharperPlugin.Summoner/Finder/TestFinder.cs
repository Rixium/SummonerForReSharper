using System.Linq;
using JetBrains.ProjectModel;

namespace ReSharperPlugin.Summoner.Finder
{
    public class TestFinder : ITestFinder
    {
        private const string TestIdentifier = "Tests";

        public IProjectFile GetTestFor(IProjectFile file)
        {
            var project = file.GetProject();
            if (project == null) return null;
            var solution = project.GetSolution();
            var testProject = FindTestProjectInSolution(solution, project.Name);
            var testFile = FindTestFileInProject(testProject, file.Name);
            return testFile;
        }

        private static IProject FindTestProjectInSolution(IProjectCollection solution, string projectName) =>
            solution
                .GetAllProjects()
                .FirstOrDefault(project => project.Name.Contains(projectName) && project.Name.Contains($".{TestIdentifier}"));

        private static IProjectFile FindTestFileInProject(IProjectFolder testProject, string fileName)
        {
            if (fileName.Contains('.'))
                fileName = fileName.Split('.').First();
            var projectFiles = testProject.GetAllProjectFiles();
            return projectFiles.FirstOrDefault(file => file.Name.Contains(fileName));
        }
    }
}