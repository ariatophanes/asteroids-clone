namespace Damaging.Components
{
    public struct TeamMember
    {
        public TeamTag Tag;

        public TeamMember(TeamTag tag) => this.Tag = tag;

        public enum TeamTag
        {
            Blue,
            Red
        }
    }
}