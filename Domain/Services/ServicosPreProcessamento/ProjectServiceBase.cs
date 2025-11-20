using Domain.Agreggates;

namespace Domain.Services.ProjectServices
{
    public abstract class ProjectServiceBase
    {
        private ProjectServiceBase? _next;

        public ProjectServiceBase SetNext(ProjectServiceBase next)
        {
            _next = next;
            return next;
        }
        public void Handle(Project project)
        {
            Execute(project);
            _next?.Handle(project);
        }

        public abstract void Execute(Project project);

    }
}
