namespace Damaging.Components
{
    public struct TeamMember
    {
        public TeamTag Team;

        public TeamMember(TeamTag team) => this.Team = team;

        public enum TeamTag
        {
            Blue,
            Red
        }
    }
}