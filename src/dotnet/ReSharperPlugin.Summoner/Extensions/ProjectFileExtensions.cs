using JetBrains.ProjectModel;

namespace ReSharperPlugin.Summoner.Extensions
{
    public static class ProjectFileExtensions
    {
        public static bool DoesNotExist(this IProjectFile testInfo) => testInfo == null;
        public static bool Exists(this IProjectFile testInfo) => testInfo != null;
    }
}