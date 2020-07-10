using System;
using JetBrains.Application.Progress;
using JetBrains.IDE;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.Summoner.Extensions;
using ReSharperPlugin.Summoner.Finder;

namespace ReSharperPlugin.Summoner
{

    [ContextAction(Name = "GoToUnitTests", Description = "Go to unit tests", Group = "C#", Disabled = false,
        Priority = 1)]
    public class ToLowerCaseContextAction : ContextActionBase
    {
        private readonly TestFinder _testFinder;
        private readonly IProjectFile _projectFile;

        public ToLowerCaseContextAction(LanguageIndependentContextActionDataProvider dataProvider)
        {
            _projectFile = dataProvider.PsiFile
                .GetSourceFile()
                .ToProjectFile();

            _testFinder = new TestFinder();
        }


        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var file = _testFinder.GetTestFor(_projectFile);
            EditorManager.GetInstance(solution).OpenFileAsync(file.Location, OpenFileOptions.NormalActivate);
            return null;
        }

        public override string Text => "Goto unit tests";

        public override bool IsAvailable(IUserDataHolder cache) =>
            _testFinder.GetTestFor(_projectFile)
                .Exists();
    }

}